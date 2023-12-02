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
            RoleHelper.SetPlayerRoles(player, Server);
        }
        
        return Task.CompletedTask;
    }

    public override Task OnPlayerConnected(LifestealPlayer player)
    {
        RoleHelper.SetPlayerRoles(player, Server);

        return Task.CompletedTask;
    }

    public override Task OnPlayerJoiningToServer(ulong steamID, PlayerJoiningArguments args)
    {
        // TODO: Get roles from database instead of whatever the fuck this is
        if (steamID != Server.BimsID) return Task.CompletedTask;

        args.Stats.Roles = Roles.Admin;
        return Task.CompletedTask;
    }
}