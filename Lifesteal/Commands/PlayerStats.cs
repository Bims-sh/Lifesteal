using System.Text;
using Lifesteal.API;
using Lifesteal.Types;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Lifesteal.Commands;

public class PlayerStats : ConsoleCommand
{
    public PlayerStats() : base(
        name: "playerstats",
        description:"Get stats for a player.",
        usage: "playerstats <steamid>"
    )
    {
        Action = args =>
        {
            if (!Server.AllPlayers.Any())
            {
                Logger.Info("No players online.");
                return;
            }
            
            ulong playerSteamId;
            try
            {
                playerSteamId = ulong.Parse(args[0]);
            }
            catch (Exception)
            {
                Logger.Error($"Invalid SteamId \"{args[0]}\".");
                return;
            }
            
            LifestealPlayer? player = Server.AllPlayers.FirstOrDefault(p => p.SteamID == playerSteamId);
            
            if (player == null)
            {
                Logger.Error($"Player with SteamId \"{playerSteamId}\" not found.");
                return;
            }
            
            var dbFilter = Builders<BsonDocument>.Filter.Eq("SteamID", player.SteamID.ToString());
            var dbResult = Server.PlayerStatsData.Find(dbFilter).FirstOrDefault();
            
            if (dbResult == null)
            {
                Logger.Error($"Player with SteamId \"{playerSteamId}\" not found in database.");
                return;
            }
            
            var playerStats = new Dictionary<string, int>();
            playerStats.Add("Kills", dbResult["TotalKills"].AsInt32);
            playerStats.Add("Deaths", dbResult["TotalDeaths"].AsInt32);
            playerStats.Add("HighestStreak", dbResult["HighestStreak"].AsInt32);
            playerStats.Add("TotalLevelsCleared", dbResult["TotalLevelsCleared"].AsInt32);
            playerStats.Add("TotalHeadShots", dbResult["TotalHeadShots"].AsInt32);
            playerStats.Add("TotalGamesPlayed", dbResult["TotalGamesPlayed"].AsInt32);
            
            Logger.Info($"Stats for {player.Name} ({player.SteamID}):");
            foreach (var stat in playerStats)
            {
                Logger.Info($"{stat.Key}: {stat.Value}");
            }
        };
    }
}