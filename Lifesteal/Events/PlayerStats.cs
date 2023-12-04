using BattleBitAPI.Common;
using Lifesteal.API;
using Lifesteal.Helpers;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Lifesteal.Events;

public class PlayerStats : Event
{
    public override async Task OnPlayerConnected(LifestealPlayer player)
        {
            var filter = Builders<BsonDocument>.Filter.Eq("SteamID", player.SteamID.ToString());

            var document = await Server.PlayerStatsData.Find(filter).FirstOrDefaultAsync();
            if (document != null) return;

            var newDocument = MongoHelper.GetDefaultPlayerStats(player);
            await MongoHelper.InsertDataAsync(Server.PlayerStatsData, newDocument, Server);
        }

        public override async Task OnAPlayerDownedAnotherPlayer(OnPlayerKillArguments<LifestealPlayer> args)
        {
            var killerFilter = Builders<BsonDocument>.Filter.Eq("SteamID", args.Killer.SteamID.ToString());
            var killerDocument = await Server.PlayerStatsData.Find(killerFilter).FirstOrDefaultAsync();
            var killerPlayer = args.Killer;

            var victimFilter = Builders<BsonDocument>.Filter.Eq("SteamID", args.Victim.SteamID.ToString());
            var victimDocument = await Server.PlayerStatsData.Find(victimFilter).FirstOrDefaultAsync();
            var victimPlayer = args.Victim;

            if (killerDocument == null)
            {
                killerDocument = MongoHelper.GetDefaultPlayerStats(killerPlayer);
                await MongoHelper.InsertDataAsync(Server.PlayerStatsData, killerDocument, Server);
            }

            if (victimDocument == null)
            {
                victimDocument = MongoHelper.GetDefaultPlayerStats(victimPlayer);
                await MongoHelper.InsertDataAsync(Server.PlayerStatsData, victimDocument, Server);
            }

            // --- update killer stats ---
            killerDocument["Kills"] = killerDocument["Kills"].AsInt32 + 1;
            if (killerDocument["OnStreak"].AsBoolean)
            {
                if (killerDocument["KillsOnStreak"].AsInt32 > killerDocument["HighestStreak"].AsInt32)
                {
                    killerDocument["HighestStreak"] = killerDocument["KillsOnStreak"].AsInt32;
                }

                killerDocument["KillsOnStreak"] = killerDocument["KillsOnStreak"].AsInt32 + 1;
            }
            else
            {
                killerDocument["KillsOnStreak"] = 1;
                killerDocument["OnStreak"] = new BsonBoolean(true);
            }

            // --- update victim stats ---
            victimDocument["Deaths"] = victimDocument["Deaths"].AsInt32 + 1;
            if (victimDocument["OnStreak"].AsBoolean)
            {
                victimDocument["KillsOnStreak"] = 0;
                victimDocument["OnStreak"] = new BsonBoolean(false);
            }
            victimDocument["KillsOnStreak"] = 0;
        }
}