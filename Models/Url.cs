using MongoDB.Bson.Serialization.Attributes;


namespace UrlShortening.Models
{
    public class Url
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id {  get; set; }
        public string OriginalUrl { get; set; }
        public string ShortCode { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }
        [BsonRepresentation(MongoDB.Bson.BsonType.DateTime)]
        public DateTime UpdatedAt { get; set; }

    }
}
