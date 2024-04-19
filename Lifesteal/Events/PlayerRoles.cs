using BattleBitAPI.Common;
using Lifesteal.API;
using Lifesteal.Helpers;

namespace Lifesteal.Events;

public class PlayerRoles : Event
{
    public override Task OnConnected()
    {
        foreach (var player in Server.AllPlayers)
        {
            RoleHelper.SetPlayerRoles(player);
        }
        
        return Task.CompletedTask;
    }

    public override Task OnPlayerConnected(LifestealPlayer player)
    {
        RoleHelper.SetPlayerRoles(player);

        return Task.CompletedTask;
    }

    public override Task OnPlayerJoiningToServer(ulong steamID, PlayerJoiningArguments args)
    {
        if (RoleHelper.Admins.Contains(steamID))
        {
            args.Stats.Roles = Roles.Admin;
        }
        else if (RoleHelper.Moderators.Contains(steamID))
        {
            args.Stats.Roles = Roles.Moderator;
        }
        else if (RoleHelper.Specials.Contains(steamID))
        {
            args.Stats.Roles = Roles.Special;
        }
        else if (RoleHelper.Vips.Contains(steamID))
        {
            args.Stats.Roles = Roles.Vip;
        }
        
        return Task.CompletedTask;
    }
}