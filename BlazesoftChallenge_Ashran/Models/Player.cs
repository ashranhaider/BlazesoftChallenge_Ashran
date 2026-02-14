using MongoDB.Bson.Serialization.Attributes;

namespace BlazesoftChallenge_Ashran.Models
{
    public class Player
    {
        [BsonId]
        public string Id { get; set; } = "default-player";

        [BsonElement("balance")]
        public decimal Balance { get; set; }
    }
}
