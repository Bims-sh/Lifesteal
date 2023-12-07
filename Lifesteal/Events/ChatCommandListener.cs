using BattleBitAPI.Common;
using Lifesteal.API;
using Lifesteal.Handlers;

namespace Lifesteal.Events;

public class ChatCommandListener : Event
{
    public override async Task<bool> OnPlayerTypedMessage(LifestealPlayer player, ChatChannel channel, string msg)
    {
        var returnValue = await ChatCommandHandler.Run(msg, player);
        return returnValue;
    }
}