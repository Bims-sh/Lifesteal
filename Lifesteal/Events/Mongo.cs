using Lifesteal.API;
using Lifesteal.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Lifesteal.Events;

public class Mongo : Event
{
    public override Task OnConnected()
    {
        Task.Run(() => MongoHelper.PeriodicMongoRetry(Server));
        
        GungameHelper.UpdateVisitors(Server);

        return Task.CompletedTask;
    }

    public override async Task OnSessionChanged(long oldSessionID, long newSessionID)
    {
        GungameHelper.UpdateVisitors(Server);
        Program.Logger.Info($"Connected to MongoDB! {Server.Visitors} visitors so far!");
        
        var failedDataQueue = Server.FailedDataQueue;
        if (failedDataQueue.Count > 0)
        {
            Program.Logger.Info($"Attempting to insert {failedDataQueue.Count} failed documents into the database...");
            var failedData = failedDataQueue.ToArray();
            foreach (var document in failedData)
            {
                var success = await MongoHelper.InsertDataAsync(Server.PlayerStatsData, document, Server);
                if (success)
                {
                    failedDataQueue.Dequeue();
                }
            }
        }
    }

    public override Task OnPlayerConnected(LifestealPlayer player)
    {
        var dbFilter = Builders<BsonDocument>.Filter.Eq("SteamID", player.SteamID.ToString());
        var dbDocument = Server.PlayerStatsData.Find(dbFilter).FirstOrDefault();
        if (dbDocument == null)
        {
            var newDocument = MongoHelper.GetDefaultPlayerStats(player);
            Task.Run(() => MongoHelper.InsertDataAsync(Server.PlayerStatsData, newDocument, Server));
        }

        if (dbDocument != null && dbDocument["Banned"].AsBoolean)
        {
            if (DateTime.TryParseExact(dbDocument["BannedUntil"].AsString, "yyyy-MM-ddTHH:mm:ss.fffZ", null,
                    System.Globalization.DateTimeStyles.RoundtripKind, out DateTime banDate))
            {
                if (banDate.CompareTo(DateTime.UtcNow) > 0)
                {
                    var banDateFormatted = FormattingHelper.GetFormattedTime(banDate);
                    var banReason = dbDocument["BanReason"];

                    player.Kick($"You are banned from this server until {banDateFormatted} (UTC)\nReason: {banReason}");
                    return Task.CompletedTask;
                }
            }

            var bannedUntil = DateTimeOffset.FromUnixTimeMilliseconds(0).UtcDateTime;
            var formattedBannedUntil = FormattingHelper.GetFormattedTime(bannedUntil);
            dbDocument["Banned"] = false;
            dbDocument["BannedUntil"] = formattedBannedUntil;
            dbDocument["BanReason"] =
                "Ban expired, you can now join the server again! If you can't join the server, contact @dasischbims on Discord!";

            Task.Run(() => MongoHelper.UpdateDataAsync(Server.PlayerStatsData, dbDocument, Server));
        }

        if (dbDocument != null)
        {
            dbDocument["Name"] = player.Name;
            var hudEnabled = dbDocument["HudEnabled"].AsBoolean;
            var playerConfig = Server.GetPlayer(player);
            playerConfig.HasHudEnabled = hudEnabled;

            Task.Run(() => MongoHelper.UpdateDataAsync(Server.PlayerStatsData, dbDocument, Server));
        }

        Server.UILogOnServer($"{player.Name} joined the server!", 5);

        return Task.CompletedTask;
    }
}