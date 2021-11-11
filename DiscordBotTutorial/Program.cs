using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Exceptions;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using Microsoft.Extensions.Logging;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DiscordBotTutorial
{
    class Program
    {
        public EventId BotEventId { get; private set; }

        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
        private Task Client_Ready(DiscordClient sender, ReadyEventArgs e)
        {
            // let's log the fact that this event occured
            sender.Logger.LogInformation(BotEventId, "Client is ready to process events.");

            // since this method is not async, let's return
            // a completed task, so that no additional work
            // is done
            return Task.CompletedTask;
        }

        private Task Client_GuildAvailable(DiscordClient sender, GuildCreateEventArgs e)
        {
            // let's log the name of the guild that was just
            // sent to our client
            sender.Logger.LogInformation(BotEventId, $"Guild available: {e.Guild.Name}");

            // since this method is not async, let's return
            // a completed task, so that no additional work
            // is done
            return Task.CompletedTask;
        }

        private Task Client_ClientError(DiscordClient sender, ClientErrorEventArgs e)
        {
            // let's log the details of the error that just 
            // occured in our client
            sender.Logger.LogError(BotEventId, e.Exception, "Exception occured");

            // since this method is not async, let's return
            // a completed task, so that no additional work
            // is done
            return Task.CompletedTask;
        }

        private Task Commands_CommandExecuted(CommandsNextExtension sender, CommandExecutionEventArgs e)
        {
            // let's log the name of the command and user
            e.Context.Client.Logger.LogInformation(BotEventId, $"{e.Context.User.Username} successfully executed '{e.Command.QualifiedName}'");

            // since this method is not async, let's return
            // a completed task, so that no additional work
            // is done
            return Task.CompletedTask;
        }

        private async Task Commands_CommandErrored(CommandsNextExtension sender, CommandErrorEventArgs e)
        {
            // let's log the error details
            e.Context.Client.Logger.LogError(BotEventId, $"{e.Context.User.Username} tried executing '{e.Command?.QualifiedName ?? "<unknown command>"}' but it errored: {e.Exception.GetType()}: {e.Exception.Message ?? "<no message>"}", DateTime.Now);

            // let's check if the error is a result of lack
            // of required permissions
            if (e.Exception is ChecksFailedException ex)
            {
                // yes, the user lacks required permissions, 
                // let them know

                var emoji = DiscordEmoji.FromName(e.Context.Client, ":no_entry:");

                // let's wrap the response into an embed
                var embed = new DiscordEmbedBuilder
                {
                    Title = "Access denied",
                    Description = $"{emoji} You do not have the permissions required to execute this command.",
                    Color = new DiscordColor(0xFF0000) // red
                };
                await e.Context.RespondAsync(embed);
            }
            /*/ partial class RPCProgram
             {
                 /// <summary>
                 /// The level of logging to use.
                 /// </summary>
                 private static DiscordRPC.Logging.LogLevel logLevel = DiscordRPC.Logging.LogLevel.Trace;

                 /// <summary>
                 /// The pipe to connect too.
                 /// </summary>
                 private static int discordPipe = -1;

                 /// <summary>
                 /// The current presence to send to discord.
                 /// </summary>
                 private static RichPresence presence = new RichPresence()
                 {
                     Details = "Example Project 🎁",
                     State = "csharp example",
                     Assets = new Assets()
                     {
                         LargeImageKey = "image_large",
                         LargeImageText = "Lachee's Discord IPC Library",
                         SmallImageKey = "image_small"
                     }
                 };

                 /// <summary>
                 /// The discord client
                 /// </summary>
                 private static DiscordRpcClient client;

                 /// <summary>
                 /// Is the main loop currently running?
                 /// </summary>
                 private static bool isRunning = true;

                 /// <summary>
                 /// The string builder for the command
                 /// </summary>
                 private static StringBuilder word = new StringBuilder();


                 //Main Loop
                 static void Main(string[] args)
                 {
                     var bot = new Bot();
                     bot.RunAsync().GetAwaiter().GetResult();
                     //Reads the arguments for the pipe
                     for (int i = 0; i < args.Length; i++)
                     {
                         switch (args[i])
                         {
                             case "-pipe":
                                 discordPipe = int.Parse(args[++i]);
                                 break;

                             default: break;
                         }
                     }

                     //Seting a random details to test the update rate of the presence
                     BasicExample();
                     //FullClientExample();
                     //Issue104();
                     //IssueMultipleSets();
                     //IssueJoinLogic();

                     Console.WriteLine("Press any key to terminate");
                     Console.ReadKey();
                 }

                 static void BasicExample()
                 {
                     // == Create the client
                     var client = new DiscordRpcClient("873208721956806736", pipe: discordPipe)
                     {
                         Logger = new DiscordRPC.Logging.ConsoleLogger(logLevel, true)
                     };

                     // == Subscribe to some events
                     client.OnReady += (sender, msg) =>
                     {
                         //Create some events so we know things are happening
                         Console.WriteLine("Connected to discord with user {0}", msg.User.Username);
                     };

                     client.OnPresenceUpdate += (sender, msg) =>
                     {
                         //The presence has updated
                         Console.WriteLine("Presence has been updated! ");
                     };

                     // == Initialize
                     client.Initialize();

                     // == Set the presence
                     client.SetPresence(new RichPresence()
                     {
                         Details = "A Basic Example",
                         State = "In Game",
                         Timestamps = Timestamps.FromTimeSpan(10),
                         Buttons = new Button[]
                         {
                             new Button() { Label = "Fish", Url = "https://lachee.dev/" }
                         }
                     });

                     // == Do the rest of your program.
                     //Simulated by a Console.ReadKey
                     // etc...
                     Console.ReadKey();

                     // == At the very end we need to dispose of it
                     client.Dispose();
                 }
             }/*/
        }
    }
}
