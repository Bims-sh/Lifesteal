using BattleBitAPI.Common;
using Lifesteal.API;
using Lifesteal.Enums;

namespace Lifesteal.Modules;

public class ChatRewrite : LifestealServer
{
    public override Task<bool> OnPlayerTypedMessage(LifestealPlayer player, ChatChannel channel, string msg)
    {
        if (msg.StartsWith("!")) return Task.FromResult(false);

        var playerSteamId = player.SteamID;
        PlayerRoles highestRole = player.GetHighestRole();
        var prefix = player.GetPrefixForHighestRole(highestRole);
        var suffix = player.GetSuffixForHighestRole(highestRole);
        
        // TODO: Finish later
        
        return base.OnPlayerTypedMessage(player, channel, msg);
    }
}