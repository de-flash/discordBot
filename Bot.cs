using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;
using bot.Handlers;


namespace bot.Handlers
{
    public class Bot
    {
        public static async Task MainAsync()
        {
            var client = new DiscordSocketClient();
            var commandService = new CommandService();

            // Log information to the console
            client.Log += Log;
            client.ReactionAdded += ReactionAdded.ReactionAddedAsync;

            // Read the token for your bot from file
            var token = File.ReadAllText("token.txt");

            // Log in to Discord
            await client.LoginAsync(TokenType.Bot, token);

            // Start connection logic
            await client.StartAsync();
            
            //Create banlist.txt
            if (!File.Exists("banlist.txt")) File.Create("banlist.txt");

            // Here you can set up your event handlers
            await new CommandHandler(client, commandService).SetupAsync();
            

            // Block this task until the program is closed
            await Task.Delay(-1);
        }

        private static Task Log(LogMessage message)
        {
            Console.WriteLine(message.ToString());
            return Task.CompletedTask;
        }
    }
}