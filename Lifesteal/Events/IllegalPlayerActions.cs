using BattleBitAPI.Common;
using BattleBitAPI.Server;
using Lifesteal.API;

namespace Lifesteal.Events;

public class IllegalPlayerActions : Event
{
    public override Task<bool> OnPlayerJoinedSquad(LifestealPlayer player, Squad<LifestealPlayer> squad)
    {
        player.KickFromSquad();
        return Task.FromResult(false);
    }

    public override Task<bool> OnPlayerRequestingToChangeRole(LifestealPlayer player, GameRole requestedRole)
    {
        if (requestedRole != GameRole.Assault)
            player.SetNewRole(GameRole.Assault);

        return Task.FromResult(false);
    }
}