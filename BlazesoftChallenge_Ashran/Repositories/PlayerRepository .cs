using BlazesoftChallenge_Ashran.Data;
using BlazesoftChallenge_Ashran.Models;
using MongoDB.Driver;

namespace BlazesoftChallenge_Ashran.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly MongoContext _context;

        public PlayerRepository(MongoContext context)
        {
            _context = context;
        }

        public async Task<Player> GetPlayerAsync(string playerId)
        {
            return await _context.Players
                .Find(p => p.Id == playerId)
                .FirstOrDefaultAsync();
        }

        public async Task<Player?> IncrementBalanceAsync(string playerId, decimal amount)
        {
            var update = Builders<Player>.Update.Inc(p => p.Balance, amount);

            return await _context.Players.FindOneAndUpdateAsync(
                p => p.Id == playerId,
                update,
                new FindOneAndUpdateOptions<Player>
                {
                    ReturnDocument = ReturnDocument.After
                });
        }

        public async Task<Player?> TryUpdateBalanceAtomicAsync(string playerId, decimal bet, decimal netChange)
        {
            var filter = Builders<Player>.Filter.And(
                Builders<Player>.Filter.Eq(p => p.Id, playerId),
                Builders<Player>.Filter.Gte(p => p.Balance, bet)
            );

            var update = Builders<Player>.Update
                .Inc(p => p.Balance, netChange);

            return await _context.Players.FindOneAndUpdateAsync(
                filter,
                update,
                new FindOneAndUpdateOptions<Player>
                {
                    ReturnDocument = ReturnDocument.After
                });
        }
    }
}
