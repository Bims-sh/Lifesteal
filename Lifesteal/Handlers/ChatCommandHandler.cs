using Lifesteal.API;
using Lifesteal.ChatCommands;

namespace Lifesteal.Handlers;

public abstract class ChatCommandHandler
{
    public static bool Run(string message, LifestealPlayer player)
    {
        string chatCommandPrefix = Program.ServerConfiguration.ChatCommandPrefix ?? "!";
        if (!message.StartsWith(chatCommandPrefix)) return true;

        string[] args = message[chatCommandPrefix.Length..].Split(' ');
        string commandName = args[0];

        ChatCommand? command = GetCommandFromName(commandName);
        if (command == null)
        {
            player.Message($"Command {commandName} not found.");
            return false;
        }

        try
        {
            command.Action?.Invoke(args.Skip(1).ToArray(), player);
            Program.Logger.Info($"Command {commandName} with args {string.Join(" ", args)} executed by \"{player.Name}\"");
        }
        catch (Exception e)
        {
            player.Message($"An error occurred while executing command {commandName}");
            Program.Logger.Error($"An error occurred while executing command {commandName} with args {string.Join(" ", args)} by \"{player.Name}\"", e);
            return false;
        }
        
        return false;
    }

    private static ChatCommand? GetCommandFromName(string name)
    {
        return ChatCommandList.Commands.FirstOrDefault(command => command.Name == name);
    }
}