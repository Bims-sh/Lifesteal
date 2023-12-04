using BattleBitAPI.Common;
using Lifesteal.API;
using Lifesteal.Enums;
using Lifesteal.Interfaces;

namespace Lifesteal.Events;

public class ChatRewrite : Event
{
    public override Task<bool> OnPlayerTypedMessage(LifestealPlayer player, ChatChannel channel, string msg)
    {
        if (msg.StartsWith("!")) return Task.FromResult(false);

        Enums.PlayerRoles highestRole = player.GetHighestRole();
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
                foreach (var teamPlayer in Server.AllPlayers.Where(p => p.Team == player.Team))
                {
                    var colorCodedName = teamPlayer.Team == player.Team
                        ? $"<color={IChatColorChannels.GoodGuys}>{player.Name}</color>"
                        : $"<color={IChatColorChannels.BadGuys}>{player.Name}</color>";

                    var rewrittenMessage = $"{prefix}{colorCodedName}{suffix} [{team}]: {msg}";

                    Server.SayToChat(rewrittenMessage, teamPlayer);
                }

                return Task.FromResult(false);
            case ChatChannel.AllChat:
                foreach (var teamPlayer in Server.AllPlayers)
                {
                    var colorCodedName = teamPlayer.Team == player.Team
                        ? $"<color={IChatColorChannels.GoodGuys}>{player.Name}</color>"
                        : $"<color={IChatColorChannels.BadGuys}>{player.Name}</color>";

                    var rewrittenMessage = $"{prefix}{colorCodedName}{suffix}: {msg}";

                    Server.SayToChat(rewrittenMessage, teamPlayer);
                }
                return Task.FromResult(false);
            case ChatChannel.SquadChat:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(channel), channel, null);
        }
        
        return Task.FromResult(true);
    }
}