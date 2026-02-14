using BlazesoftChallenge_Ashran.Models;
using BlazesoftChallenge_Ashran.Responses;

namespace BlazesoftChallenge_Ashran.Services
{
    public interface IGameService
    {
        public Task<GameConfig> GetConfigAsync();
        public Task<decimal> AddBalanceAsync(decimal amount);
        public Task<SpinResponse> SpinAsync(decimal bet);
    }
}
