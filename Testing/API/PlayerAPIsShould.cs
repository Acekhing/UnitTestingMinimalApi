using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestingMinimalApi.APIs;
using UnitTestingMinimalApi.Models;
using UnitTestingMinimalApi.Repositories;
using Xunit;

namespace Testing.APIs
{

    /* API Testing Practice using xUnit */
    // The playerAPI depends on PlayerRepository 
    // So we use FakePlayerRepository
    public class PlayerAPIsShould
    {
        private readonly IPlayerRepository _playerRepository;

        public PlayerAPIsShould()
        {
            _playerRepository = new FakePlayerRepository();
        }

        [Fact]
        public async void GetAll_WhenCalledAndPlayerListIsEmpty_ReturnNoContent()
        {
            // Arrange
            var sut = new PlayerAPIs();
            
            // Act
            var actual = await sut.GetAll(_playerRepository);
            
            // Assert
            Assert.IsType<NoContentResult>(actual);
        }

        [Fact]
        public async void GetAll_WhenCalledAndPlayerListIsNotEmpty_ReturnOKObjectResult()
        {

            // Arrange
            Player player = new() 
            {
                FirstName = "Charles",
                LastName = "Manu",
                CountryCode = "Gh",
                Age = 30
            };
            var sut = new PlayerAPIs();

            // Act
            await sut.Post(_playerRepository, player);
            var actual = await sut.GetAll(_playerRepository) as OkObjectResult;

            // Assert
            Assert.NotNull(actual.Value);
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        public async void GetById_PlayerIdIsNullOrEmpty_ReturnNotFoundObjectResult()
        {

            // Arrange
            var sut = new PlayerAPIs();
            Guid guid = Guid.Empty;

            // Act
            var actual = await sut.GetById(_playerRepository, guid);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Fact]
        public async void GetById_WhenCalledAndPlayerDoNotExist_ReturnNotFoundObjectResult()
        {
            // Arrange
            var sut = new PlayerAPIs();
            Guid guid = Guid.NewGuid();

            // Act
            var actual = await sut.GetById(_playerRepository, guid);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actual);
        }

        [Fact]
        public async void GetById_WhenCalledAndPlayerExist_ReturnOkObjectResult()
        {

            // Arrange
            Player player = new()
            {
                FirstName = "Charles",
                LastName = "Manu",
                CountryCode = "Gh",
                Age = 30
            };

            var sut = new PlayerAPIs();
            Guid guid = player.ID;

            // Act
            await sut.Post(_playerRepository, player);
            var actual = await sut.GetById(_playerRepository, guid);

            // Assert
            Assert.IsType<OkObjectResult>(actual);
        }

        [Fact]
        public async void Post_PlayerIsNull_ReturnBadRequestObjectResult()
        {

            // Arrange
            var sut = new PlayerAPIs();
            Player player = null;

            // Act
            var actual = await sut.Post(_playerRepository, player);

            // Assert
            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Fact]
        public async void Post_PlayerIsNotNull_ReturnCreatedAtRouteObjectResult()
        {

            // Arrange
            var sut = new PlayerAPIs();
            Player player = new();

            // Act
            var actual = await sut.Post(_playerRepository, player);

            // Assert
            Assert.IsType<CreatedAtRouteResult>(actual);
        }


        [Fact]
        public async void Delete_PlayerWithIDNotExist_ReturnNotFoundObjectResult()
        {

            // Arrange
            var sut = new PlayerAPIs();
            Guid guid = Guid.NewGuid();

            // Act
            var actual = await sut.Delete(_playerRepository, guid);

            // Assert
            Assert.IsType<NotFoundObjectResult>(actual);
        }

        [Fact]
        public async void Delete_WhenDeleted_ReturnNotFoundObjectResult()
        {

            // Arrange
            Player player = new()
            {
                FirstName = "Charles",
                LastName = "Manu",
                CountryCode = "Gh",
                Age = 30
            };

            var sut = new PlayerAPIs();
            Guid guid = player.ID;

            // Act
            await sut.Post(_playerRepository, player);
            var actual = await sut.Delete(_playerRepository, guid);

            // Assert
            Assert.IsType<NoContentResult>(actual);
        }

    }
}
