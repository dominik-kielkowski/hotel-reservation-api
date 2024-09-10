using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HotelReservation.Tests.Utilities
{
    public class BaseTest : IClassFixture<TestWebAppFactory>
    {
        protected HttpClient HttpClient { get; }

        public BaseTest(TestWebAppFactory factory)
        {
            HttpClient = factory.CreateClient();
        }
    }
}
