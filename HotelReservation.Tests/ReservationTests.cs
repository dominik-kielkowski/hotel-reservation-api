using Core.Hotels.Rooms;
using Core.Hotels;
using FluentAssertions;
using HotelReservationWebsite.Hotels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Core.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Moq;
using Core.User;
using System.Net.Http.Headers;
using Azure;
using Application.Users;

namespace HotelReservation.Tests
{
    public class ReservationTests : BaseIntegrationTest
    {
        public ReservationTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task MakeReservation_ValidReservation_Success()
        {
            // Arrange
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


            // Act

            HttpResponseMessage postUserResponse = await HttpClient.PostAsJsonAsync("api/Account/register", user);

            HttpResponseMessage loginResponse = await HttpClient.PostAsJsonAsync("api/Account/login", loginDto);

            // Check if login was successful
            if (loginResponse.IsSuccessStatusCode)
            {
                // Read the response content as UserDto
                var userDto = await loginResponse.Content.ReadFromJsonAsync<UserDto>();

                // Set the authorization token for subsequent requests
                HttpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", userDto.Token);

                // Register the user

                // Post the hotel
                HttpResponseMessage postRoomResponse = await HttpClient.PostAsJsonAsync("api/Hotel", hotel);

                // Post the reservation
                HttpResponseMessage postReservationResponse = await HttpClient.PostAsJsonAsync("api/Room/1/reservations", reservation);

                // Assert
                Assert.True(postReservationResponse.IsSuccessStatusCode);
            }
        }
    }
}