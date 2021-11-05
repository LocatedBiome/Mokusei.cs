using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System.Threading.Tasks;

namespace DiscordBotTutorial.Commands
{
    public class ImportantCommands : BaseCommandModule
    {
        [Command("dc-restart")]
        [Description("allows developers to restart the bot.")]
        public async Task Restart(CommandContext ctx)
        {
            if (!ctx.Message.Author.Id.Equals(456594340148674562))
                await ctx.RespondAsync("No");

            else await ctx.RespondAsync("Pls run &beta!");
        }
        [Command("invite"), Description("The bot will send a link to add the bot to your server!")]
        public async Task Invite(CommandContext ctx)
        {

            if (!ctx.Message.Author.Id.Equals(456594340148674562))
                await ctx.RespondAsync("The bot is not public yet! You can ask LocatedReaper for permission, but he might not accept.");

            else await ctx.RespondAsync("<https://discord.com/api/oauth2/authorize?client_id=873208721956806736&permissions=8&scope=bot>");
        }
    }
}