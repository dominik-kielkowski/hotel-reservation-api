using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelReservation.Tests
{
    public class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
    {
        public BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            HttpClient = factory.CreateClient();
        }

        protected HttpClient HttpClient { get; set; }
    }
}
