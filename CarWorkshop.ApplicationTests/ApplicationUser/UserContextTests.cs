using Xunit;
using CarWorkshop.Application.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using FluentAssertions;

namespace CarWorkshop.Application.ApplicationUser.Tests
{
    public class UserContextTests
    {
        [Fact()]
        public void GetCurrentUser_WithAuthenticatedUser_ShouldReturnCurrentUser()
        {
            // arrange
            var claims = new List<Claim>() 
            { 
                new Claim(ClaimTypes.NameIdentifier, "1"),
                new Claim(ClaimTypes.Email, "mail@example.test"),
                new Claim(ClaimTypes.Role, "Admin"),
                new Claim(ClaimTypes.Role, "User")
            };

            var user = new ClaimsPrincipal(new ClaimsIdentity(claims, "Test"));

            var httpContextAccessor = new Mock<IHttpContextAccessor>();

            httpContextAccessor.Setup(x => x.HttpContext).Returns(new DefaultHttpContext()
            {
                User = user
            });

            var userContext = new UserContext(httpContextAccessor.Object);

            // act
            var currentUser = userContext.GetCurrentUser();

            // assert
            currentUser.Should().NotBeNull();
            currentUser!.Id.Should().Be("1");
            currentUser!.Email.Should().Be("mail@example.test");
            currentUser!.Roles.Should().ContainInOrder("Admin", "User");
        }
    }
}