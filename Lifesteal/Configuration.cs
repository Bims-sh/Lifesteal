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
        public string IP { get; set; } = "0.0.0.0";
        
        [Required]
        public int Port { get; set; } = 30001;
        
        [JsonIgnore]
        public IPAddress? IPAddress { get; set; }
        public LogLevel LogLevel { get; set; } = LogLevel.Players | LogLevel.GameServers | LogLevel.GameServerErrors | LogLevel.Sockets;
    }
}