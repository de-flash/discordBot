using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bot.Handlers
{
    public class BanList : ModuleBase<SocketCommandContext>
    {
        [Command("banlist")]
        public async Task BanListAsync()
        {
            await Context.Message.DeleteAsync();
            
            if (!Context.Message.Channel.Id.Equals(Global.SetupId))
            {
                var msg = await Context.Channel.SendMessageAsync
                    ("You can use `!banlist` command only in `setup` channel");
                await Task.Delay(5000);
                await Context.Channel.DeleteMessageAsync(msg);
                return;
            }
            
            var banList = (await File.ReadAllLinesAsync("banlist.txt")).ToList();
            var embed = new EmbedBuilder();
            
            embed.WithTitle("Banlist");
            foreach (var id in banList)
            {
                var user = Context.Guild.GetUser(ulong.Parse(id));
                embed.Description += $"{user}\n";
            }
            var message = await ReplyAsync(embed: embed.Build());
            await Task.Delay(5000);
            await Context.Channel.DeleteMessageAsync(message);
        }
    }
}