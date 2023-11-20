using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using BattleBitAPI.Server;
using Lifesteal.API;
using Lifesteal.Util;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;

namespace Lifesteal;

class Program
{
    public static ILog Logger { get; private set; } = null!;
    private Configuration.ServerConfiguration ServerConfiguration { get; } = new();
    private ServerListener<LifestealPlayer, LifestealServer> ServerListener { get; } = new();
    
    private static void Main()
    {
        Program program = new();
        program.StartAPI();
    }
    
    private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
    {
        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        WriteIndented = true,
        AllowTrailingCommas = true
    };

    private void StartAPI()
    {
        try
        {
            SetupLogger();
            LoadConfiguration();
            ValidateConfiguration();
            PrepareDirectories();
            HookServer();
            StartServerListener();
            StartCommandHandler();
        }
        catch (Exception ex)
        {
            Logger.Error($"Initialization error: {ex}");
            Environment.Exit(-1);
        }
            
        Thread.Sleep(Timeout.Infinite);
    } 

    private void SetupLogger()
    {
        string log4netConfig = "log4net.config";
        if (!File.Exists(log4netConfig))
        {
            File.WriteAllText(log4netConfig, @"<?xml version=""1.0"" encoding=""utf-8"" ?>
<log4net>
    <root>
    <level value=""INFO"" />
    <appender-ref ref=""ManagedColoredConsoleAppender"" />
    <appender-ref ref=""ManagedFileAppender"" /> <!-- New line to reference the file appender -->
    </root>
    <appender name=""ManagedColoredConsoleAppender"" type=""log4net.Appender.ManagedColoredConsoleAppender"">
    <layout type=""log4net.Layout.PatternLayout"">
        <conversionPattern value=""%date [%logger] %level - %message%newline"" />
    </layout>
    <mapping>
        <level value=""WARN"" />
        <foreColor value=""Yellow"" />
    </mapping>
    <mapping>
        <level value=""ERROR"" />
        <foreColor value=""Red"" />
    </mapping>
    </appender>
    <appender name=""ManagedFileAppender"" type=""log4net.Appender.FileAppender"">
    <file value=""logs\LogFile.log"" /> <!-- Adjust the file path as needed -->
    <appendToFile value=""true"" />
    <layout type=""log4net.Layout.PatternLayout"">
        <conversionPattern value=""%date [%logger] %level - %message%newline"" />
    </layout>
    </appender>
</log4net>");
        }

        try
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            XmlConfigurator.Configure(new FileInfo(log4netConfig));
        }
        catch (Exception ex)
        {
            Console.WriteLine("Failed to load log4net.config" + Environment.NewLine + ex);
            throw;
        }

        Logger = LogManager.GetLogger("Lifesteal");
    }

    private void LoadConfiguration()
    {
        if (!File.Exists("appsettings.json"))
        {
            File.WriteAllText("appsettings.json", JsonSerializer.Serialize(ServerConfiguration, JsonOptions));
        }

        new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build()
            .Bind(ServerConfiguration);
    }

    public void ValidateConfiguration()
    {
        List<ValidationResult> validationResults = new();
        IPAddress? ipAddress = null;

        bool isValid = Validator.TryValidateObject(ServerConfiguration, new ValidationContext(ServerConfiguration), validationResults, true)
                       && IPAddress.TryParse(ServerConfiguration.IP, out ipAddress);

        if (!isValid || ipAddress == null)
        {
            var errorMessages = validationResults.Select(x => x.ErrorMessage);
            if (ipAddress == null)
            {
                errorMessages = errorMessages.Append($"Invalid IP address: {ServerConfiguration.IP}");
            }

            string errorString = $"Invalid configuration:{Environment.NewLine}{string.Join(Environment.NewLine, errorMessages)}";
            throw new ValidationException(errorString);
        }
        
        Logger.Info("Configuration is valid.");

        ServerConfiguration.IPAddress = ipAddress;
    }

    private void PrepareDirectories()
    {
        if (!Directory.Exists(ServerConfiguration.ConfigurationPath))
        {
            Directory.CreateDirectory(ServerConfiguration.ConfigurationPath);
        }
    }

    private void StartServerListener()
    {
        Logger.Info("Starting server listener...");
        Logger.Info($"LogLevel: {ServerConfiguration.LogLevel}");
        Logger.Info($"IP: {ServerConfiguration.IPAddress}");
        Logger.Info($"Port: {ServerConfiguration.Port}");
        
        ServerListener.LogLevel = ServerConfiguration.LogLevel;
        ServerListener.OnLog += ServerListener.OnLog;
        ServerListener.Start(ServerConfiguration.IPAddress, ServerConfiguration.Port);

        Logger.Info($"Started server listener on {ServerConfiguration.IPAddress}:{ServerConfiguration.Port}");
    }

    private void HookServer()
    {
        ServerListener.OnCreatingGameServerInstance += InitializeServer;
    }

    private LifestealServer InitializeServer(IPAddress ip, ushort port)
    {
        LifestealServer server = new(ip, port);

        return server;
    }

    private void StartCommandHandler()
    {
        ConsoleCommandHandler commandHandler = new();
        commandHandler.Listen();
    }
}