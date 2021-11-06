using System.Reflection;
using Discord;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
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
        
        [Command("kick")]
        public async Task KickAsync(CommandContext Context, IGuildUser user)
        {

            var GuildUser = Context.User.Id;

            if (!Context.Message.Author.Id.Equals(456594340148674562))
            {
                await Context.Message.DeleteAsync();
                await Context.RespondAsync(":warning: `No permissions to kick players`");
                return;
            }
            else
            {
                await user.KickAsync();
                await Context.Channel.SendMessageAsync($":eye: `{user.Username} has been kicked from the server`");

                var guild = Context.Guild;
                var channel = guild.GetChannel(609086251978457098); //582790350620327952

                EmbedBuilder builder = new EmbedBuilder();

                builder.WithTitle("Logged Information")
                    .AddField("User", $"{user.Mention}")
                    .AddField("Moderator", $"{Context.User.Username}")
                    .AddField("Other Information", "Can be invited back")
                    .AddField("Command", $"``.kick {user.Username}``")
                    .WithDescription($"This player has been kicked from {Context.Guild.Name} by {Context.User.Username}")

                    .WithCurrentTimestamp()
                    .WithColor(new Color(54, 57, 62));
            }
        }
    }
}