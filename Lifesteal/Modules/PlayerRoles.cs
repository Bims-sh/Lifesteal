using Lifesteal.API;

namespace Lifesteal.Modules;

public class PlayerRoles : LifestealServer
{
    public override Task OnPlayerConnected(LifestealPlayer player)
    {
        if (player.SteamID == 76561198395073327) return Task.CompletedTask;
        
        if (player.AddPlayerRole(Enums.PlayerRoles.Admin))
        {
            Program.Logger.Info($"Successfully added roles for {player.Name} ({player.SteamID})");
        }

        return Task.CompletedTask;
    }
}