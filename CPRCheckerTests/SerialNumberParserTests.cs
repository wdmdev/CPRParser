using Xunit;
using System;

namespace CPRChecker.Tests
{
    public class SerialNumberParserTests
    {
        [Theory(DisplayName = "ParseSerialNumber given valid serial number returns correct year")]
        [InlineData(93, 2, 1993)]
        [InlineData(36, 4, 2036)]
        [InlineData(37, 4, 1937)]
        [InlineData(57, 6, 2057)]
        [InlineData(99, 6, 1899)]
        [InlineData(36, 9, 2036)]
        public void ParseSerialNumber_given_valid_serial_number_returns_correct_year(int yearEnding, int serialNumber, int year)
        {
            //Arrange
            var serialParser = new SerialNumberParser();

            //Act
            var result = serialParser.Parse(yearEnding, serialNumber);

            //Assert
            Assert.Equal(year, result);
        }

        [Theory(DisplayName = "ParseSerialNumber given invalid serial number throws new ArgumentException")]
        [InlineData(-1)]
        [InlineData(10)]
        public void ParseSerialNumber_given_invalid_serial_number_throws_new_ArgumentException(int serialNumber)
        {
            //Arrange
            var serialParser = new SerialNumberParser();

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => serialParser.Parse(93, serialNumber));
        }

        [Theory(DisplayName = "ParseSerialNumber given invalid year throws new ArgumentException")]
        [InlineData(-93)]
        [InlineData(19935)]
        [InlineData(135)]
        public void ParseSerialNumber_given_invalid_year_throws_new_ArgumentException(int year)
        {
            //Arrange
            var serialParser = new SerialNumberParser();

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => serialParser.Parse(year, 2));
        }

    }
}