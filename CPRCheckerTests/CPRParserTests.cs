using Xunit;
using System;
using Moq;

namespace CPRChecker.Tests
{
    public class CPRParserTests
    {
        #region Check
        [Fact(DisplayName = "Check given valid cpr returns cpr")]
        public void CPRCheckerTest()
        {
            //Arrange
            var cpr = "2507933001";
            var trimmer = new Mock<ITrimmer>();
            trimmer.Setup(t => t.Trim(It.IsAny<string>())).Returns(cpr);
            var validator = new Mock<IValidator>();
            validator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(true);
            var serialParser = new Mock<ISerialNumberParser>();
            var checker = new CPRParser(
                trimmer.Object, validator.Object, serialParser.Object);

            //Act
            var result = checker.Parse(cpr);

            //Assert
            Assert.Equal(cpr, result);
        }

        [Fact(DisplayName = "Check given invalid cpr returns null")]
        public void CheckTest()
        {
            //Arrange
            var cpr = "25079330011";
            var trimmer = new Mock<ITrimmer>();
            trimmer.Setup(t => t.Trim(It.IsAny<string>())).Returns(cpr);
            var validator = new Mock<IValidator>();
            validator.Setup(v => v.IsValid(It.IsAny<string>())).Returns(false);
            var serialParser = new Mock<ISerialNumberParser>();
            var checker = new CPRParser(
                trimmer.Object, validator.Object, serialParser.Object);

            //Act
            var result = checker.Parse(cpr);

            //Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Check given null returns null")]
        public void Check_given_null_returns_null()
        {
            //Arrange
            var trimmer = new Mock<ITrimmer>();
            var validator = new Mock<IValidator>();
            var serialParser = new Mock<ISerialNumberParser>();
            var checker = new CPRParser(
                trimmer.Object, validator.Object, serialParser.Object);

            //Act
            var result = checker.Parse(null);

            //Assert
            Assert.Null(result);
        }

        #endregion

        #region GetBirthDate

        [Fact(DisplayName="GetBrithDate given 2507933001 returns birthdate 25-07-1993")]
        public void GetBirthDate_given_2507933001_returns_brithdate_25_07_1993()
        {
            //Arrange
            var cpr = "2507933001";
            var trimmer = new Mock<ITrimmer>();
            trimmer.Setup(t => t.Trim(cpr)).Returns(cpr);
            var validator = new Mock<IValidator>();
            validator.Setup(v => v.IsValid(cpr)).Returns(true);
            var serialParser = new Mock<ISerialNumberParser>();
            serialParser.Setup(s => s.Parse(93, 3)).Returns(1993);
            var checker = new CPRParser(
                trimmer.Object, validator.Object, serialParser.Object);

            //Act
            var result = checker.GetBirthDate(cpr);

            //Assert
            var expected = new DateTime(1993, 07, 25);
            Assert.Equal(expected, result);
        }

        [Theory(DisplayName = "GetBirthDate given invalid CPR throws new ArgumentException")]
        [InlineData("-250793301")]
        [InlineData("")]
        [InlineData("2507933011")]
        [InlineData("3307933011")]
        [InlineData("2514933011")]
        public void GetBirthDate_given_invalid_CPR_throws_new_ArgumentException(string cpr)
        {
            //Arrange
            var trimmer = new Mock<ITrimmer>();
            var validator = new Mock<IValidator>();
            var serialParser = new Mock<ISerialNumberParser>();
            serialParser.Setup(s => s.Parse(93, 3)).Returns(1993);
            var checker = new CPRParser(
                trimmer.Object, validator.Object, serialParser.Object);

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => checker.GetBirthDate(cpr));
        }

        #endregion

        #region GetGender
        [Theory(DisplayName = "GetGender given valid CPR returns correct gender code")]
        [InlineData("2507933001", 1)]
        [InlineData("2507933002", 0)]
        public void GetGender_given_valid_CPR_returns_correct_gender_code(string cpr, int gender)
        {
            //Arrange
            var trimmer = new Mock<ITrimmer>();
            trimmer.Setup(t => t.Trim(cpr)).Returns(cpr);
            var validator = new Mock<IValidator>();
            validator.Setup(v => v.IsValid(cpr)).Returns(true);
            var serialParser = new Mock<ISerialNumberParser>();
            serialParser.Setup(s => s.Parse(It.IsAny<int>(), It.IsAny<int>())).Returns(1993);
            var checker = new CPRParser(
                trimmer.Object, validator.Object, serialParser.Object);

            //Act
            var result = checker.GetGender(cpr);

            //Assert
            Assert.Equal(gender, result);
        }

        [Fact(DisplayName = "GetGender given empty CPR throws new ArgumentException")]
        public void GetGender_given_empty_CPR_throws_new_ArgumentException()
        {
            //Arrange
            var cpr = string.Empty;
            var trimmer = new Mock<ITrimmer>();
            trimmer.Setup(t => t.Trim(cpr)).Returns(cpr);
            var validator = new Mock<IValidator>();
            validator.Setup(v => v.IsValid(cpr)).Returns(true);
            var serialParser = new Mock<ISerialNumberParser>();
            var checker = new CPRParser(
                trimmer.Object, validator.Object, serialParser.Object);

            //Act
            //Assert
            Assert.Throws<ArgumentException>(() => checker.GetGender(cpr));
        }

        [Fact(DisplayName = "GetGender given null throws new ArgumentNullException")]
        public void GetGender_given_null_throws_new_ArgumentNullException()
        {
            //Arrange
            string cpr = null;
            var trimmer = new Mock<ITrimmer>();
            trimmer.Setup(t => t.Trim(cpr)).Returns(cpr);
            var validator = new Mock<IValidator>();
            validator.Setup(v => v.IsValid(cpr)).Returns(true);
            var serialParser = new Mock<ISerialNumberParser>();
            var checker = new CPRParser(
                trimmer.Object, validator.Object, serialParser.Object);

            //Act
            //Assert
            Assert.Throws<ArgumentNullException>(() => checker.GetGender(cpr));
        }
        #endregion

        #region Integration testing

        [Fact(DisplayName = "Check given valid cpr returns cpr")]
        public void Check_given_valid_cpr_returns_cpr()
        {
            //Arrange
            var cpr = "2507933001";
            var checker = new CPRParser(
                new DynamicsCPRTrimmer(), 
                new DynamicsCPRValidator(new SerialNumberParser()),
                new SerialNumberParser());

            //Act
            var result = checker.Parse(cpr);

            //Assert
            Assert.Equal(cpr, result);
        }

        [Theory(DisplayName = "Check given valid cpr with symbols returns cpr without symbols")]
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
        public void Check_given_valid_cpr_with_symbols_returns_cpr_without_s(string untrimmedCPR, string trimmedCPR)
        {
            //Arrange
            var checker = new CPRParser(
                new DynamicsCPRTrimmer(), 
                new DynamicsCPRValidator( new SerialNumberParser()),
                new SerialNumberParser());

            //Act
            var result = checker.Parse(untrimmedCPR);

            //Assert
            Assert.Equal(trimmedCPR, result);
        }

        [Theory(DisplayName = "Check given invalid cpr returns null")]
        [InlineData("")]
        [InlineData("25079330011")]
        public void Check_given_invalid_cpr_returns_null(string cpr)
        {
            //Arrange
            var checker = new CPRParser(
                new DynamicsCPRTrimmer(),
                new DynamicsCPRValidator(new SerialNumberParser()),
                new SerialNumberParser());

            //Act
            var result = checker.Parse(cpr);

            //Assert
            Assert.Null(result);
        }

        #endregion
    }
}