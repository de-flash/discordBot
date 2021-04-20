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
    public class ReactionAdded
    {
        public static async Task ReactionAddedAsync(Cacheable<IUserMessage, ulong> cachedMessage,
            ISocketMessageChannel channel, SocketReaction reaction)
        {
            var message = await cachedMessage.DownloadAsync();
            if (message == null) return;
            if (message.Author.Id != Global.MyBotId 
                || message.Channel.Id != Global.EventsId) return;

            if (!reaction.Emote.Equals(Global.ThumbUp))
            {
                await message.RemoveAllReactionsForEmoteAsync(reaction.Emote);
                return;
            }
            
            var reactionCount = message.Reactions[Global.ThumbUp].ReactionCount;
            var minReactions = int.Parse(message.Content.Split("\n")[5]);
            if (reactionCount >= minReactions)
            {
                //Get announcements channel through guild since I cant use context.
                var guildChannel = channel as SocketGuildChannel;
                var announcements = guildChannel.Guild.GetChannel(Global.AnnouncementsId) as ISocketMessageChannel;
                await announcements.SendMessageAsync(message.Content);
                await message.DeleteAsync();
            }
        }
    }
}