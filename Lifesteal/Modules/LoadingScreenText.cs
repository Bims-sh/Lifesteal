using Lifesteal.API;

namespace Lifesteal.Modules;

public class LoadingScreenText : LifestealServer
{
    public override Task OnConnected()
    {
        LoadingScreenText = Program.ServerConfiguration.LoadingScreenText;
        
        return Task.CompletedTask;
    }
}