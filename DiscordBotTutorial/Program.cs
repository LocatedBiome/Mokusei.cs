using System;
using System.Text;
using System.Threading;

namespace DiscordBotTutorial
{
    class Program
    {
        static void Main(string[] args)
        {
            var bot = new Bot();
            bot.RunAsync().GetAwaiter().GetResult();
        }
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
