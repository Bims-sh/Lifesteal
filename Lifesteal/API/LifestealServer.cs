using BattleBitAPI.Common;
using BattleBitAPI.Server;
using Lifesteal.Data;
using Lifesteal.Events;
using Lifesteal.Helpers;
using Lifesteal.Structs;
using MongoDB.Bson;
using MongoDB.Driver;
using PlayerStats = BattleBitAPI.Common.PlayerStats;
using ServerSettings = Lifesteal.Events.ServerSettings;

namespace Lifesteal.API;

public class LifestealServer : GameServer<LifestealPlayer>
{
    private readonly Dictionary<ulong, LifestealPlayer> PlayerList = new();
    public IMongoCollection<BsonDocument> PlayerStatsData { get; set; }
    private readonly List<Event> events = new();
    public readonly Queue<BsonDocument> FailedDataQueue = new();
    public List<Loadout> LoadoutList = new();
    public long Visitors { get; set; } = 0;
    public bool UpdateAfterRound { get; set; } = false;
    public ulong BimsID { get; set; } = 76561198395073327;
    public string ServerInfoMessage { get; set; }
    public string ServerDataMessage { get; set; }
    public string ServerLoadingScreenMessage { get; set; }
    public string CurrentMotd { get; set; }
    
    public LifestealServer()
    {
        SetRandomMotd();
        PlayerStatsData = MongoHelper.GetCollection(Program.ServerConfiguration.DatabaseName, Program.ServerConfiguration.CollectionNames["PlayerStats"]);
        
        ServerInfoMessage = InfoTextHelper.GetServerInfoMessage();
        ServerDataMessage = InfoTextHelper.GetServerDataMessage();
        ServerLoadingScreenMessage = InfoTextHelper.GetServerLoadingScreenText(CurrentMotd);

        AddEvent(new DiscordWebhook(), this);
        AddEvent(new LoadingScreenText(), this);
        AddEvent(new ServerSettings(), this);
        AddEvent(new IllegalPlayerActions(), this);
        AddEvent(new ChatRewrite(), this);
        AddEvent(new PlayerRoles(), this);
        AddEvent(new Mongo(), this);
        AddEvent(new Events.PlayerStats(), this);
        AddEvent(new GungameCore(), this);
    }

    public void SetRandomMotd()
    {
        CurrentMotd = MOTD.motd[new Random().Next(0, MOTD.motd.Length)];
    }
    
    public LifestealPlayer GetPlayer(LifestealPlayer player)
    {
        PlayerList.TryAdd(player.SteamID, player);

        return PlayerList[player.SteamID];
    }

    public void AddEvent(Event @event, LifestealServer server)
    {
        @event.Server = server;
        
        events.Add(@event);
    }

    public void RemoveEvent(Event @event)
    {
        if (!events.Contains(@event))
            return;

        events.Remove(@event);
    }

    public override async Task OnConnected()
    {
        foreach (var @event in events)
            await @event.OnConnected();
    }

    public override async Task OnTick()
    {
        foreach (var @event in events)
            await @event.OnTick();
    }

    public override async Task OnDisconnected()
    {
        foreach (var @event in events)
            await @event.OnDisconnected();
    }

    public override async Task OnPlayerConnected(LifestealPlayer player)
    {
        foreach (var @event in events)
            await @event.OnPlayerConnected(player);
    }

    public override async Task OnPlayerDisconnected(LifestealPlayer player)
    {
        foreach (var @event in events)
            await @event.OnPlayerDisconnected(player);
    }

    public override async Task<bool> OnPlayerTypedMessage(LifestealPlayer player, ChatChannel channel, string msg)
    {
        foreach (var @event in events)
            if (!await @event.OnPlayerTypedMessage(player, channel, msg))
                return false;

        return true;
    }

    public override async Task OnPlayerJoiningToServer(ulong steamID, PlayerJoiningArguments args)
    {
        foreach (var @event in events)
            await @event.OnPlayerJoiningToServer(steamID, args);
    }

    public override async Task OnSavePlayerStats(ulong steamID, PlayerStats stats)
    {
        foreach (var @event in events)
            await @event.OnSavePlayerStats(steamID, stats);
    }

    public override async Task<bool> OnPlayerRequestingToChangeRole(LifestealPlayer player, GameRole requestedRole)
    {
        foreach (var @event in events)
            if (!await @event.OnPlayerRequestingToChangeRole(player, requestedRole))
                return false;

        return true;
    }

    public override async Task<bool> OnPlayerRequestingToChangeTeam(LifestealPlayer player, Team requestedTeam)
    {
        foreach (var @event in events)
            if (!await @event.OnPlayerRequestingToChangeTeam(player, requestedTeam))
                return false;

        return true;
    }

    public override async Task OnPlayerChangedRole(LifestealPlayer player, GameRole role)
    {
        foreach (var @event in events)
            await @event.OnPlayerChangedRole(player, role);
    }

    public override async Task OnPlayerJoinedSquad(LifestealPlayer player, Squad<LifestealPlayer> squad)
    {
        foreach (var @event in events)
            await @event.OnPlayerJoinedSquad(player, squad);
    }

    public override async Task OnSquadLeaderChanged(Squad<LifestealPlayer> squad, LifestealPlayer newLeader)
    {
        foreach (var @event in events)
            await @event.OnSquadLeaderChanged(squad, newLeader);
    }

    public override async Task OnPlayerLeftSquad(LifestealPlayer player, Squad<LifestealPlayer> squad)
    {
        foreach (var @event in events)
            await @event.OnPlayerLeftSquad(player, squad);
    }
    
    public override async Task OnPlayerChangeTeam(LifestealPlayer player, Team team)
    {
        foreach (var @event in events)
            await @event.OnPlayerChangeTeam(player, team);
    }
    
    public override async Task OnSquadPointsChanged(Squad<LifestealPlayer> squad, int newPoints)
    {
        foreach (var @event in events)
            await @event.OnSquadPointsChanged(squad, newPoints);
    }
    
    public override async Task<OnPlayerSpawnArguments?> OnPlayerSpawning(LifestealPlayer player, OnPlayerSpawnArguments request)
    {
        foreach (var @event in events)
            await @event.OnPlayerSpawning(player, request);

        return request;
    }
    
    public override async Task OnPlayerSpawned(LifestealPlayer player)
    {
        foreach (var @event in events)
            await @event.OnPlayerSpawned(player);
    }
    
    public override async Task OnPlayerDied(LifestealPlayer player)
    {
        foreach (var @event in events)
            await @event.OnPlayerDied(player);
    }
    
    public override async Task OnPlayerGivenUp(LifestealPlayer player)
    {
        foreach (var @event in events)
            await @event.OnPlayerGivenUp(player);
    }
    
    public override async Task OnAPlayerDownedAnotherPlayer(OnPlayerKillArguments<LifestealPlayer> args)
    {
        foreach (var @event in events)
            await @event.OnAPlayerDownedAnotherPlayer(args);
    }
    
    public override async Task OnAPlayerRevivedAnotherPlayer(LifestealPlayer from, LifestealPlayer to)
    {
        foreach (var @event in events)
            await @event.OnAPlayerRevivedAnotherPlayer(from, to);
    }
    
    public override async Task OnPlayerReported(LifestealPlayer from, LifestealPlayer to, ReportReason reason, string additional)
    {
        foreach (var @event in events)
            await @event.OnPlayerReported(from, to, reason, additional);
    }
    
    public override async Task OnGameStateChanged(GameState oldState, GameState newState)
    {
        foreach (var @event in events)
            await @event.OnGameStateChanged(oldState, newState);
    }
    
    public override async Task OnRoundStarted()
    {
        foreach (var @event in events)
            await @event.OnRoundStarted();
    }
    
    public override async Task OnRoundEnded()
    {
        foreach (var @event in events)
            await @event.OnRoundEnded();
    }
    
    public override async Task OnSessionChanged(long oldSessionID, long newSessionID)
    {
        foreach (var @event in events)
            await @event.OnSessionChanged(oldSessionID, newSessionID);
    }
}