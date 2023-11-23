using System.Net;
using BattleBitAPI.Server;
using log4net;

namespace Lifesteal.API;
    
public class LifestealServer : GameServer<LifestealPlayer>
{
    ILog Logger { get; } = LogManager.GetLogger(typeof(LifestealServer));
}