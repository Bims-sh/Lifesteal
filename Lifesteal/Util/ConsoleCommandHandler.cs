using System.Text.RegularExpressions;
using Lifesteal.Commands;
using Lifesteal.Types;

namespace Lifesteal.Util;

public class ConsoleCommandHandler
{
    public void Listen()
    {
        while (true)
        {
            string? command = Console.ReadLine();
            if (command is null)
            {
                Program.Logger.Info("No std in stream available.");
                return;
            }
            
            string[] commandParts = command.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            if (commandParts.Length == 0) continue;
            
            string commandName = commandParts[0];
            
            // run the command, for now just log it
            Program.Logger.Info($"Running command: {command}");
        }
    }
}