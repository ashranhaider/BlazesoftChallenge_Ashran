using BlazesoftChallenge_Ashran.Models;
using BlazesoftChallenge_Ashran.Repositories;
using BlazesoftChallenge_Ashran.Services;
using Moq;
using System.Numerics;

namespace BlazesoftChallenge_Ashran.tests
{
    public class GameServiceTests
    {
        [Fact]
        public async Task AddBalance_ShouldIncreaseBalance()
        {
            var playerRepoMock = new Mock<IPlayerRepository>();
            var configRepoMock = new Mock<IGameConfigRepository>();
            var winCalculatorMock = new Mock<IWinCalculator>();

            playerRepoMock
                .Setup(x => x.GetPlayerAsync("default-player"))
                .ReturnsAsync(new Player { Id = "default-player", Balance = 100 });

            var service = new GameService(configRepoMock.Object, playerRepoMock.Object, winCalculatorMock.Object);

            var result = await service.AddBalanceAsync(50);

            playerRepoMock.Verify(x => x.UpdateBalanceAsync("default-player", 50), Times.Once);

            Assert.Equal(100, result);
        }
        [Fact]
        public async Task AddBalance_ShouldThrowErrorOnWrongInput()
        {
            var playerRepoMock = new Mock<IPlayerRepository>();
            var configRepoMock = new Mock<IGameConfigRepository>();
            var winCalculatorMock = new Mock<IWinCalculator>();

            var service = new GameService(configRepoMock.Object, playerRepoMock.Object, winCalculatorMock.Object);

            await Assert.ThrowsAsync<ArgumentException>(() => service.AddBalanceAsync(0));
        }
    }
}
