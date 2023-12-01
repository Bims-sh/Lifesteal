using Lifesteal.API;
using Lifesteal.Helpers;

namespace Lifesteal.Events;

public class GungameCore : Event
{
    public override Task OnConnected()
    {
        ListHelper.ShuffleList(Server.LoadoutList);
        GungameHelper.GenerateLoadouts(Server);
        
        return Task.CompletedTask;
    }

    public override Task OnSessionChanged(long oldSessionID, long newSessionID)
    {
        ListHelper.ShuffleList(Server.LoadoutList);
        GungameHelper.GenerateLoadouts(Server);
        
        return Task.CompletedTask;
    }
}