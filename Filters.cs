using MongoDB.Bson;
using MongoDB.Driver;

namespace MiniProject_MongoDB
{
    public static class Filters
    {
        public static FilterDefinition<BsonDocument> Filter1 = Builders<BsonDocument>.Filter.And(
        Builders<BsonDocument>.Filter.Eq("genre", "Comedy"),
        Builders<BsonDocument>.Filter.Gt("duration", 100)
        );

        static BsonRegularExpression regexFilter = new BsonRegularExpression("a", "i");

        public static FilterDefinition<BsonDocument> Filter2 = Builders<BsonDocument>.Filter.And(
            Builders<BsonDocument>.Filter.Eq("genre", "Horror"),
            Builders<BsonDocument>.Filter.Lt("year", 2023),
            Builders<BsonDocument>.Filter.Regex("title", regexFilter)
        );

        public static FilterDefinition<BsonDocument> Filter3 = Builders<BsonDocument>.Filter.And(
        Builders<BsonDocument>.Filter.Eq("actors", "Vin Diesel"),
        Builders<BsonDocument>.Filter.Gt("price", 20)
        );
    }
}
