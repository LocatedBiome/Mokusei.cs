using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace DiscordBotTutorial.Commands
{

    public class CommandsFromFile : BaseCommandModule
    {
        [Command("atestfilecommand")]
        public async Task Command(CommandContext ctx)
        {
            
            using (var fs = new FileStream("ADumbFile.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite))
            {
                var msg = await new DiscordMessageBuilder()
                    .WithContent("Please report to LocatedReaper if you think that this is missing something.")
                    .WithFiles(new Dictionary<string, Stream>() { { "files/ADumbFile1.txt", fs } })
                    .WithReply(ctx.Message.Id)
                    .SendAsync(ctx.Channel);
                //Txtconvert.DeserializeObject<Ticket>(File.ReadAllText($"tickets/{id.ToString()}.json"));
            }
        }
    }
}
