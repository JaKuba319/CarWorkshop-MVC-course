using Xunit;
using CarWorkshop.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;

namespace CarWorkshop.Domain.Entities.Tests
{
    public class CarWorkshopTests
    {
        [Fact()]
        public void EncodeName_ShouldSetEncodedName()
        {
            // arrange
            var carworkshop = new CarWorkshop();
            carworkshop.Name = "TEST NAME";

            // act
            carworkshop.EncodeName();

            // assert
            carworkshop.EncodedName.Should().Be("test-name");
        }

        [Fact()]
        public void EncodeName_ShouldThrowException_WhenNameIsNull()
        {
            // arrange
            var carworkshop = new CarWorkshop();

            // act
            Action action = () => carworkshop.EncodeName();

            // assert
            action.Invoking(a => a.Invoke())
                .Should().Throw<NullReferenceException>();

        }
    }
}