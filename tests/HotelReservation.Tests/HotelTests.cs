using Application.Hotels;
using Core.Entities.Rooms;
using Core.Hotels;
using FluentAssertions;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Testcontainers.PostgreSql;
using Xunit;

namespace HotelReservation.Tests
{
    public class HotelTests : BaseIntegrationTest
    {
        public HotelTests(IntegrationTestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task PostAndRetrieveHotel_ShouldReturnOkAndCorrectData()
        {
            // Arrange
            var newHotel = new Hotel
            {
                Id = 1,
                Name = "Test",
                Description = "Description",
                Address = new Address { },
                Rooms = new List<Room>()
            };

            // Act
            HttpResponseMessage postResponse = await HttpClient.PostAsJsonAsync("/api/hotel", newHotel);
            postResponse.EnsureSuccessStatusCode();
            HttpResponseMessage getResponse = await HttpClient.GetAsync($"/api/hotel/{newHotel.Id}");

            // Assert
            getResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var retrievedHotel = await getResponse.Content.ReadFromJsonAsync<HotelDto>();
            retrievedHotel.Should().NotBeNull();
            retrievedHotel.Id.Should().Be(newHotel.Id);
            retrievedHotel.Name.Should().Be(newHotel.Name);
            retrievedHotel.Description.Should().Be(newHotel.Description);
        }
    }
}
