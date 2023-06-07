using MongoDB.Bson;

namespace MiniProject_MongoDB
{
    public static class Documents
    {
        public static BsonDocument Document1 = new BsonDocument
        {
            { "title", "Drive" },
            { "genre", "Action" },
            { "year", 2011 },
            { "duration", 100}
        };

        public static BsonDocument Document2 = new BsonDocument
        {
            { "title", "Nightcrawler" },
            { "genre", "Thriller" },
            { "year", 2014 },
            { "duration", 117}
        };

        public static BsonDocument Document3 = new BsonDocument
        {
            { "title", "The Irishman" },
            { "genre", "Drama" },
            { "year", 2019 },
            { "duration", 209}
        };
    }
}
