using MiniProject_MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

class Program
{
    static void Main(string[] args)
    {
        MongoClient client = new MongoClient();
        IMongoDatabase database = client.GetDatabase("theatre");

        var dbNames = client.ListDatabaseNames().ToList();
        Console.WriteLine("Database names:");
        for (var index = 0; index < dbNames.Count; index++)
        {
            Console.WriteLine($"{index + 1}) {dbNames[index]}");
        }

        IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("movies");
        var collectionNames = database.ListCollectionNames().ToList();

        Console.WriteLine("\nSelected DB theatre. Collections list:");
        for (var index = 0; index < collectionNames.Count; index++)
        {
            Console.WriteLine($"{index + 1}) {collectionNames[index]}");
        }

        var documents = collection.Find(new BsonDocument()).ToList();
        Console.WriteLine("\nDocuments from the collection [movies]:");
        for (int index = 0; index < documents.Count; index++)
        {
            var document = documents[index];
            string title = document.GetValue("title", "").AsString;
            string id = document.GetValue("_id", "").ToString();
            string genre = document.GetValue("genre", "").ToString();
            double price = document.TryGetValue("price", out BsonValue priceValue) ? priceValue.AsDouble : 0.0;
            Console.WriteLine($"{index + 1}) Title: {title}, Genre: {genre}, Price: {Math.Round(price, 2)}, ID: [{id}]");
        }

        documents = collection.Find(Filters.Filter1).ToList();
        Console.WriteLine("\nComedy movies longer then 100 minutes:");
        foreach (var document in documents)
        {
            string title = document.GetValue("title").AsString;
            string id = document.GetValue("_id").ToString();
            string duration = document.GetValue("duration").ToString();
            Console.WriteLine($"Title: {title}, Duration: {duration}, ID: [{id}]");
        }

        documents = collection.Find(Filters.Filter2).ToList();
        Console.WriteLine("\nHorror movies made before 2023 and contains letter 'a':");
        foreach (var document in documents)
        {
            string title = document.GetValue("title").AsString;
            string id = document.GetValue("_id").ToString();
            string year = document.GetValue("year").ToString();
            Console.WriteLine($"Title: {title}, Year: {year}, ID: [{id}]");
        }

        documents = collection.Find(Filters.Filter3).ToList();
        Console.WriteLine("\nMovies with Vin Diesel with tickets that costs more than than $20:");
        foreach (var document in documents)
        {
            string title = document.GetValue("title").AsString;
            string id = document.GetValue("_id").ToString();
            double price = document.GetValue("price").AsDouble;
            Console.WriteLine($"Title: {title}, Price: {Math.Round(price, 2)}, ID: [{id}]");
        }

        collection.InsertOne(Documents.Document1);

        #region output
        Console.WriteLine("\nInserted document:");
        Console.WriteLine($"Title: {Documents.Document1["title"]}, Genre: {Documents.Document1["genre"]}, Year: {Documents.Document1["year"]}");
        #endregion

        var documentsInsMany = new List<BsonDocument> { Documents.Document2, Documents.Document3 };
        collection.InsertMany(documentsInsMany);

        #region output
        Console.WriteLine("\nInserted documents:");
        Console.WriteLine($"Title: {Documents.Document2["title"]}, Genre: {Documents.Document2["genre"]}, Year: {Documents.Document2["year"]}");
        Console.WriteLine($"Title: {Documents.Document3["title"]}, Genre: {Documents.Document3["genre"]}, Year: {Documents.Document3["year"]}");
        #endregion

        var updateFilter = Builders<BsonDocument>.Filter.Eq("title", "Umma");
        var update = Builders<BsonDocument>.Update.Set("duration", 93);
        collection.UpdateOne(updateFilter, update);

        #region output
        Console.WriteLine("\nUpdated document:");
        Console.WriteLine($"Before - Title: Umma, Duration: 83");
        Console.WriteLine("After - Title: Umma, Duration: 93");
        #endregion

        var deleteFilter1 = Builders<BsonDocument>.Filter.Eq("genre", "Action and adventure");
        var deleteFilter2 = Builders<BsonDocument>.Filter.Eq("title", "The Conjuring: The Devil Made Me Do It");
        collection.DeleteMany(deleteFilter1);
        collection.DeleteOne(deleteFilter2);

        #region output
        Console.WriteLine("\nDeleted documents:");
        Console.WriteLine($"[Filter] Genre: Action and adventure");
        Console.WriteLine("\nDeleted document:");
        Console.WriteLine($"[Filter] Title: The Conjuring: The Devil Made Me Do It");
        #endregion
    }
}
