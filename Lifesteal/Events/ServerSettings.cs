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

        Server.ExecuteCommand("set fps 256");

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
                {
                    Server.RoundSettings.SecondsLeft = 69420;
                    Server.RoundSettings.TeamATickets = 69420;
                    Server.RoundSettings.TeamBTickets = 69420;
                    break;
                }
                case GameState.WaitingForPlayers:
                {
                    Server.ForceStartGame();
                    break;
                }
                case GameState.CountingDown:
                    break;
                case GameState.EndingGame:
                    if (Server.UpdateAfterRound)
                    {
                        Task.Run(Server.StopServer);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            if (DateTime.UtcNow.Hour != 8 || DateTime.UtcNow.Minute != 0 || DateTime.UtcNow.Second != 0 || Server.UpdateAfterRound) return;

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("[Server] It's 8:00 UTC, restarting server after the round!");
            Console.ResetColor();

            Server.AnnounceLong("The server will restart after the current round!");
            Server.UpdateAfterRound = true;


            if (!Server.AllPlayers.Any() && Server.InQueuePlayerCount == 0 && Server.UpdateAfterRound)
            {
                Task.Run(Server.StopServer);
            }
        });

        return Task.CompletedTask;
    }
}