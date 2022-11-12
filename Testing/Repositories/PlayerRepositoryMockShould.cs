using Moq;
using UnitTestingMinimalApi.Data;
using UnitTestingMinimalApi.Models;
using UnitTestingMinimalApi.Repositories;
using Xunit;

namespace Testing.Repositories
{
    /* Moq Testing Practice for Repository */
    // Here the playerRepository (System under test) depends on DbContext for Data access
    // The DbContext ie: PlayerContext is been MOCKED here

    public class PlayerRepositoryMockShould
    {
        [Fact]
        public async void SaveChanges_WhenCalled_Returns1()
        {
            // Arrange
            var expected = 1;
            var mockPlayerContext = new Mock<IPlayerContext>();
            mockPlayerContext.Setup(c => c.SaveChanges().Result).Returns(1);
            
            var sut = new PlayerRepository(mockPlayerContext.Object);

            // Act
            var actual = await sut.SaveChanges();

            // Arrange
            Assert.Equal(expected, actual);
        }

        [Fact]
        public async void GetAllAsync_WhenCalledAndListIsEmpty_ReturnsEmptyListOfPlayers()
        {
           
            // Arrange
            var mockPlayerContext = new Mock<IPlayerContext>();
            mockPlayerContext.Setup(c => c.Get().Result).Returns(new List<Player>());
            
            var sut = new PlayerRepository(mockPlayerContext.Object);

            //Act 
            var actual = await sut.GetAllAsync();

            // Assert
            Assert.Empty(actual);
        }

        [Fact]
        public async Task GetByIdAsync_ResultIsNull_ReturnsNull()
        {
            // Arrange
            Player player = null;
            var guid = Guid.NewGuid();
            var mockPlayerContext = new Mock<IPlayerContext>();
            var ree = mockPlayerContext.Setup(c => c.Find(guid).Result).Returns(player);
            
            var sut = new PlayerRepository(mockPlayerContext.Object);

            //Act 
            var actual = await sut.GetByIdAsync(guid);

            // Assert
            Assert.Null(actual);
        }

        [Fact]
        public async Task GetByIdAsync_ResultIsNotNull_ReturnsResult()
        {
            // Arrange
            Player player = new() { };
            var mockPlayerContext = new Mock<IPlayerContext>();
            mockPlayerContext.Setup(c => c.Find(player.ID).Result).Returns(player);

            var sut = new PlayerRepository(mockPlayerContext.Object);

            //Act 
            var actual = await sut.GetByIdAsync(player.ID);

            // Assert
            Assert.Equal(player, actual);
        }
    }
}
