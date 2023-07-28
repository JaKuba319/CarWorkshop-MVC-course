using CarWorkshop.Application.CarWorkshop;
using CarWorkshop.Application.CarWorkshop.Queries.GetAllCarWorkshops;
using CarWorkshop.Domain.Interfaces;
using FluentAssertions;
using MediatR;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Data.SqlClient;
using Moq;
using System.Net;
using Xunit;

namespace CarWorkshop.MVC.Controllers.Tests
{
    public class CarWorkshopControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public CarWorkshopControllerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }
        [Fact()]
        public async Task Index_ReturnsViewWithModel_ForExistingCarWorkshops()
        {
            // arrange
            var carWorkshops = new List<CarWorkshopDto>()
            {
                new CarWorkshopDto()
                {
                    Name = "Name 1"
                },
                new CarWorkshopDto()
                {
                    Name = "Name 2"
                },
                new CarWorkshopDto()
                {
                    Name = "Name 3"
                }
            };

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(x => x.Send(It.IsAny<GetAllCarWorkshopsQuerry>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(carWorkshops);

            var client = _factory
                .WithWebHostBuilder(builder => 
                    builder.ConfigureTestServices(services 
                        => services.AddScoped(_ => mediatorMock.Object)))
                .CreateClient();

            // act
            var response = await client.GetAsync("/CarWorkshop/Index");

            // assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("<h5 class=\"card-title\">Name 1</h5>")
                .And.Contain("<h5 class=\"card-title\">Name 2</h5>")
                .And.Contain("<h5 class=\"card-title\">Name 3</h5>");

        }

        [Fact()]
        public async Task Index_ReturnsEmptyView_WhenCarWorkshopsDoNotExist()
        {
            // arrange
            var carWorkshops = new List<CarWorkshopDto>();

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(x => x.Send(It.IsAny<GetAllCarWorkshopsQuerry>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(carWorkshops);

            var client = _factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services
                        => services.AddScoped(_ => mediatorMock.Object)))
                .CreateClient();

            // act
            var response = await client.GetAsync("/CarWorkshop/Index");

            // assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().NotContain("<div class=\"card m-3\" style=\"width: 18rem;\">");

        }

        [Fact()]
        public async Task Index_ReturnsViewWithModelAndEditButton_WhenCarWorkshopExistAndCanBeEdited()
        {
            // arrange
            var carWorkshops = new List<CarWorkshopDto>()
            {
                new CarWorkshopDto()
                {
                    Name = "Name 1",
                    IsEditable = true
                }
            };

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(x => x.Send(It.IsAny<GetAllCarWorkshopsQuerry>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(carWorkshops);

            var client = _factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services
                        => services.AddScoped(_ => mediatorMock.Object)))
                .CreateClient();

            // act
            var response = await client.GetAsync("/CarWorkshop/Index");

            // assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().Contain("Edit");

        }

        [Fact()]
        public async Task Index_ReturnsViewWithModelWithoutEditButton_WhenCarWorkshopExistAndCantBeEdited()
        {
            // arrange
            var carWorkshops = new List<CarWorkshopDto>()
            {
                new CarWorkshopDto()
                {
                    Name = "Name 1",
                    IsEditable = false
                }
            };

            var mediatorMock = new Mock<IMediator>();

            mediatorMock.Setup(x => x.Send(It.IsAny<GetAllCarWorkshopsQuerry>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(carWorkshops);

            var client = _factory
                .WithWebHostBuilder(builder =>
                    builder.ConfigureTestServices(services
                        => services.AddScoped(_ => mediatorMock.Object)))
                .CreateClient();

            // act
            var response = await client.GetAsync("/CarWorkshop/Index");

            // assert

            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var content = await response.Content.ReadAsStringAsync();

            content.Should().NotContain("Edit");

        }
    }
}