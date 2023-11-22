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
            
            ulong playerSteamId;
            try
            {
                playerSteamId = ulong.Parse(args[0]);
            }
            catch (Exception)
            {
                Logger.Error($"Invalid SteamId \"{args[0]}\".");
                return;
            }
            
            LifestealPlayer? player = Server.AllPlayers.FirstOrDefault(p => p.SteamID == playerSteamId);
            
            if (player == null)
            {
                Logger.Error($"Player with SteamId \"{playerSteamId}\" not found.");
                return;
            }
            
            player.Kick();
        };
    }
}