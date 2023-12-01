using Lifesteal.Types;

namespace Lifesteal.Commands;
    
public class MongoInfo : ConsoleCommand
{
    public MongoInfo() : base(
        name: "mongo",
        description: "Gets info about the MongoDB connection."
    )
    {
        Action = args =>
        {
            var visitors = Server.Visitors;
            Logger.Info($"Current visitors: {visitors}");
        };
    }
}