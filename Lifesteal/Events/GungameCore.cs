using BattleBitAPI.Common;
using Lifesteal.API;
using Lifesteal.Data;
using Lifesteal.Helpers;

namespace Lifesteal.Events;

public class GungameCore : Event
{
    public override Task OnConnected()
    {
        ListHelper.ShuffleList(Server.LoadoutList);
        GungameHelper.GenerateLoadouts(Server);
        ScoreboardHelper.UpdateScoreboard(Server);
        
        return Task.CompletedTask;
    }

    public override Task OnSessionChanged(long oldSessionID, long newSessionID)
    {
        Server.RoundWon = false;
        ListHelper.ShuffleList(Server.LoadoutList);
        GungameHelper.GenerateLoadouts(Server);
        
        return Task.CompletedTask;
    }

    public override Task OnPlayerConnected(LifestealPlayer player)
    {
        player.ResetPlayer();
        
        ScoreboardHelper.UpdateScoreboard(Server);

        Server.KillsPerLevel = Server.AllPlayers.Count() switch
        {
            <= 24 => 1,
            <= 32 => 2,
            _ => 3
        };
        
        Server.UILogOnServer($"Player {player.Name} connected", 3);
        
        return Task.CompletedTask;
    }

    public override Task OnPlayerDisconnected(LifestealPlayer player)
    {
        player.ResetPlayer();
        
        ScoreboardHelper.UpdateScoreboard(Server);
        
        Server.UILogOnServer($"Player {player.Name} disconnected", 3);
        
        return base.OnPlayerDisconnected(player);
    }

    public override Task OnPlayerSpawned(LifestealPlayer player)
    {
        player.Modifications.JumpHeightMultiplier = 1.25f;
        player.Modifications.RunningSpeedMultiplier =
            player.Level > Items.WeaponList.Count ? 1.5f : 1.2f;
        player.Modifications.FallDamageMultiplier = 0f;
        player.Modifications.CanSpectate = true;
        player.Modifications.ReloadSpeedMultiplier = 1.5f;
        player.Modifications.GiveDamageMultiplier = 1f;
        player.Modifications.RespawnTime = 1;
        player.Modifications.DownTimeGiveUpTime = 5;
        player.Modifications.MinimumDamageToStartBleeding = 100f;
        player.Modifications.MinimumHpToStartBleeding = 0f;
        player.Modifications.HitMarkersEnabled = true;
        player.Modifications.KillFeed = true;
        player.Modifications.AirStrafe = true;
        player.Modifications.CanSuicide = true;
        player.Modifications.StaminaEnabled = false;
        player.Modifications.PointLogHudEnabled = false;
        player.Modifications.SpawningRule = SpawningRule.None;
        player.Modifications.FriendlyHUDEnabled = true;

        ScoreboardHelper.UpdateScoreboard(Server);

        return Task.CompletedTask;
    }

    public override Task<OnPlayerSpawnArguments?> OnPlayerSpawning(LifestealPlayer player, OnPlayerSpawnArguments request)
    {
        var loadout = Server.LoadoutList[player.Level];
        player.UpdateLoadout(loadout);

        request.Loadout.FirstAid = default;
        request.Loadout.Throwable = default;

        player.HasKillStreak = false;
        player.KillsOnCurrentStreak = 0;

        return Task.FromResult<OnPlayerSpawnArguments?>(request);
    }

    public override Task OnAPlayerDownedAnotherPlayer(OnPlayerKillArguments<LifestealPlayer> args)
    {
        var killer = args.Killer;
        var victim = args.Victim;
        
        if (Server.RoundWon) return Task.CompletedTask;

        if (args.Killer == victim)
        {
            victim.Kill();
            victim.Deaths++;
            victim.HasKillStreak = false;
            victim.KillsOnCurrentStreak = 0;
        }
        else
        {
            victim.Deaths++;
            victim.HasKillStreak = false;
            victim.KillsOnCurrentStreak = 0;
            killer.Kills++;
            killer.HasKillStreak = true;
            killer.KillsOnCurrentStreak++;
            killer.Heal(100);

            var autoBalanceTeam = TeamHelper.GetAutoBalanceTeam(Server);
            if (autoBalanceTeam != Team.None)
            {
                victim.ChangeTeam(autoBalanceTeam);
                Program.Logger.Info($"[Teams] {victim.Name} ({victim.SteamID}) was forced into Team {autoBalanceTeam}");
            }

            if (killer.Kills % Server.KillsPerLevel == 0) killer.Level++;

            var loadout = Server.LoadoutList[killer.Level - 1];
            killer.UpdateLoadout(loadout);
        }

        if (killer.Level >= Server.LoadoutList.Count)
        {
            Server.RoundWon = true;
            Server.SayToAllChat($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("Gold")}{killer.Name}{RichTextHelper.Size(15)} has won the game!");
            Server.AnnounceLong($"{RichTextHelper.Sprite("Special")}{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("Black")}{killer.Name}{RichTextHelper.Size(22)} has won the game! {RichTextHelper.Sprite("Special")}");
            
            var top5 = Server.AllPlayers.OrderByDescending(p => p.Kills).Take(5).ToList();
            var topList = top5.Select(topPlayer => new EndGamePlayer<LifestealPlayer>(topPlayer, topPlayer.Kills)).ToList();
            
            Server.ForceEndGame(topList);
            
            foreach (var player in Server.AllPlayers)
            {
                player.ResetPlayer();
            }
        }
        
        if (killer.KillsOnCurrentStreak % 3 == 0)
            Server.UILogOnServer($"{killer.Name} is on a {killer.KillsOnCurrentStreak} kill streak!", 5);
        
        ScoreboardHelper.UpdateScoreboard(Server);

        return Task.CompletedTask;
    }
}