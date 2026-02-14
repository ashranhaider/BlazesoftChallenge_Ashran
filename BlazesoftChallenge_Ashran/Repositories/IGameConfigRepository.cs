using BlazesoftChallenge_Ashran.Models;

namespace BlazesoftChallenge_Ashran.Repositories
{
    public interface IGameConfigRepository
    {
        public Task<GameConfig> GetConfigAsync();
    }
}
