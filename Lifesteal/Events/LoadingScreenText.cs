using BattleBitAPI.Common;
using Lifesteal.API;

namespace Lifesteal.Events;

public class LoadingScreenText : Event
{
    public override Task OnConnected()
    {
        Server.SetRandomMotd();
        Server.LoadingScreenText = Server.CurrentMotd;
        
        return Task.CompletedTask;
    }

    public override Task OnPlayerJoiningToServer(ulong steamID, PlayerJoiningArguments args)
    {
        Server.SetRandomMotd();
        Server.LoadingScreenText = Server.CurrentMotd;
        
        return Task.CompletedTask;
    }

    public override Task OnPlayerConnected(LifestealPlayer player)
    {
        Server.SetRandomMotd();
        Server.LoadingScreenText = Server.CurrentMotd;
        
        return Task.CompletedTask;
    }
}