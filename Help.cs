using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Threading.Tasks;

namespace bot.Handlers
{
    public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task HelpAsync()
        {
            await Context.Message.DeleteAsync();
            var embed = new EmbedBuilder
            {
                Title = "DefinitelyNotFlash commands"
            };
            embed.AddField("`!ping`", "Pings your bot");
            embed.AddField("`!set` role1 role2...", "Sets roles you choose to the owner of the bot.\n" +
                                                    "Sets all roles if without arguments");
            embed.AddField("`!ban` user",
                "Adds user to ban list. Your bot will not react to events created by the user");
            embed.AddField("`!unban` user", "Removes user from the ban list");
            embed.AddField("`!banlist`", "Shows banned users");
            embed.AddField("`!new` Type Title Place Date Time minMembers", "Creates new event");

            var message = await ReplyAsync(embed: embed.Build());
            await Task.Delay(10000);
            await message.Channel.DeleteMessageAsync(message);
        }
    }
}