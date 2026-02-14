using BlazesoftChallenge_Ashran.Models;

namespace BlazesoftChallenge_Ashran.Repositories
{
    public interface IPlayerRepository
    {
        Task<Player> GetPlayerAsync(string playerId);
        Task UpdateBalanceAsync(string playerId, decimal amount);
        public Task<Player?> TryUpdateBalanceAtomicAsync(string playerId, decimal bet, decimal netChange);
    }
}
