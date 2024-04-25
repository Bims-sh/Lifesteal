using BattleBitAPI.Common;
using Lifesteal.API;

namespace Lifesteal.Helpers;

public static class TeamHelper
{
    public static Team GetAutoBalanceTeam(LifestealServer server)
    {
        var teamAPlayers = server.AllTeamAPlayers.Count();
        var teamBPlayers = server.AllTeamBPlayers.Count();

        if (teamAPlayers == teamBPlayers)
        {
            return Team.None;
        }

        return teamAPlayers > teamBPlayers ? Team.TeamB : Team.TeamA;
    }
}