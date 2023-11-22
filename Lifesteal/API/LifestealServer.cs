using System.Net;
using BattleBitAPI.Server;
using log4net;

namespace Lifesteal.API;
    
public class LifestealServer : GameServer<LifestealPlayer>
{
    public LifestealServer(IPAddress ip, ushort port)
    {
        var logger = LogManager.GetLogger($"LifestealServer({ip}:{port})");
        logger.Debug("Initializing LifestealServer...");
    }
}