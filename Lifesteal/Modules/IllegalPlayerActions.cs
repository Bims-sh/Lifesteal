using BattleBitAPI.Common;
using BattleBitAPI.Server;
using Lifesteal.API;

namespace Lifesteal.Modules;

public class IllegalPlayerActions : LifestealServer
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