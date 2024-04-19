using BattleBitAPI.Common;
using Lifesteal.API;

namespace Lifesteal.Events;

public class ServerSettings : Event
{
    public override Task OnConnected()
    {
        foreach (var map in Data.ServerSettings.MapRotation)
        {
            Server.MapRotation.AddToRotation(map);
        }
        
        Server.GamemodeRotation.ClearRotation();
        Server.GamemodeRotation.AddToRotation("TDM");
        Server.ServerSettings.PlayerCollision = true;
        Server.ServerSettings.FriendlyFireEnabled = false;
        Server.ServerSettings.CanVoteDay = true;
        Server.ServerSettings.CanVoteNight = false;
        Server.ServerSettings.TeamlessMode = false;
        Server.ServerSettings.UnlockAllAttachments = true;
        
        return Task.CompletedTask;
    }

    public override Task OnTick()
    {
        Task.Run(() =>
        {
            switch (Server.RoundSettings.State)
            {
                case GameState.Playing:
                    Server.RoundSettings.SecondsLeft = 45297;
                    break;
                case GameState.WaitingForPlayers:
                    break;
                case GameState.CountingDown:
                    Server.RoundSettings.SecondsLeft = 15;
                    break;
                case GameState.EndingGame:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        });

        return Task.CompletedTask;
    }
}