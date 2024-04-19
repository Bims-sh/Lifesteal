using Lifesteal.API;

namespace Lifesteal.Helpers;

public class RoleHelper
{
    public static List<ulong> Admins = new()
    {
        76561198395073327,
        76561198173566107
    };

    public static List<ulong> Moderators = new() { };
    
    public static List<ulong> Vips = new() { };
    
    public static List<ulong> Specials = new() { };
    
    public static void SetPlayerRoles(LifestealPlayer player)
    {
        if (Admins.Contains(player.SteamID))
        {
            Program.Logger.Info(player.AddPlayerRole(Enums.PlayerRoles.Admin)
                ? $"Successfully added admin for {player.Name} ({player.SteamID})"
                : $"User {player.Name} ({player.SteamID}) already has the Role.");
        }
        else if (Moderators.Contains(player.SteamID))
        {
            Program.Logger.Info(player.AddPlayerRole(Enums.PlayerRoles.Moderator)
                ? $"Successfully added moderator for {player.Name} ({player.SteamID})"
                : $"User {player.Name} ({player.SteamID}) already has the Role.");
        }
        else if (Vips.Contains(player.SteamID))
        {
            Program.Logger.Info(player.AddPlayerRole(Enums.PlayerRoles.Vip)
                ? $"Successfully added vip for {player.Name} ({player.SteamID})"
                : $"User {player.Name} ({player.SteamID}) already has the Role.");
        }
        else if (Specials.Contains(player.SteamID))
        {
            Program.Logger.Info(player.AddPlayerRole(Enums.PlayerRoles.Special)
                ? $"Successfully added special for {player.Name} ({player.SteamID})"
                : $"User {player.Name} ({player.SteamID}) already has the Role.");
        }
    }
}