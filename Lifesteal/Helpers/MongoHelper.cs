using Lifesteal.API;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Lifesteal.Helpers;

public class MongoHelper
{
    public static BsonDocument GetDefaultPlayerStats(LifestealPlayer player)
    {
        var bannedUntil = DateTimeOffset.FromUnixTimeMilliseconds(0).UtcDateTime;
        var formattedBannedUntil = bannedUntil.ToString("yyyy-MM-ddTHH:mm:ss.fffZ");

        var document = new BsonDocument
        {
            { "SteamID", player.SteamID.ToString() },
            { "Name", player.Name },
            { "TotalKills", 0 },
            { "TotalDeaths", 0 },
            { "HighestStreak", 0 },
            { "TotalLevelsCleared", 0 },
            { "TotalHeadShots", 0 },
            { "TotalGamesPlayed", 0 },
            { "HudEnabled", true },
            { "Banned", false },
            { "BannedUntil", formattedBannedUntil },
            {
                "BanReason",
                "Ban expired, you can now join the server again! If you can't join the server, contact @dasischbims on Discord!"
            }
        };

        return document;
    }

    public static IMongoCollection<BsonDocument> GetCollection(string databaseName, string collectionName)
    {
        try
        {
            return new MongoClient(Program.ServerConfiguration.MongoDBConnectionString)
                .GetDatabase(databaseName)
                .GetCollection<BsonDocument>(collectionName);
        }
        catch (MongoException ex)
        {
            Program.Logger.Error($"An error occurred while connecting to MongoDB: {ex.Message}");
            Environment.Exit(1);
        }
        
        return null;
    }

    public static async Task<bool> InsertDataAsync(IMongoCollection<BsonDocument> collection, BsonDocument document, LifestealServer server)
    {
        try
        {
            await collection.InsertOneAsync(document);
            return true;
        }
        catch (MongoException ex)
        {
            Console.WriteLine($"An error occurred while connecting to MongoDB: {ex.Message}");
            server.FailedDataQueue.Enqueue(document);
            return false;
        }
    }

    public static async Task<bool> UpdateDataAsync(IMongoCollection<BsonDocument> collection, BsonDocument document, LifestealServer server)
    {
        try
        {
            var filter = Builders<BsonDocument>.Filter.Eq("SteamID", document["SteamID"]);
            await collection.ReplaceOneAsync(filter, document);
            return true;
        }
        catch (MongoException ex)
        {
            Console.WriteLine($"An error occurred while connecting to MongoDB: {ex.Message}");
            server.FailedDataQueue.Enqueue(document);
            return false;
        }
    }

    private static async Task RetryFailedData(IMongoCollection<BsonDocument> collection, LifestealServer server)
    {
        while (server.FailedDataQueue.Count > 0)
        {
            var data = server.FailedDataQueue.Dequeue();
            var success = await InsertDataAsync(collection, data, server);

            if (!success)
            {
                server.FailedDataQueue.Enqueue(data);
            }
        }
    }

    public static async Task PeriodicMongoRetry(LifestealServer server)
    {
        while (server.IsConnected)
        {
            await RetryFailedData(server.PlayerStatsData, server);
            await Task.Delay(5 * 60 * 1000);
        }
    }
}