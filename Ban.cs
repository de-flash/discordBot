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
    public class Ban : ModuleBase<SocketCommandContext>
    {
        [Command("ban")]
        public async Task BanAsync(SocketGuildUser user)
        {
            await Context.Message.DeleteAsync();
            
            if (!Context.Message.Channel.Id.Equals(Global.SetupId))
            {
                var msg = await Context.Channel.SendMessageAsync
                    ("You can use `!ban` command only in `setup` channel");
                await Task.Delay(5000);
                await Context.Channel.DeleteMessageAsync(msg);
                return;
            }
            
            var banList = (await File.ReadAllLinesAsync("banlist.txt")).ToList();
            if (banList.Contains($"{user.Id}"))
            {
                var msg = await Context.Channel.SendMessageAsync(
                    $"`{user.Username}` is already banned, if you want to unban `{user.Username}`" +
                    $" try `!unban {user.Username}`");
                await Task.Delay(3000); 
                await Context.Channel.DeleteMessageAsync(msg);
                return;
            }
            
            await File.AppendAllTextAsync("banlist.txt", $"{user.Id}\n");
            var message = await Context.Channel.SendMessageAsync($"User `{user.Username}` was added to ban list");
            await Task.Delay(3000); 
            await Context.Channel.DeleteMessageAsync(message);
        }
        
    }
}