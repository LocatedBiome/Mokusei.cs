using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Interactivity.Extensions;
using System.Threading.Tasks;

namespace DiscordBotTutorial.Commands
{
    public class SuggestedCommands : BaseCommandModule
    {


        [Command("rules")]
        public async Task PaginationCommand(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            System.Threading.Thread.Sleep(2000);
            var reallyLongString = "These are the general rules for LocatedBiome." +
                "\n 1. Do not @ mention staff without permission, or without pingable role." +
                "\n 2. Keep all messages pg, (remember, this is a family friendly server.)" +
                "\n 3. Think about what you are saying, don't be an idiot and say something you will regret." +
                "\n 4. Don't advertise without permission from a head admin or higher role." +
                "\n 5. do not share personal info (sorta obvious, but it must be in place.)" +
                "\n " +
                "\n Youtube rank rules." +
                "\n 1. LocatedReaper has to have you friended in discord so he can notify you about rank updates and youtube rank stuff (if he is not friended, Please send him a Friend request!)" +
                "\n 2. You Must have 100 subscribers for rank 1 (If you are already rank 1, I am NOT going to take your rank.)" +
                "\n 3. If you have any questions, DM me NOT any other staff members (I manage the YouTube Rank stuff. No other staff should be expected to know how to operate it)" +
                "\n 4. Dont spam post (If you spam post I will have to remove all embeds from each message sent. If you are spaming and someone else posts, how do you expect people to see theres?!)" +
                "\n 5. Post appropriet content (Any NSFW content at all and you will be removed YouTube Rank.)" +
                "\n 6. Extra stuff (250 is rank 2 which posts channel link every upload, 500: rank 3 which posts video link, 750: rank 4 pings and sends channel link, 1k: rank 5 pings and sends yt video link, 1.5k: rank 6 pings and sends yt video link in #announcements, and if staff member, with 1000 total views post in #staff-announcements if they dont have youtube rank)" +
                "\n Note: These can and will change whenever nessicary.";

            var interactivity = ctx.Client.GetInteractivity();
            var pages = interactivity.GeneratePagesInEmbed(reallyLongString);

            await ctx.Channel.SendPaginatedMessageAsync(ctx.Member, pages);
        }
        [Command("ap")]
        [Description("Grants or Removes Announcement ping role.")]
        public async Task AnnouncementPing(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            System.Threading.Thread.Sleep(500);
            await ctx.RespondAsync("Command still in progress. Please wait.").ConfigureAwait(false);
        }
        [Command("beta"), Description("This command is a placeholder for commands that need testing.")]
        public async Task Beta(CommandContext ctx)
        {

            if (!ctx.Message.Author.Id.Equals(456594340148674562))
                await ctx.RespondAsync("No");

            else
            {
                await ctx.TriggerTypingAsync();
                System.Threading.Thread.Sleep(500);
                await ctx.RespondAsync("This is going to be a restart command. Please wait.");
            }
        }
    }
}
