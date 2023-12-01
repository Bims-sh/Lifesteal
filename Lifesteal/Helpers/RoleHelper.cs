using Lifesteal.API;

namespace Lifesteal.Helpers;

public class RoleHelper
{
    public static void SetPlayerRoles(LifestealPlayer player, LifestealServer server)
    {
        if (player.SteamID != server.BimsID) return;

        Program.Logger.Info(player.AddPlayerRole(Enums.PlayerRoles.Admin)
            ? $"Successfully added roles for {player.Name} ({player.SteamID})"
            : $"User {player.Name} ({player.SteamID}) already has the Role.");
    }
}