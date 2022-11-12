using System.Xml.Linq;
using UnitTestingMinimalApi.Models;
using UnitTestingMinimalApi.Utils;
using Xunit;

namespace Testing.Services
{

    /* Unit Testing Practice using xUnit */
    // Here we testing the extension methods of Player class
    public class PlayerExtensionShould
    {
        [Theory]
        [InlineData(14)]
        [InlineData(15)]
        public void IsEliglePlayer_AgeEqualOrLessThan15_ReturnFalse(int value)
        {
            var stubPlayer = new Player() { Age = value };
            Assert.False(stubPlayer.IsEliglePlayer());
        }

        [Theory]
        [InlineData(20)]
        [InlineData(21)]
        public void IsEliglePlayer_AgeEqualOrGreaterThan20_ReturnFalse(int value)
        {
            var stubPlayer = new Player() { Age = value };
            Assert.False(stubPlayer.IsEliglePlayer());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void IsCitizen_CountryCodeIsNullOrEmpty_ReturnFalse(string code)
        {
            var stubPlayer = new Player() { CountryCode = code };
            Assert.False(stubPlayer.IsCitizen());
        }

        [Fact]
        public void IsCitizen_CountryCodeLengthIsMoreThan2_ReturnFalse()
        {
            var stubPlayer = new Player() { CountryCode = "Ghana" };
            Assert.False(stubPlayer.IsCitizen());
        }


        [Fact]
        public void IsCitizen_CountryCodeValueNotEqualGH_ReturnFalse()
        {
            var stubPlayer = new Player() { CountryCode = "uk" };
            Assert.False(stubPlayer.IsCitizen());
        }

        [Theory]
        [InlineData(null, "Manu")]
        [InlineData("", "Manu")]
        public void GetFullName_FirstNameIsNullOrEmpty_ThrowsArgumentException(string fName, string lName)
        {
            var stubPlayer = new Player() 
            {
                FirstName = fName ,
                LastName = lName
            };
            Assert.Throws<ArgumentException>(paramName:"FirstName", () => stubPlayer.GetFullName());
        }

        [Theory]
        [InlineData("Charles", null)]
        [InlineData("Charles", "")]
        public void GetFullName_LastNameIsNullOrEmpty_ThrowsArgumentException(string fName, string lName)
        {
            var stubPlayer = new Player()
            {
                FirstName = fName,
                LastName = lName
            };
            Assert.Throws<ArgumentException>(paramName: "LastName", () => stubPlayer.GetFullName());
        }

        [Fact]
        public void GetFullName_FirstNameIsLessThanFour_ReturnNull()
        {
            var stubPlayer = new Player()
            {
                FirstName = "Cha",
                LastName = "Man"
            };
            Assert.Throws<FormatException>(() => stubPlayer.GetFullName());
        }

        [Fact]
        public void GetFullName_FirstnameAndLastNameAreNotEmptyAndMoreThan3Characters_ReturnFullName()
        {
            var expected = "Charles Manu";
            var stubPlayer = new Player()
            {
                FirstName = "Charles",
                LastName = "Manu"
            };
            Assert.Equal(expected, stubPlayer.GetFullName());
        }
    }
}
