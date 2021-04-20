using System.Linq;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System.IO;

namespace bot.Handlers
{
    public class AddReaction
    {
        public static async Task AddReactionAsync(SocketMessage message)
        {
            var channel = message.Channel as SocketGuildChannel;
            var messageAuthor = (message.Author.Id).ToString();
            var banList = (await File.ReadAllLinesAsync("banlist.txt")).ToList();
            var roles = channel.Guild.GetUser(Global.MyId).Roles;
            var eventType = message.Content.Split("\n")[0];
            foreach (var myRole in roles)
            {
                if (myRole.Name == eventType && myRole.Id != Global.EveryoneId && !banList.Contains(messageAuthor))
                {
                    await message.AddReactionAsync(Global.ThumbUp);
                    return;
                }
            }
        }
    }
}