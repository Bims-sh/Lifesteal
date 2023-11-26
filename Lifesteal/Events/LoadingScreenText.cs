using BattleBitAPI.Common;
using Lifesteal.API;

namespace Lifesteal.Events;

public class LoadingScreenText : Event
{
    public override Task OnConnected()
    {
        Server.LoadingScreenText = Program.ServerConfiguration.LoadingScreenText;
        
        return Task.CompletedTask;
    }
}