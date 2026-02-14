using BlazesoftChallenge_Ashran.Helpers;
using BlazesoftChallenge_Ashran.Models;
using BlazesoftChallenge_Ashran.Repositories;
using BlazesoftChallenge_Ashran.Responses;

namespace BlazesoftChallenge_Ashran.Services
{
    public class GameService : IGameService
    {
        private readonly IGameConfigRepository _configRepo;
        private readonly IPlayerRepository _playerRepo;
        private readonly IWinCalculator _winCalculator;

        public GameService(IGameConfigRepository configRepo,
                           IPlayerRepository playerRepo,
                           IWinCalculator winCalculator)
        {
            _configRepo = configRepo;
            _playerRepo = playerRepo;
            _winCalculator = winCalculator;

        }

        public async Task<GameConfig> GetConfigAsync()
        {
            return await _configRepo.GetConfigAsync();
        }
        public async Task<decimal> AddBalanceAsync(decimal amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero.");

            await _playerRepo.UpdateBalanceAsync("default-player", amount);

            var player = await _playerRepo.GetPlayerAsync("default-player");

            return player.Balance;
        }

        public async Task<SpinResponse> SpinAsync(decimal bet)
        {
            if (bet <= 0)
                throw new ArgumentException("Bet must be greater than zero.");

            var config = await _configRepo.GetConfigAsync();
            if (config == null)
                throw new Exception("Game configuration not found.");

            var matrix = Utilities.GenerateMatrix(config.Width, config.Height);
            decimal win = _winCalculator.CalculateTotalWin(matrix, bet);

            decimal netChange = win - bet;

            var updatedPlayer = await _playerRepo.TryUpdateBalanceAtomicAsync(
                "default-player",
                bet,
                netChange);

            if (updatedPlayer == null)
                throw new InvalidOperationException("Insufficient balance.");

            return new SpinResponse
            {
                ResultMatrix = matrix,
                Win = win,
                Balance = updatedPlayer.Balance
            };
        }
    }
}
