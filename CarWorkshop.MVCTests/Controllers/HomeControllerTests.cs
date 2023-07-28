using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using Xunit;

namespace CarWorkshop.MVC.Controllers.Tests
{
    public class HomeControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public HomeControllerTests(WebApplicationFactory<Program> factory) 
        {
            _factory = factory;
        }


        [Fact()]
        public async Task About_ReturnsViewWithModel()
        {
            // arrange
            var client = _factory.CreateClient();

            // act
            var response = await client.GetAsync("/Home/About");

            // assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("<h1>About</h1>")
                .And.Contain("<div class=\"alert alert-success\">Test description.</div>")
                .And.Contain("<li>Car</li>")
                .And.Contain("<li>Workshop</li>")
                .And.Contain("<li>Car services</li>")
                .And.Contain("<li>Mechanic</li>");
            
        }
    }
}