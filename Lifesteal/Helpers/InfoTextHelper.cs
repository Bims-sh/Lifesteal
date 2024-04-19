using System.Text;
using Lifesteal.Data;

namespace Lifesteal.Helpers;

public class InfoTextHelper
{
    public static string GetServerInfoMessage()
    {
        var infoMessage = new StringBuilder()
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.Size(15)}{RichTextHelper.FromColorName("MediumVioletRed")}BattleBit Community Game Night{RichTextHelper.Color()}{RichTextHelper.Size()}")
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("OrangeRed")}---{RichTextHelper.Color()}{RichTextHelper.FromColorName("RoyalBlue")} Server Rules {RichTextHelper.Color()}{RichTextHelper.FromColorName("OrangeRed")}---{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Align("left")}{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("ForestGreen")}1. {RichTextHelper.Color()}{RichTextHelper.FromColorName("White")}You level up trough a total of {RichTextHelper.FromColorName("Gold")}{Items.WeaponList.Count + Items.GadgetList.Count}{RichTextHelper.Color()}{RichTextHelper.FromColorName("White")} levels{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Align("left")}{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("ForestGreen")}2. {RichTextHelper.Color()}{RichTextHelper.FromColorName("White")}Each time you reach the kill requirement, you will get a new weapon{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Align("left")}{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("ForestGreen")}3. {RichTextHelper.Color()}{RichTextHelper.FromColorName("White")}You can see your current weapon and the next weapon in the leaderboard on the right side of your screen{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Align("left")}{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("ForestGreen")}4. {RichTextHelper.Color()}{RichTextHelper.FromColorName("White")}Your current round stats are also displayed in the leaderboard along with the top 5 players in the round{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Align("left")}{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("ForestGreen")}5. {RichTextHelper.Color()}{RichTextHelper.FromColorName("White")}The first player to reach the last level wins the game{RichTextHelper.Color()}{RichTextHelper.Align("center")}");

        return infoMessage.ToString();
    }

    public static string GetServerLoadingScreenText(string currentMotd)
    {
        var loadingMessage = new StringBuilder()
            .AppendLine($"{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("MediumVioletRed")}{RichTextHelper.Size(50)}BattleBit Community Game Night{RichTextHelper.Color()}")
            .AppendLine($"{RichTextHelper.Size(30)}{RichTextHelper.Sprite("Moderator")}{RichTextHelper.Bold(true)} Provided by yours truly, {RichTextHelper.FromColorName("Gold")}Bims{RichTextHelper.Color()}!")
            .AppendLine($"{RichTextHelper.NewLine()}{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("PaleVioletRed")}{RichTextHelper.Size(25)}{currentMotd}{RichTextHelper.Color()}");
        
        return loadingMessage.ToString();
    }
}