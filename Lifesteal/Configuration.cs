using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;
using BattleBitAPI.Common;

namespace Lifesteal;

public class Configuration
{
    internal class ServerConfiguration
    {
        [JsonIgnore] public IPAddress? IPAddress { get; set; }
        [Required] public string IP { get; set; } = "0.0.0.0";
        [Required] public int Port { get; set; } = 30001;
        [Required] public LogLevel LogLevel { get; set; } = LogLevel.Players | LogLevel.GameServers | LogLevel.GameServerErrors | LogLevel.Sockets;
        [Required] public string ChatCommandPrefix { get; set; } = "!";
        [Required] public string DiscordWebhookURL { get; set; } = "";
        [Required] public string DatabaseName { get; set; } = "LifeStealGunGame";
        [Required] public string MongoDBConnectionString { get; set; } = "mongodb://localhost:27017";
        [Required] public Dictionary<string, string> CollectionNames { get; set; } = new()
        {
            { "PlayerStats", "PlayerStats" }
        };

        [Required] public Dictionary<string, string> PlayerChatPrefixes { get; set; } = new()
        {
            { Program.LifeStealServerRoles.Admin, "<color=#05C3DD>" },
            { Program.LifeStealServerRoles.Moderator, "<color=#05C3DD>" },
            { Program.LifeStealServerRoles.Vip, "<color=#05C3DD>" },
            { Program.LifeStealServerRoles.Special, "<color=#05C3DD>" },
            { Program.LifeStealServerRoles.Default, "<color=#05C3DD>" },
        };

        [Required] public Dictionary<string, string> PlayerChatSuffixes { get; set; } = new()
        {
            { Program.LifeStealServerRoles.Admin, "</color> <color=#FF0000>[Server Admin]</color> <sprite index=8><sprite index=7><sprite index=3>" },
            { Program.LifeStealServerRoles.Moderator, "</color> <color=purple>[Server Mod]</color> <sprite index=0>" },
            { Program.LifeStealServerRoles.Vip, "</color> <color=yellow>[VIP]</color> <sprite index=6>" },
            { Program.LifeStealServerRoles.Special, "</color> <color=green>[Special]</color> <sprite index=4>" },
            { Program.LifeStealServerRoles.Default, "</color>" }
        };
    }
}