using AutoMapper;
using CarWorkshop.Application.ApplicationUser;
using CarWorkshop.Application.CarWorkshop;
using FluentAssertions;
using Moq;
using Xunit;

namespace CarWorkshop.Application.Mappings.Tests
{
    public class CarWorkshopMappingProfileTests
    {
        [Fact()]
        public void MappingProfile_ShouldMapCarWorkshopDtoToCarWorkshop()
        {
            // arrange 

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(x => x.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "Moderator" }));

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new CarWorkshopMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var dto = new CarWorkshopDto()
            {
                Name = "Name",
                PhoneNumber = "123456789",
                Street = "Street",
                City = "City",
                PostalCode = "12-345"
            };

            // act
            var result = mapper.Map<Domain.Entities.CarWorkshop>(dto);

            // assert

            result.Should().NotBeNull();

            result.Name.Should().Be(dto.Name);
            result.ContactDetails.City.Should().Be(dto.City);
            result.ContactDetails.PhoneNumber.Should().Be(dto.PhoneNumber);
            result.ContactDetails.PostalCode.Should().Be(dto.PostalCode);
            result.ContactDetails.Street.Should().Be(dto.Street);
        }


        [Fact()]
        public void MappingProfile_WhenUserIsAuthorized_ShouldMapCarWorkshopToCarWorkshopDtoEditable()
        {
            // arrange 

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(x => x.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "Moderator" }));

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new CarWorkshopMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "1",
                Name = "Name",
                ContactDetails = new()
                {
                    PhoneNumber = "123456789",
                    Street = "Street",
                    City = "City",
                    PostalCode = "12-345"
                }
            };

            // act
            var result = mapper.Map<CarWorkshopDto>(carWorkshop);

            // assert

            result.Should().NotBeNull();

            result.Name.Should().Be(carWorkshop.Name);
            result.City.Should().Be(carWorkshop.ContactDetails.City);
            result.PhoneNumber.Should().Be(carWorkshop.ContactDetails.PhoneNumber);
            result.PostalCode.Should().Be(carWorkshop.ContactDetails.PostalCode);
            result.Street.Should().Be(carWorkshop.ContactDetails.Street);
            result.IsEditable.Should().BeTrue();
        }

        [Fact()]
        public void MappingProfile_WhenUserIsNotAuthorized_ShouldMapCarWorkshopToCarWorkshopDtoNoEditable()
        {
            // arrange 

            var userContextMock = new Mock<IUserContext>();

            userContextMock.Setup(x => x.GetCurrentUser())
                .Returns(new CurrentUser("1", "test@test.com", new[] { "User" }));

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(new CarWorkshopMappingProfile(userContextMock.Object)));

            var mapper = configuration.CreateMapper();

            var carWorkshop = new Domain.Entities.CarWorkshop()
            {
                Id = 1,
                CreatedById = "2",
                Name = "Name",
                ContactDetails = new()
                {
                    PhoneNumber = "123456789",
                    Street = "Street",
                    City = "City",
                    PostalCode = "12-345"
                }
            };

            // act
            var result = mapper.Map<CarWorkshopDto>(carWorkshop);

            // assert

            result.Should().NotBeNull();

            result.Name.Should().Be(carWorkshop.Name);
            result.City.Should().Be(carWorkshop.ContactDetails.City);
            result.PhoneNumber.Should().Be(carWorkshop.ContactDetails.PhoneNumber);
            result.PostalCode.Should().Be(carWorkshop.ContactDetails.PostalCode);
            result.Street.Should().Be(carWorkshop.ContactDetails.Street);
            result.IsEditable.Should().BeFalse();
        }
    }
}