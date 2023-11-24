using BattleBitAPI.Common;
using Lifesteal.API;
using Lifesteal.Enums;
using Lifesteal.Interfaces;

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

        var team = player.Team switch
        {
            Team.TeamA => "US",
            Team.TeamB => "RU",
            _ => string.Empty
        };

        switch (channel)
        {
            case ChatChannel.TeamChat:
                foreach (var teamPlayer in AllPlayers.Where(p => p.Team == player.Team))
                {
                    var colorCodedName = teamPlayer.Team == player.Team
                        ? $"<color={IChatColorChannels.GoodGuys}>{player.Name}</color>"
                        : $"<color={IChatColorChannels.BadGuys}>{player.Name}</color>";

                    var rewrittenMessage = $"{prefix}{player.Name}{suffix} [{team}]: {msg}";

                    SayToChat(rewrittenMessage, teamPlayer);
                }

                return Task.FromResult(false);
            case ChatChannel.AllChat:
                foreach (var teamPlayer in AllPlayers)
                {
                    var colorCodedName = teamPlayer.Team == player.Team
                        ? $"<color={IChatColorChannels.GoodGuys}>{player.Name}</color>"
                        : $"<color={IChatColorChannels.BadGuys}>{player.Name}</color>";

                    var rewrittenMessage = $"{prefix}{player.Name}{suffix}: {msg}";

                    SayToChat(rewrittenMessage, teamPlayer);
                }
                return Task.FromResult(false);
        }
        
        return Task.FromResult(true);
    }
}