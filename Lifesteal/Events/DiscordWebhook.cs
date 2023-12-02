using System.Text;
using System.Text.Json;
using BattleBitAPI.Common;
using Lifesteal.API;

namespace Lifesteal.Events;

public class DiscordWebhook : Event
{
    private readonly Queue<DiscordMessage> DiscordMessages = new();
    private readonly HttpClient httpClient = new();

    public override Task OnConnected()
    {
        DiscordMessages.Enqueue(new WarningMessage("Server connected to API"));
        Task.Run(sendChatMessagesToDiscord);
        return Task.CompletedTask;
    }
    
    public override Task OnDisconnected()
    {
        DiscordMessages.Enqueue(new WarningMessage("Server disconnected from API"));
        return base.OnDisconnected();
    }

    public override Task<bool> OnPlayerTypedMessage(LifestealPlayer player, ChatChannel channel, string msg)
    {
        DiscordMessages.Enqueue(new ChatMessage(player.Name, player.SteamID, channel, msg));

        return Task.FromResult(true);
    }

    public override Task OnPlayerReported(LifestealPlayer from, LifestealPlayer to, ReportReason reason, string additional)
    {
        DiscordMessages.Enqueue(new WarningMessage($"{from.Name} ({from.SteamID}) reported {to.Name} ({to.SteamID}) for {reason}:{Environment.NewLine}> {additional}"));
        return Task.CompletedTask;
    }
    
    private async Task sendChatMessagesToDiscord()
    {
        do
        {
            List<DiscordMessage> messages = new();
            do
            {
                try
                {
                    while (DiscordMessages.TryDequeue(out DiscordMessage? message))
                    {
                        messages.Add(message);
                    }


                    if (messages.Count > 0)
                    {
                        await sendWebhookMessage(Program.ServerConfiguration.DiscordWebhookURL, string.Join(Environment.NewLine, messages.Select(message => message.ToString())));
                    }

                    messages.Clear();
                }
                catch (Exception ex)
                {
                    Program.Logger.Error($"Failed to process message queue.", ex);
                    await Task.Delay(500);
                }
            } while (messages.Count > 0);

            await Task.Delay(500);
        } while (Server.IsConnected);
    }

    private async Task sendWebhookMessage(string webhookUrl, string message)
    {
        bool success = false;
        while (!success)
        {
            var payload = new
            {
                content = message
            };

            var payloadJson = JsonSerializer.Serialize(payload);
            var content = new StringContent(payloadJson, Encoding.UTF8, "application/json");

            var response = await this.httpClient.PostAsync(webhookUrl, content);

            if (!response.IsSuccessStatusCode)
            {
                Program.Logger.Error($"Error sending webhook message. Status Code: {response.StatusCode}");
            }

            success = response.IsSuccessStatusCode;
        }
    }
}

internal class DiscordMessage
{
}

internal class ChatMessage : DiscordMessage
{
    private string PlayerName { get; set; }

    public ChatMessage(string playerName, ulong steamID, ChatChannel channel, string message)
    {
        PlayerName = playerName;
        SteamID = steamID;
        Channel = channel;
        Message = message;
    }

    private ulong SteamID { get; set; }
    public ChatChannel Channel { get; set; }
    private string Message { get; set; }

    public override string ToString()
    {
        return $":speech_balloon: [{SteamID}] {PlayerName}: `{Message.Replace("`", "'")}`";
    }
}

internal class WarningMessage : DiscordMessage
{
    public WarningMessage(string message)
    {
        Message = message;
    }

    public bool IsError { get; set; }

    private string Message { get; set; }

    public override string ToString()
    {
        return $":warning: {Message}";
    }
}