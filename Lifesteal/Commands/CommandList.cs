using Lifesteal.Types;

namespace Lifesteal.Commands;

public class CommandList
{
    public static List<ConsoleCommand> Commands { get; } = new()
    {
        new Help(),
        new Kick(),
        new PlayerList(),
        new PlayerStats(),
        new MongoInfo()
    };
}