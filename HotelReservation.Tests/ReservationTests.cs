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
            var room = new Room()
            {
                Id = 1,
                Name = "Test",
                Description = "Test",
                Price = 123,
                RoomSize = 10,
            };

            var reservation = new Reservation()
            {
                Begin = DateTime.UtcNow.AddHours(1),
                End = DateTime.UtcNow.AddHours(2)
            };

            // Act
            HttpResponseMessage postRoomResponse = await HttpClient.PostAsJsonAsync("api/Room", room);
            HttpResponseMessage postReservationResponse = await HttpClient.PostAsJsonAsync("api/1/reservations", reservation);

            // Assert
            Assert.NotNull(postRoomResponse);
        }

    }
}


