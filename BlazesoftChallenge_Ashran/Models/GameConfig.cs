using MongoDB.Bson.Serialization.Attributes;

namespace BlazesoftChallenge_Ashran.Models
{
    public class GameConfig
    {
        [BsonId]
        public string Id { get; set; } = "slot-config";

        [BsonElement("width")]
        public int Width { get; set; }

        [BsonElement("height")]
        public int Height { get; set; }
    }
}
