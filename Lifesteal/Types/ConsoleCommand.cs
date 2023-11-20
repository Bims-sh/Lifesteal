using BattleBitAPI.Server;
using Lifesteal.API;
using log4net;

namespace Lifesteal.Types;

public class ConsoleCommand : Attribute
{
    public string Name { get; }
    public string Description { get; }
    public Action<string[]>? Action { get; set; }
    public LifestealServer Server { get; set; }
    public ILog Logger => Program.Logger;
        
    public ConsoleCommand(string name, string description, Action<string[]>? action = null)
    {
        Name = name;
        Description = description;
        Action = action;
        Server = null!;
    }
        
    public override string ToString()
    {
        return $"{Name} - {Description}";
    }
}