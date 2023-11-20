using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text.Json.Serialization;
using BattleBitAPI.Common;

namespace Lifesteal;

public class Configuration
{
    internal class ServerConfiguration
    {
        [Required]
        public string IP { get; set; } = "127.0.0.1";
        
        [Required]
        public int Port { get; set; } = 30000;
        
        [JsonIgnore]
        public IPAddress? IPAddress { get; set; }

        public string ConfigurationPath { get; set; } = "./config";
        public LogLevel LogLevel { get; set; } = LogLevel.Players | LogLevel.GameServers | LogLevel.GameServerErrors | LogLevel.Sockets;
    }
}