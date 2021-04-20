using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace bot.Handlers
{
    public class Set : ModuleBase<SocketCommandContext>
    {
        [Command("set")]
        public async Task SetAsync(params SocketRole[] roles)
        {
            await Context.Message.DeleteAsync();
            
            if (!Context.Message.Channel.Id.Equals(Global.SetupId))
            {
                var msg = await Context.Channel.SendMessageAsync
                    ("You can use `!set` command only in `setup` channel");
                await Task.Delay(5000);
                await Context.Channel.DeleteMessageAsync(msg);
                return;
            }
            
            var user = Context.Guild.GetUser(Context.Message.Author.Id);
            var assignedRoles = user.Roles;
            
            foreach (var role in assignedRoles)
            {
                if (role.Id == Global.EveryoneId) continue;
                await user.RemoveRoleAsync(role);
            }

            if (roles.Length == 0)
            {
                var guildRoles = Context.Guild.Roles;
                foreach (var role in guildRoles)
                {
                    if (Global.ValidRoles.Contains(role.Name))
                    {
                        await user.AddRoleAsync(role);
                    }
                }
                return;
            }
            foreach (var role in roles)
            {
                await user.AddRoleAsync(role);
            }
        }
    }
}