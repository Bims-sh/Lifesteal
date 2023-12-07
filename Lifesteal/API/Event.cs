using BattleBitAPI.Common;
using BattleBitAPI.Server;

namespace Lifesteal.API;

public class Event
{
    public LifestealServer Server { get; set; }
    
    public virtual async Task OnConnected()
    {
    }

    public virtual async Task OnTick()
    {
    }

    public virtual async Task OnDisconnected()
    {
    }

    public virtual async Task OnPlayerConnected(LifestealPlayer player)
    {
    }

    public virtual async Task OnPlayerDisconnected(LifestealPlayer player)
    {
    }

    public virtual async Task<bool> OnPlayerTypedMessage(LifestealPlayer player, ChatChannel channel, string msg)
    {
        return true;
    }

    public virtual async Task OnPlayerJoiningToServer(ulong steamID, PlayerJoiningArguments args)
    {
    }

    public virtual async Task OnSavePlayerStats(ulong steamID, PlayerStats stats)
    {
    }

    public virtual async Task<bool> OnPlayerRequestingToChangeRole(LifestealPlayer player, GameRole requestedRole)
    {
        return true;
    }

    public virtual async Task<bool> OnPlayerRequestingToChangeTeam(LifestealPlayer player, Team requestedTeam)
    {
        return true;
    }

    public virtual async Task OnPlayerChangedRole(LifestealPlayer player, GameRole role)
    {
    }

    public virtual async Task OnPlayerJoinedSquad(LifestealPlayer player, Squad<LifestealPlayer> squad)
    {
    }

    public virtual async Task OnSquadLeaderChanged(Squad<LifestealPlayer> squad, LifestealPlayer newLeader)
    {
    }

    public virtual async Task OnPlayerLeftSquad(LifestealPlayer player, Squad<LifestealPlayer> squad)
    {
    }

    public virtual async Task OnPlayerChangeTeam(LifestealPlayer player, Team team)
    {
    }

    public virtual async Task OnSquadPointsChanged(Squad<LifestealPlayer> squad, int newPoints)
    {
    }

    public virtual async Task<OnPlayerSpawnArguments?> OnPlayerSpawning(LifestealPlayer player, OnPlayerSpawnArguments request)
    {
        return request;
    }

    public virtual async Task OnPlayerSpawned(LifestealPlayer player)
    {
    }

    public virtual async Task OnPlayerDied(LifestealPlayer player)
    {
    }

    public virtual async Task OnPlayerGivenUp(LifestealPlayer player)
    {
    }

    public virtual async Task OnAPlayerDownedAnotherPlayer(OnPlayerKillArguments<LifestealPlayer> args)
    {
    }

    public virtual async Task OnAPlayerRevivedAnotherPlayer(LifestealPlayer from, LifestealPlayer to)
    {
    }

    public virtual async Task OnPlayerReported(LifestealPlayer from, LifestealPlayer to, ReportReason reason,
        string additional)
    {
    }

    public virtual async Task OnGameStateChanged(GameState oldState, GameState newState)
    {
    }

    public virtual async Task OnRoundStarted()
    {
    }

    public virtual async Task OnRoundEnded()
    {
    }

    public virtual async Task OnSessionChanged(long oldSessionID, long newSessionID)
    {
    }
}