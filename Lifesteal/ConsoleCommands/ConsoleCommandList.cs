﻿namespace Lifesteal.ConsoleCommands;

public class ConsoleCommandList
{
    public static List<ConsoleCommand> Commands { get; } = new()
    {
        new Help(),
        new Kick(),
        new PlayerList(),
        new SendMessage(),
        new SendAnnouncement()
    };
}