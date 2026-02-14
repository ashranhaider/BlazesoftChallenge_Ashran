using BlazesoftChallenge_Ashran.Data;
using BlazesoftChallenge_Ashran.Models;
using MongoDB.Driver;

namespace BlazesoftChallenge_Ashran.Repositories
{
    public class GameConfigRepository : IGameConfigRepository
    {
        private readonly MongoContext _context;

        public GameConfigRepository(MongoContext context)
        {
            _context = context;
        }

        public async Task<GameConfig> GetConfigAsync()
        {
            return await _context.GameConfigs
                .Find(x => x.Id == "slot-config")
                .FirstOrDefaultAsync();
        }
    }

}
