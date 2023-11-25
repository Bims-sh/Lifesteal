using BattleBitAPI;
using Lifesteal.Enums;

namespace Lifesteal.API;
public class LifestealPlayer : Player<LifestealPlayer>
{
    public int Kills = 0;
    public int Deaths = 0;
    public int Level = 1;
    public float Kd => Deaths == 0 ? Kills : (float)Math.Round((float)Kills / Deaths, 2);
    public float KillsOnCurrentStreak = 0;
    public bool HasKillStreak = false;
    public bool HasUsedCommand = false;
    public bool HasHudEnabled = true;
    
    public PlayerRoles[] PlayerRoles = {
        Enums.PlayerRoles.Default
    };

    public bool AddPlayerRole(PlayerRoles playerRole)
    {
        if (PlayerRoles.Contains(playerRole)) return false;
        
        var updatedRoles = new PlayerRoles[PlayerRoles.Length + 1];

        for (var i = 0; i < PlayerRoles.Length; i++)
        {
            updatedRoles[i] = PlayerRoles[i];
        }

        updatedRoles[PlayerRoles.Length] = playerRole;
        PlayerRoles = updatedRoles;

        return true;
    }

    public PlayerRoles GetHighestRole()
    {
        return PlayerRoles.Length == 0 ? Enums.PlayerRoles.Default : PlayerRoles.Max();
    }

    public string GetPrefixForHighestRole(PlayerRoles highestRole)
    {
        return Program.ServerConfiguration.PlayerChatPrefixes.TryGetValue(highestRole.ToString(), out var prefix) ? prefix : Program.LifeStealServerRoles.Default;
    }
    
    public string GetSuffixForHighestRole(PlayerRoles highestRole)
    {
        return Program.ServerConfiguration.PlayerChatSuffixes.TryGetValue(highestRole.ToString(), out var prefix) ? prefix : Program.LifeStealServerRoles.Default;
    }
}