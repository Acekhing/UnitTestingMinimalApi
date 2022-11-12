using System.Xml.Linq;
using UnitTestingMinimalApi.Models;
using UnitTestingMinimalApi.Utils;
using Xunit;

namespace Testing.Services
{
    public class PlayerExtensionShould
    {
        [Theory]
        [InlineData(14)]
        [InlineData(15)]
        public void IsEliglePlayer_AgeEqualOrLessThan15_ReturnFalse(int value)
        {
            var mockPlayer = new Player() { Age = value };
            Assert.False(mockPlayer.IsEliglePlayer());
        }

        [Theory]
        [InlineData(20)]
        [InlineData(21)]
        public void IsEliglePlayer_AgeEqualOrGreaterThan20_ReturnFalse(int value)
        {
            var mockPlayer = new Player() { Age = value };
            Assert.False(mockPlayer.IsEliglePlayer());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void IsCitizen_CountryCodeIsNullOrEmpty_ReturnFalse(string code)
        {
            var mockPlayer = new Player() { CountryCode = code };
            Assert.False(mockPlayer.IsCitizen());
        }

        [Fact]
        public void IsCitizen_CountryCodeLengthIsMoreThan2_ReturnFalse()
        {
            var mockPlayer = new Player() { CountryCode = "Ghana" };
            Assert.False(mockPlayer.IsCitizen());
        }


        [Fact]
        public void IsCitizen_CountryCodeValueNotEqualGH_ReturnFalse()
        {
            var mockPlayer = new Player() { CountryCode = "uk" };
            Assert.False(mockPlayer.IsCitizen());
        }

        [Theory]
        [InlineData(null, "Manu")]
        [InlineData("", "Manu")]
        public void GetFullName_FirstNameIsNullOrEmpty_ThrowsArgumentException(string fName, string lName)
        {
            var mockPlayer = new Player() 
            {
                FirstName = fName ,
                LastName = lName
            };
            Assert.Throws<ArgumentException>(paramName:"FirstName", () => mockPlayer.GetFullName());
        }

        [Theory]
        [InlineData("Charles", null)]
        [InlineData("Charles", "")]
        public void GetFullName_LastNameIsNullOrEmpty_ThrowsArgumentException(string fName, string lName)
        {
            var mockPlayer = new Player()
            {
                FirstName = fName,
                LastName = lName
            };
            Assert.Throws<ArgumentException>(paramName: "LastName", () => mockPlayer.GetFullName());
        }

        [Fact]
        public void GetFullName_FirstNameIsLessThanFour_ReturnNull()
        {
            var mockPlayer = new Player()
            {
                FirstName = "Cha",
                LastName = "Man"
            };
            Assert.Throws<FormatException>(() => mockPlayer.GetFullName());
        }

        [Fact]
        public void GetFullName_FirstnameAndLastNameAreNotEmptyAndMoreThan3Characters_ReturnFullName()
        {
            var expected = "Charles Manu";
            var mockPlayer = new Player()
            {
                FirstName = "Charles",
                LastName = "Manu"
            };
            Assert.Equal(expected, mockPlayer.GetFullName());
        }
    }
}
