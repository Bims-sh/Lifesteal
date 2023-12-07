using Lifesteal.API;
using Lifesteal.Enums;
using log4net;

namespace Lifesteal.ChatCommands;

public class ChatCommand : Attribute
{
    public string Name { get; }
    public string Description { get; }
    public string Usage { get; }
    public PlayerRoles MinimumRequiredRole { get; set; }
    public Action<string[], LifestealPlayer>? Action { get; set; }
    protected LifestealServer Server { get; set; }
    protected  ILog Logger => Program.Logger;
        
    protected ChatCommand(string name, string description, string usage = "", PlayerRoles minimumRequiredRole = PlayerRoles.Default ,Action<string[], LifestealPlayer>? action = null)
    {
        Name = name;
        Description = description;
        Usage = usage;
        Action = action;
        Server = Program.Server;
        MinimumRequiredRole = minimumRequiredRole;
    }
        
    public override string ToString()
    {
        return $"{Name} - {Description}";
    }
    
    public bool CanExecute(LifestealPlayer player)
    {
        return player.PlayerRoles.Any(role => role >= MinimumRequiredRole);
    }
}