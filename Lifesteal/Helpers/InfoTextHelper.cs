using System.Text;
using Lifesteal.Data;

namespace Lifesteal.Helpers;

public class InfoTextHelper
{
    public static string GetServerInfoMessage()
    {
        var infoMessage = new StringBuilder()
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("MediumVioletRed")}Welcome to Life Steal Gun Game{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("OrangeRed")}---{RichTextHelper.Color()} {RichTextHelper.FromColorName("RoyalBlue")}Server Rules{RichTextHelper.Color()} {RichTextHelper.FromColorName("OrangeRed")}---{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("ForestGreen")}1. {RichTextHelper.Color()}{RichTextHelper.FromColorName("White")}You level up trough a total of {RichTextHelper.FromColorName("Gold")}{Items.WeaponList.Count + Items.GadgetList.Count}{RichTextHelper.Color()}{RichTextHelper.FromColorName("White")} levels{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("ForestGreen")}2. {RichTextHelper.Color()}{RichTextHelper.FromColorName("White")}Each time you reach the kill requirement, you will get a new weapon{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("ForestGreen")}3. {RichTextHelper.Color()}{RichTextHelper.FromColorName("White")}You can see your current weapon and the next weapon in the leaderboard on the right side of your screen{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("ForestGreen")}4. {RichTextHelper.Color()}{RichTextHelper.FromColorName("White")}Your current round stats are also displayed in the leaderboard along with the top 5 players in the current game{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("ForestGreen")}5. {RichTextHelper.Color()}{RichTextHelper.FromColorName("White")}The first player to reach the last level wins the game{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.NewLine()}{RichTextHelper.Bold(true)}{RichTextHelper.Sprite("Creator")} To get more info on data collection type {RichTextHelper.FromColorName("Gold")}!data{RichTextHelper.FromColorName("White")} in chat{RichTextHelper.Color()}");
        
        return infoMessage.ToString();
    }
    
    public static string GetServerDataMessage()
    {
        var dataMessage = new StringBuilder()
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("Blue")}{RichTextHelper.Size(13)}By playing on this server you agree to letting me collect the following data from you:{RichTextHelper.Size()}{RichTextHelper.Color()}{RichTextHelper.NewLine()}")
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("White")}Your SteamID, Name, Kills, Deaths, Highest Kill Streak, Total Gungame Levels cleared, Total Headshot Kills, Total Games Played, Banned Status (If you're banned from this single server), Banned Until Date (If previous applies){RichTextHelper.Color()}{RichTextHelper.NewLine()}")
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("White")}This data is collected to provide a ban system and a server leaderboard which can be viewed on my website if I implement it in the future (Work in progress){RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("White")}If you want your data removed from the database you can run the command {RichTextHelper.FromColorName("Orange")}!removedata{RichTextHelper.Color()}{RichTextHelper.FromColorName("White")} in the chat{RichTextHelper.Color()}");
        
        return dataMessage.ToString();
    }

    public static string GetServerLoadingScreenText(string currentMotd)
    {
        var loadingMessage = new StringBuilder()
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("MediumVioletRed")}{RichTextHelper.Size(50)}Lifesteal Gungame{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("LightCoral")}{RichTextHelper.Size(30)}Made by{RichTextHelper.Color()} {RichTextHelper.FromColorName("LightSkyBlue")}@DasIschBims{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("White")}{RichTextHelper.Size(25)}Discord: {RichTextHelper.Color()}{RichTextHelper.FromColorName("MediumPurple")}https://dsc.gg/bblifesteal{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("White")}{RichTextHelper.Size(20)}Version: {RichTextHelper.Color()}{RichTextHelper.FromColorName("Gold")}1.3.0{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.NewLine()}{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("Orange")}Type {RichTextHelper.FromColorName("Gold")}!help{RichTextHelper.FromColorName("Orange")} in chat to see all available commands{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.NewLine()}")
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("Gold")}{RichTextHelper.Size(20)}{currentMotd}{RichTextHelper.Color()}");
        
        return loadingMessage.ToString();
    }
}