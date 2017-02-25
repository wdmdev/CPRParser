using Xunit;
using System;

namespace CPRChecker.Tests
{
    public class DynamicsCPRTrimmerTests
    {
        [Theory(DisplayName = "Trim given CPR with non-digit characters returns only CPR number")]
        [InlineData("250793-3001", "2507933001")]
        [InlineData("250793 3001", "2507933001")]
        [InlineData("250793d3001", "2507933001")]
        [InlineData("d2507933001", "2507933001")]
        [InlineData(" 2507933001", "2507933001")]
        [InlineData("-2507933001", "2507933001")]
        [InlineData("-2507933001\n", "2507933001")]
        [InlineData("-2507933001\r", "2507933001")]
        [InlineData("2507933001.0", "2507933001")]
        [InlineData("2507933001", "2507933001")]
        public void Trim_given_CPR_with_non_digit_characters_returns_only_CPR_number(string untrimmedCPR, string trimmedCPR)
        {
            //Arrange
            var cprTrimmer = new DynamicsCPRTrimmer();

            //Act
            var result = cprTrimmer.Trim(untrimmedCPR);

            //Assert
            Assert.Equal(trimmedCPR, result);
        }

        [Fact(DisplayName = "Trim given null throws new ArgumentNullException")]
        public void Trim_given_null_throws_new_ArgumentNullException()
        {
            //Arrange
            var cprTrimmer = new DynamicsCPRTrimmer();

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => cprTrimmer.Trim(null));
        }

        [Fact(DisplayName = "Trim given empty string throws new ArgumentException")]
        public void Trim_given_empty_string_throws_new_ArgumentException()
        {
            //Arrange
            var cprTrimmer = new DynamicsCPRTrimmer();

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => cprTrimmer.Trim(string.Empty));
        }
    }
}