using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitTestingMinimalApi.Controllers;
using UnitTestingMinimalApi.Models;
using UnitTestingMinimalApi.Repositories;
using Xunit;

namespace Testing.Controllers
{

    /* Controller Testing Practice using xUnit */
    // Here the controller depends on the PlayerRepository
    // So we create FakeRepository instance of PlayerRepository for the test
    // Later on the PlayerRepository will be tested using Moq

    public class PlayerControllerShould
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly PlayerController sut;

        public PlayerControllerShould()
        {
            _playerRepository = new FakePlayerRepository();
            sut = new PlayerController(_playerRepository);
        }

        [Fact]
        public async void GetPlayers_ListIsEmpty_ReturnNoContentResult()
        {
            // Act
            var actual = await sut.GetPlayers();

            // Assert
            Assert.IsType<NoContentResult>(actual);
        }

        [Fact]
        public async void GetPlayers_ListNotEmpty_ReturnOkObjectResult()
        {

            // Arrange
            Player player = new()
            {
                FirstName = "Charles",
                LastName = "Manu",
                CountryCode = "GH",
                Age = 18
            };

            // Act
            await sut.SignPlayer(player);
            var actual = await sut.GetPlayers();

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        public async void GetPlayerById_PlayerNotExist_ReturnNoFoundResult()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var actual = await sut.GetPlayerById(id);

            // Assert
            Assert.IsType<NotFoundResult>(actual);
        }

        [Fact]
        public async void GetPlayerById_PlayerExist_ReturnOkObjectResult()
        {

            // Arrange
            Player player = new()
            {
                FirstName = "Charles",
                LastName = "Manu",
                CountryCode = "GH",
                Age = 18
            };
            var playerId = player.ID;

            // Act
            await sut.SignPlayer(player);
            var actual = await sut.GetPlayerById(playerId);

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        public async void SignPlayer_PlayerIsNull_ReturnBadRequestObjectResult()
        {
            // Arrange
            Player player = null;

            // Act
            var actual = await sut.SignPlayer(player);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("", "")]
        [InlineData("Charles", null)]
        [InlineData(null, "Manu")]
        [InlineData("Charles", "")]
        [InlineData("", "Manu")]
        [InlineData("Cha", "Ma")]
        public async void SignPlayer_IsNotValidName_ReturnBadRequestObjectResult(string fName, string lName)
        {

            // Arrange
            Player player = new()
            {
                FirstName = fName,
                LastName = lName,
                CountryCode = "GH",
                Age = 16
            };

            // Act
            var actual = await sut.SignPlayer(player);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Theory]
        [InlineData(14)]
        [InlineData(15)]
        [InlineData(20)]
        [InlineData(21)]
        public async void SignPlayer_IsNotEligible_ReturnBadRequestObjectResult(int age)
        {

            // Arrange
            Player player = new()
            {
                FirstName = "Charles",
                LastName = "Manu",
                CountryCode = "GH",
                Age = age
            };

            // Act
            var actual = await sut.SignPlayer(player);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Theory]
        [InlineData("UK")]
        [InlineData("UN")]
        [InlineData("NG")]
        [InlineData("ZN")]
        public async void SignPlayer_IsNotCitizen_ReturnBadRequestObjectResult(string code)
        {

            // Arrange
            Player player = new()
            {
                FirstName = "Charles",
                LastName = "Manu",
                CountryCode = code,
                Age = 18
            };

            // Act
            var actual = await sut.SignPlayer(player);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Fact]
        public async void SignPlayer_IsValidNameIsEligibleIsCitizen_ReturnCreatedAtRouteResult()
        {

            // Arrange
            Player player = new()
            {
                FirstName = "Charles",
                LastName = "Manu",
                CountryCode = "GH",
                Age = 18
            };

            // Act
            var actual = await sut.SignPlayer(player);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(actual);
        }
    }
}
