using Discord.Commands;
using Discord.WebSocket;
using System.Reflection;
using System.Threading.Tasks;

namespace bot.Handlers
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService commandService;

        // Retrieve client and CommandService instance via constructor
        public CommandHandler(DiscordSocketClient client, CommandService commandService)
        {
            this.client = client;
            this.commandService = commandService;
        }

        public async Task SetupAsync()
        {
            // Hook the MessageReceived event into our command handler
            client.MessageReceived += HandleCommandAsync;

            // Here we discover all of the command modules in the entry assembly and load them
            await commandService.AddModulesAsync(assembly: Assembly.GetEntryAssembly(), services: null);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            // Don't process the command if it was a system message
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            // Create a number to track where the prefix ends and the command begins
            int argPos = 0;

            // Reacts to events
            if (message.Author.IsBot && message.Channel.Id == Global.EventsId)
            {
                await AddReaction.AddReactionAsync(message);
            }

            // Determine if the message is a command based on the prefix and make sure no bots trigger commands
            if (!message.HasCharPrefix('!', ref argPos) ||
                message.Author.IsBot || message.Author.Id != Global.MyId)
                return;

            // Create a WebSocket-based command context based on the message
            var context = new SocketCommandContext(client, message);

            // Execute the command with the command context we just created
            var result = await commandService.ExecuteAsync(context: context, argPos: argPos, services: null);
            
            if (!result.IsSuccess)
            {
                await context.Channel.DeleteMessageAsync(message);
                var msg = await context.Channel.SendMessageAsync(result.ErrorReason);
                await Task.Delay(5000);
                await context.Channel.DeleteMessageAsync(msg);
            }
        }
    }
}
