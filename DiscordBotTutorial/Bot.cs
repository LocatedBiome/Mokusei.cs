using DiscordBotTutorial.Commands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.VoiceNext;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace DiscordBotTutorial
{
    public class CustomHelpFormatter : DefaultHelpFormatter
    {
        public CustomHelpFormatter(CommandContext ctx) : base(ctx) { }

        public override CommandHelpMessage Build()
        {
            EmbedBuilder.Color = DiscordColor.Goldenrod;
           // EmbedBuilder.Thumbnail.Url = "upload.wikimedia.org/wikipedia/commons/thumb/4/4b/16_wood_samples.jpg/350px-16_wood_samples.jpg";
            return base.Build();
        }
    }

    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }
        public InteractivityExtension Interactivity { get; private set; }
        public VoiceNextExtension Voice { get; private set; }

        public async Task RunAsync()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug
            };

            Client = new DiscordClient(config);

            Client.Ready += Client_Ready;

            Client.UseInteractivity(new InteractivityConfiguration
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            var commandsConfig = new CommandsNextConfiguration
            {
                StringPrefixes = new string[] {configJson.Prefix},
                EnableDms = false,
                EnableMentionPrefix = true,
                DmHelp = false,
                IgnoreExtraArguments = true
            };

            

            Commands = Client.UseCommandsNext(commandsConfig);

            Commands.RegisterCommands<FunCommands>();

            Commands.RegisterCommands<ImportantCommands>();

            Commands.RegisterCommands<VoiceCommands>();

            Commands.RegisterCommands<CommandsFromFile>();

            Commands.RegisterCommands<SuggestedCommands>();
            
            Commands.SetHelpFormatter<CustomHelpFormatter>();

            Voice = Client.UseVoiceNext();

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }
        private Task Client_Ready(DiscordClient sender, ReadyEventArgs e)
        {
            return Task.CompletedTask;
        } 
    }
}
