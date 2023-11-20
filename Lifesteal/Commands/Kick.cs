using Lifesteal.API;
using Lifesteal.Types;

namespace Lifesteal.Commands;
    
public class Kick : ConsoleCommand
{
    public Kick() : base("kick", "Kicks a player from the server.")
    {
        Action = args =>
        {
            if (args.Length < 1)
            {
                Logger.Error("You must provide a player name.");
                return;
            }

            string playerName = args[0];
            LifestealPlayer? player = Server.AllPlayers.FirstOrDefault(p => p.Name == playerName);
            
            if (player == null)
            {
                Logger.Error($"Player {playerName} not found.");
                return;
            }
            
            player.Kick();
        };
    }
}