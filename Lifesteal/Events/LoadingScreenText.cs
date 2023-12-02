using BattleBitAPI.Common;
using Lifesteal.API;
using Lifesteal.Helpers;

namespace Lifesteal.Events;

public class LoadingScreenText : Event
{
    public override Task OnConnected()
    {
        Server.SetRandomMotd();
        Server.LoadingScreenText = InfoTextHelper.GetServerLoadingScreenText(Server.CurrentMotd);
        
        return Task.CompletedTask;
    }

    public override Task OnPlayerJoiningToServer(ulong steamID, PlayerJoiningArguments args)
    {
        Server.SetRandomMotd();
        Server.LoadingScreenText = InfoTextHelper.GetServerLoadingScreenText(Server.CurrentMotd);
        
        return Task.CompletedTask;
    }

    public override Task OnPlayerConnected(LifestealPlayer player)
    {
        Server.SetRandomMotd();
        Server.LoadingScreenText = InfoTextHelper.GetServerLoadingScreenText(Server.CurrentMotd);
        
        return Task.CompletedTask;
    }
}