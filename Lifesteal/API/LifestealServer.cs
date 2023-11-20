using System.Net;
using BattleBitAPI.Server;
using log4net;

namespace Lifesteal.API;
    
public class LifestealServer : GameServer<LifestealPlayer>
{
    private ILog Logger;

    public LifestealServer(IPAddress ip, ushort port)
    {
        Logger = LogManager.GetLogger($"LifestealServer({ip}:{port})");
        Logger.Debug("Initializing LifestealServer...");
    }
}