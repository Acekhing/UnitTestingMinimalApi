using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UnitTestingMinimalApi.Controllers;
using UnitTestingMinimalApi.Models;
using UnitTestingMinimalApi.Repositories;
using Xunit;

namespace Testing.Controllers
{
    public class PlayerControllerTest
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly PlayerController _playerController;

        public PlayerControllerTest()
        {
            _playerRepository = new MockPlayerRepository();
            _playerController = new PlayerController(_playerRepository);
        }

        [Fact]
        public async void GetPlayers_ListIsEmpty_ReturnNoContentResult()
        {
            var actual = await _playerController.GetPlayers();
            Assert.IsType<NoContentResult>(actual);
        }

        [Fact]
        public async void GetPlayers_ListNotEmpty_ReturnOkObjectResult()
        {
            Player player = new()
            {
                FirstName = "Charles",
                LastName = "Manu",
                CountryCode = "GH",
                Age = 18
            };

            await _playerController.SignPlayer(player);

            var actual = await _playerController.GetPlayers();

            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        public async void GetPlayerById_PlayerNotExist_ReturnNoFoundResult()
        {
            var id = Guid.NewGuid();
            var actual = await _playerController.GetPlayerById(id);
            Assert.IsType<NotFoundResult>(actual);
        }

        [Fact]
        public async void GetPlayerById_PlayerExist_ReturnOkObjectResult()
        {
            Player player = new()
            {
                FirstName = "Charles",
                LastName = "Manu",
                CountryCode = "GH",
                Age = 18
            };

            var playerId = player.ID;

            await _playerController.SignPlayer(player);

            var actual = await _playerController.GetPlayerById(playerId);

            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        public async void SignPlayer_PlayerIsNull_ReturnBadRequestObjectResult()
        {
            Player player = null;
            var actual = await _playerController.SignPlayer(player);
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
        public async void SignPlayer_IsNotValidName_ReturnUnprocessableEntityObjectResult(string fName, string lName)
        {
            Player player = new()
            {
                FirstName = fName,
                LastName = lName,
                CountryCode = "GH",
                Age = 16
            };
            var actual = await _playerController.SignPlayer(player);
            Assert.IsType<UnprocessableEntityObjectResult>(actual);
        }

        [Theory]
        [InlineData(14)]
        [InlineData(15)]
        [InlineData(20)]
        [InlineData(21)]
        public async void SignPlayer_IsNotEligible_ReturnUnprocessableEntityObjectResult(int age)
        {
            Player player = new()
            {
                FirstName = "Charles",
                LastName = "Manu",
                CountryCode = "GH",
                Age = age
            };
            var actual = await _playerController.SignPlayer(player);
            Assert.IsType<UnprocessableEntityObjectResult>(actual);
        }

        [Theory]
        [InlineData("UK")]
        [InlineData("UN")]
        [InlineData("NG")]
        [InlineData("ZN")]
        public async void SignPlayer_IsNotCitizen_ReturnUnprocessableEntityObjectResult(string code)
        {
            Player player = new()
            {
                FirstName = "Charles",
                LastName = "Manu",
                CountryCode = code,
                Age = 18
            };
            var actual = await _playerController.SignPlayer(player);
            Assert.IsType<UnprocessableEntityObjectResult>(actual);
        }

        [Fact]
        public async void SignPlayer_IsValidNameIsEligibleIsCitizen_ReturnCreatedAtRouteResult()
        {
            Player player = new()
            {
                FirstName = "Charles",
                LastName = "Manu",
                CountryCode = "GH",
                Age = 18
            };
            var actual = await _playerController.SignPlayer(player);
            Assert.IsType<CreatedAtRouteResult>(actual);
        }
    }
}
