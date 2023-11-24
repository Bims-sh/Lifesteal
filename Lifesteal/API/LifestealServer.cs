using BattleBitAPI.Server;

namespace Lifesteal.API;
    
public class LifestealServer : GameServer<LifestealPlayer>
{
    public Dictionary<ulong, LifestealPlayer> PlayerList = new();
}