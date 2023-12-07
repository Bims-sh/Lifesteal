using Lifesteal.API;

namespace Lifesteal.Helpers;

public class MessageHelper
{
    public static void ConsoleToChat(string message, LifestealServer server)
    {
        server.SayToAllChat($"[{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("Green")}Server{RichTextHelper.Color()}{RichTextHelper.Bold(false)}]: {message}");
    }
    
    public static void ToAnnouncement(string message, string length, LifestealServer server)
    {
        switch (length)
        {
            case "l":
                server.AnnounceLong(message);
                break;
            case "s":
                server.AnnounceShort(message);
                break;
            default:
                Program.Logger.Error("Invalid length given in server announcement.");
                break;
        }
    }
    
    public static void ConsoleToChat(string message)
    {
        Program.Server.SayToAllChat($"[{RichTextHelper.Bold(true)}{RichTextHelper.FromColorName("Green")}Server{RichTextHelper.Color()}{RichTextHelper.Bold(false)}]: {message}");
    }
}