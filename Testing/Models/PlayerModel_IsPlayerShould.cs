using System.Xml.Linq;
using UnitTestingMinimalApi.Models;
using UnitTestingMinimalApi.Utils;
using Xunit;

namespace Testing.Services
{
    public class PlayerModel_IsPlayerShould
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
        [InlineData(null)]
        [InlineData("")]
        public void IsValidName_FirstNameIsNullOrEmpty_ReturnNull(string name)
        {
            var mockPlayer = new Player() { FirstName = name };
            Assert.Null(mockPlayer.IsValidName());
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void IsValidName_LastNameIsNullOrEmpty_ReturnNull(string name)
        {
            var mockPlayer = new Player() { LastName = name };
            Assert.Null(mockPlayer.IsValidName());
        }

        [Fact]
        public void GetFullName_FirstNameIsLessThanFour_ReturnNull()
        {
            var mockPlayer = new Player()
            {
                FirstName = "Cha",
                LastName = "Man"
            };
            Assert.Null(mockPlayer.IsValidName());
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
            Assert.Equal(expected, mockPlayer.IsValidName());
        }
    }
}
