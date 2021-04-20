using System.Collections.Generic;
using Discord;

namespace bot.Handlers
{
    public static class Global
    {
        public static ulong MyId = 188590220210339841;
        public static ulong EveryoneId = 808406743011819554;
        public static ulong MyBotId = 826769325565149214;
        public static ulong AnnouncementsId = 811291007583911997;
        public static ulong EventsId = 811290895768748053;
        public static ulong SetupId = 811290622001676318;
        public static HashSet<string> ValidRoles = new()
            {"nature", "movie", "pub", "party", "chill", "sport", "food", "games"};

        public static Emoji ThumbUp = new Emoji("👍");
    }
}