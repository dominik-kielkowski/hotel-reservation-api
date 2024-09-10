using Application.Users;
using FluentAssertions;
using HotelReservation.Tests.Utilities;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace HotelReservation.Tests.Users
{
    public class RegisterUserTests : BaseTest
    {
        public RegisterUserTests(TestWebAppFactory factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_ReturnSuccess_WhenSuccessfullyRegisteringUser()
        {
            // Arrange
            var user = new RegisterDto
            {
                DisplayName = "TestName",
                Email = "testtt@mail.com",
                Password = "Pa$$w0rd"
            };

            // Act
            var response = await HttpClient.PostAsJsonAsync("api/Account/register", user);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
