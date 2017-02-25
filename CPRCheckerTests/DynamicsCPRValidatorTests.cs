using Xunit;
using System;
using Moq;

namespace CPRChecker.Tests
{
    public class DynamicsCPRValidatorTests
    {
        [Theory(DisplayName = "Validate given invalid CPR length returns false")]
        [InlineData("25079330012")]
        [InlineData("25")]
        [InlineData("")]
        public void Validate_given_invalid_CPR_length_returns_false(string cpr)
        {
            //Arrange
            var parser = new Mock<ISerialNumberParser>();
            parser.Setup(p => p.Parse(It.IsAny<int>(), It.IsAny<int>())).Returns(1993);
            var validator = new DynamicsCPRValidator(parser.Object);

            //Act
            var result = validator.IsValid(cpr);

            //Assert
            Assert.False(result);
        }

        [Fact(DisplayName = "Validate given valid CPR length returns true")]
        public void Validate_given_valid_CPR_length_returns_true()
        {
            //Arrange
            var parser = new Mock<ISerialNumberParser>();
            parser.Setup(p => p.Parse(It.IsAny<int>(), It.IsAny<int>())).Returns(1993);
            var validator = new DynamicsCPRValidator(parser.Object);

            //Act
            var result = validator.IsValid("2507933001");

            //Assert
            Assert.True(result);
        }

        [Fact(DisplayName = "Validate given null throws new ArgumentNullException")]
        public void Validate_given_null_throws_new_ArgumentNullException()
        {
            //Arrange
            var parser = new Mock<ISerialNumberParser>();
            parser.Setup(p => p.Parse(It.IsAny<int>(), It.IsAny<int>())).Returns(1993);
            var validator = new DynamicsCPRValidator(parser.Object);

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => validator.IsValid(null));
        }
    }
}