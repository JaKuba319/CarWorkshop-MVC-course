using Xunit;
using CarWorkshop.Application.ApplicationUser;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CarWorkshop.Application.ApplicationUser.Tests
{
    public class CurrentUserTests
    {
        [Fact()]
        public void IsInRole_WithMatchingRole_ShouldReturnTrue()
        {
            // arrange 
            var user = new CurrentUser("1", "mail", new List<string>() { "Admin", "User" });

            // act
            bool isInRole = user.IsInRole("User");

            // assert
            isInRole.Should().BeTrue();
        }

        [Fact()]
        public void IsInRole_WithNoMatchingRole_ShouldReturnFalse()
        {
            // arrange 
            var user = new CurrentUser("1", "mail", new List<string>() { "Admin", "User" });

            // act
            bool isInRole = user.IsInRole("Manager");

            // assert
            isInRole.Should().BeFalse();
        }

        [Fact()]
        public void IsInRole_WithNoMatchingCaseRole_ShouldReturnFalse()
        {
            // arrange 
            var user = new CurrentUser("1", "mail", new List<string>() { "Admin", "User" });

            // act
            bool isInRole = user.IsInRole("user");

            // assert
            isInRole.Should().BeFalse();
        }
    }
}