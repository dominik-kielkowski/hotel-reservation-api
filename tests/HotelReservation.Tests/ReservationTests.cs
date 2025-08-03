using System.Net.Http.Json;
using Xunit;
using Core.User;
using System.Net.Http.Headers;
using Application.Users;
using Core.Entities.Rooms;
using Core.Entities.Hotels;
using Application.Hotels.Rooms.MakeReservation;
using HotelReservation.Application.Commands_Queries.Rooms.MakeReservation;
using HotelReservation.Tests.Utilities;

namespace HotelReservation.Tests
{
    public class ReservationTests : BaseTest
    {
        public ReservationTests(TestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task MakeReservation_ValidReservation_Success()
        {
            var hotel = new Hotel()
            {
                Name = "Test",
                Description = "Test",
                Address = new Address() { },
                Rooms = new List<Room>
                {
                    new Room {
                        Id = 1,
                        Name = "Test",
                        Description = "Test",
                        Price = 123,
                        RoomSize = 10
                    }
                }
            };

            var user = new AppUser()
            {
                Id = Guid.NewGuid().ToString(),
                DisplayName = "Test",
                Email = "Test@test.com",
                Password = "Pa$$w0rd"
            };

            var loginDto = new LoginDto
            {
                Email = "Test@test.com",
                Password = "Pa$$w0rd"
            };

            var reservation = new Reservation()
            {
                RoomId = 1,
                Begin = DateTime.UtcNow.AddHours(1),
                End = DateTime.UtcNow.AddHours(2),
                CustomerId = Guid.Parse(user.Id)
            };

            MakeReservationCommand command = new MakeReservationCommand(1, Guid.Parse(user.Id), "test@gmail.com" , reservation);

            HttpResponseMessage postUserResponse = await HttpClient.PostAsJsonAsync("api/Account/register", user);

            Assert.True(postUserResponse.IsSuccessStatusCode);

            HttpResponseMessage loginResponse = await HttpClient.PostAsJsonAsync("api/Account/login", loginDto);

            Assert.True(loginResponse.IsSuccessStatusCode);

            var userDto = await loginResponse.Content.ReadFromJsonAsync<UserDto>();

            HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userDto.Token);

            HttpResponseMessage postRoomResponse = await HttpClient.PostAsJsonAsync("api/Hotel", hotel);

            HttpResponseMessage postReservationResponse = await HttpClient.PostAsJsonAsync("api/Room/1/reservations", reservation);

            Assert.True(postReservationResponse.IsSuccessStatusCode);
        }
    }
}