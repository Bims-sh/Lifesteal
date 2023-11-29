using Lifesteal.API;

namespace Lifesteal.Events;

public class PlayerRoles : Event
{
    public override Task OnPlayerConnected(LifestealPlayer player)
    {
        if (player.SteamID != 76561198395073327) return Task.CompletedTask;

        Program.Logger.Info(player.AddPlayerRole(Enums.PlayerRoles.Admin)
            ? $"Successfully added roles for {player.Name} ({player.SteamID})"
            : $"User {player.Name} ({player.SteamID}) already has the Role.");

        return Task.CompletedTask;
    }
}