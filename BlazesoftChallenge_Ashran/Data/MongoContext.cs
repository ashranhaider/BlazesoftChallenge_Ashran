
using BlazesoftChallenge_Ashran.Models;
using MongoDB.Driver;

namespace BlazesoftChallenge_Ashran.Data
{
    public class MongoContext
    {
        private readonly IMongoDatabase _database;

        public MongoContext(IConfiguration configuration)
        {
            var client = new MongoClient(configuration["MongoDb:ConnectionString"]);
            _database = client.GetDatabase(configuration["MongoDb:DatabaseName"]);
        }

        public IMongoCollection<GameConfig> GameConfigs =>
            _database.GetCollection<GameConfig>("gameConfig");

        public IMongoCollection<Player> Players =>
            _database.GetCollection<Player>("players");
    }

}
