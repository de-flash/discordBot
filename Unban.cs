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
    public class Unban : ModuleBase<SocketCommandContext>
    {
        [Command("unban")]
        public async Task UnbanAsync(SocketGuildUser user)
        {
            await Context.Message.DeleteAsync();
            
            if (!Context.Message.Channel.Id.Equals(Global.SetupId))
            {
                var msg = await Context.Channel.SendMessageAsync
                    ("You can use `!unban` command only in `setup` channel");
                await Task.Delay(5000);
                await Context.Channel.DeleteMessageAsync(msg);
                return;
            }
            
            var banList = (await File.ReadAllLinesAsync("banlist.txt")).ToList();
            if (!banList.Contains($"{user.Id}"))
            {
                var msg = await Context.Channel.SendMessageAsync(
                    $"{user.Username} is not banned, if you want to ban {user.Username} try `!ban {user.Username}`");
                await Task.Delay(3000); 
                await Context.Channel.DeleteMessageAsync(msg);
                return;
            }

            banList.Remove((user.Id).ToString());
            await File.WriteAllLinesAsync("banlist.txt", banList.ToArray());
            
            var message = await Context.Channel.SendMessageAsync($"User `{user.Username}` was removed from ban list");
            await Task.Delay(3000); 
            await Context.Channel.DeleteMessageAsync(message);
        }
    }
}