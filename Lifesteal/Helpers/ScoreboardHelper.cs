using System.Text;
using Lifesteal.API;

namespace Lifesteal.Helpers;

public class ScoreboardHelper
{
    public static void UpdateScoreboard(LifestealServer server)
    {
        var top5 = server.AllPlayers.OrderByDescending(p => p.Kills).Take(5).ToList();
        var separator = new string('-', 35);
        var header = $"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("LawnGreen")}{RichTextHelper.Sprite("Veteran")} Top 5 Players {RichTextHelper.Sprite("Veteran")}";
        
        var top5Score = new StringBuilder();
        foreach (var topPlayer in top5)
        {
            top5Score.AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("Gold")} {top5.IndexOf(topPlayer) + 1}. {RichTextHelper.FromColorName("White")}{topPlayer.Name} {RichTextHelper.FromColorName("Gold")} Level: {RichTextHelper.FromColorName("White")}{topPlayer.Level} {RichTextHelper.FromColorName("Gold")}K/D: {RichTextHelper.FromColorName("White")}{topPlayer.Kd}");
        }

        foreach (var player in server.AllPlayers)
        {
            if (!player.HasHudEnabled || player.HasUsedCommand) continue;
            
            var playerScore = new StringBuilder();
            var currentLevelKills = player.Kills % server.KillsPerLevel;
            var nextPlayerWeapon = string.Empty;

            if (player.Level <= server.LoadoutList.Count)
            {
                nextPlayerWeapon = server.LoadoutList[player.Level].PrimaryWeapon
                                   ?? server.LoadoutList[player.Level].HeavyGadgetName
                                   ?? server.LoadoutList[player.Level].LightGadgetName;
            }
            
            // add separator
            playerScore.AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("LightGoldenrodYellow")}{separator}{RichTextHelper.FromColorName("LightGoldenrodYellow")}");

            // add next weapon
            if (nextPlayerWeapon == string.Empty)
                playerScore.AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("Aquamarine")}Next Weapon: {nextPlayerWeapon}{RichTextHelper.Color()}");
            else
                playerScore.AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("Aquamarine")}Winner winner chicken dinner!{RichTextHelper.Color()}");
            
            // add kills per level if it's more than 1
            if (server.KillsPerLevel > 1)
                playerScore.AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("Aquamarine")}Kills Per Level: {currentLevelKills}/{server.KillsPerLevel}{RichTextHelper.Color()}");
            
            // add player stats
            playerScore.AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("Gold")} Your Stats {RichTextHelper.FromColorName("White")}");
            playerScore.AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("LawnGreen")} Kills: {RichTextHelper.FromColorName("White")}{player.Kills} {RichTextHelper.FromColorName("Red")}Deaths: {RichTextHelper.FromColorName("White")}{player.Deaths}");
            playerScore.AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("Blue")} K/D: {RichTextHelper.FromColorName("White")}{player.Kd} {RichTextHelper.FromColorName("Gold")}Level: {RichTextHelper.FromColorName("White")}{player.Level}");
            
            // send scoreboard
            player.Message(player.IsAlive ? $"{header}{top5Score}{playerScore}" : server.ServerInfoMessage);
        }
    }
}