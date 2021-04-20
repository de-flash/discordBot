using Discord;
using Discord.Commands;
using Discord.WebSocket;
using System;
using System.IO;
using System.Threading.Tasks;
using bot.Handlers;

public class Program
{
    public static void Main()
    {
        Bot.MainAsync().GetAwaiter().GetResult();
    }
}