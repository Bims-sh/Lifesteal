using BattleBitAPI.Common;
using Lifesteal.API;
using Lifesteal.Handlers;

namespace Lifesteal.Events;

public class ConsoleCommandListener : Event
{
    public override Task<bool> OnPlayerTypedMessage(LifestealPlayer player, ChatChannel channel, string msg)
    {
        var returnValue = ChatCommandHandler.Run(msg, player);
        return Task.FromResult(returnValue);
    }
}