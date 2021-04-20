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
    public class New : ModuleBase<SocketCommandContext>
    {
        [Command("new")]
        public async Task NewAsync(SocketRole role, string title, string place, DateTime date, DateTime time, int minMember)
        {
            await Context.Message.DeleteAsync();
            if (!Context.Message.Channel.Id.Equals(Global.EventsId))
            {
                var msg = await Context.Channel.SendMessageAsync
                    ("You can use `!new` command only in `events` channel");
                await Task.Delay(5000);
                await Context.Channel.DeleteMessageAsync(msg);
                return;
            }

            if (minMember < 1 || title.Length < 1 || place.Length < 1)
            {
                var invalidInput =
                    await Context.Channel.SendMessageAsync(
                        "You can't create event without place, title or 0 or less people");
                await Task.Delay(5000);
                await Context.Channel.DeleteMessageAsync(invalidInput);
                return;
            }
            
            var eventDetails = 
                $"{role.Name}\n{title}\n{place}\n{date.ToShortDateString()}\n{time.ToShortTimeString()}\n{minMember}";
            await Context.Channel.SendMessageAsync(eventDetails);
            
        }
    }
}