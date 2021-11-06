using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using DSharpPlus.Interactivity.Extensions;
using System;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace DiscordBotTutorial.Commands
{
    public class FunCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("Ok, seriously. How don't you know what this does. You run the command &ping and I will simply respond with Pong.")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            System.Threading.Thread.Sleep(200);
            await ctx.RespondAsync("Pong").ConfigureAwait(false);
        }
        [Command("type"), Description("Usage: &type <milliseconds> Description: Makes the bot trigger the typing sequence for a defined amount of time.")]
        public async Task Type(CommandContext ctx, int time)
        {
            await ctx.TriggerTypingAsync();
            System.Threading.Thread.Sleep(time);
        }
        [Command("add")]
        [Description("Does addition for you.")]
        public async Task Add(CommandContext ctx, int numberOne, int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne + numberTwo).ToString())
                .ConfigureAwait(false);
        }
        [Command("subtract")]
        [Description("Does subtraction for you.")]
        public async Task Subtract(CommandContext ctx, int numberOne, int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne - numberTwo).ToString())
                .ConfigureAwait(false);
        }
        [Command("divide")]
        [Description("Does division for you.")]
        public async Task Divide(CommandContext ctx, int numberOne, int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne / numberTwo).ToString())
                .ConfigureAwait(false);
        }
        [Command("multiply")]
        [Description("Does multiplication for you.")]
        public async Task Multiply(CommandContext ctx, int numberOne, int numberTwo)
        {
            await ctx.Channel
                .SendMessageAsync((numberOne - numberTwo).ToString())
                .ConfigureAwait(false);
        }
        [Command("waitforcode"), Description("Waits for a response containing a generated code.")]
        public async Task WaitForCode(CommandContext ctx)
        {
            // first retrieve the interactivity module from the client
            var interactivity = ctx.Client.GetInteractivity();

            // generate a code
            var codebytes = new byte[8];
            using (var rng = RandomNumberGenerator.Create())
                rng.GetBytes(codebytes);

            var code = BitConverter.ToString(codebytes).ToLower().Replace("-", "");

            // announce the code
            await ctx.RespondAsync($"The first one to type the following code gets a reward: `{code}`");

            // wait for anyone who types it
            var msg = await interactivity.WaitForMessageAsync(xm => xm.Content.Contains(code), TimeSpan.FromSeconds(60));
            if (!msg.TimedOut)
            {
                // announce the winner
                await ctx.RespondAsync($"And the winner is: {msg.Result.Author.Mention}");
            }
            else
            {
                await ctx.RespondAsync("Nobody? Really?");
            }
        }
        [Command("mcskin"), Aliases("fullbody", "fullbody/"),Description("Run this command, then in a new message, send a Minecraft users ign to see there skin")]
        public async Task Response(CommandContext ctx)
        {
            await ctx.RespondAsync($"Respond to this message with the Minecraft username of the skin you would like to see");

            var interactivity = ctx.Client.GetInteractivity();

            var message = await interactivity.WaitForMessageAsync(x => x.Channel == ctx.Channel).ConfigureAwait(false);

            await ctx.RespondAsync($"https://minecraftskinstealer.com/api/v1/skin/render/fullbody/{message.Result.Content}/700");
        }

        [Command("quote"), Description("Waits for a reaction, then quotes the message reacted to.")]
        public async Task WaitForReaction(CommandContext ctx)
        {
            // first retrieve the interactivity module from the client
            var interactivity = ctx.Client.GetInteractivity();

            // specify the emoji
            var emoji = DiscordEmoji.FromName(ctx.Client, ":point_up:");

            // announce
            await ctx.RespondAsync($"React with {emoji} to quote a message!");

            // wait for a reaction
            var em = await interactivity.WaitForReactionAsync(xe => xe.Emoji == emoji, ctx.User, TimeSpan.FromSeconds(120));
            if (!em.TimedOut)
            {
                // quote
                var embed = new DiscordEmbedBuilder
                {
                    Color = em.Result.User is DiscordMember m ? m.Color : new DiscordColor(0xFF00FF),
                    Description = em.Result.Message.Content,
                    Author = new DiscordEmbedBuilder.EmbedAuthor
                    {
                        Name = em.Result.User is DiscordMember mx ? mx.DisplayName : em.Result.Message.Author.Username,
                        IconUrl = em.Result.User.AvatarUrl
                    }
                };
                await ctx.RespondAsync(embed);
            }
            else
            {
                await ctx.RespondAsync("Seriously?");
            }
        }
    }
}