using BattleBitAPI.Server;
using Lifesteal.API;
using log4net;

namespace Lifesteal.Types;

public class ConsoleCommand : Attribute
{
    public string Name { get; }
    private string Description { get; }
    public Action<string[]>? Action { get; set; }
    protected LifestealServer Server { get; set; }
    protected  ILog Logger => Program.Logger;
        
    protected ConsoleCommand(string name, string description, Action<string[]>? action = null)
    {
        Name = name;
        Description = description;
        Action = action;
        Server = Program.Server;
    }
        
    public override string ToString()
    {
        return $"{Name} - {Description}";
    }
}