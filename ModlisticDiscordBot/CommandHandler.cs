using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Discord.Commands;
using Discord.WebSocket;

namespace ModlisticDiscordBot
{
    class CommandHandler
    {
        private readonly DiscordSocketClient client;
        private readonly CommandService comms;
        private char commandPrefix = '&';
        public CommandHandler(DiscordSocketClient client, CommandService comms)
        {
            this.client = client;
            this.comms = comms;
        }

        public async Task InstallCommandsAsync()
        {
            client.MessageReceived += HandleCommandsAsync;
            await comms.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                      services: null);
        }

        private async Task HandleCommandsAsync(SocketMessage messageParams)
        {
            //If the message was from the system ignore it.
            SocketUserMessage message = messageParams as SocketUserMessage;
            if (message == null) return;

            //Check if the message is a command by ensuring
            int messageArgPos = 0;
            if (message.Author.IsBot || !message.HasCharPrefix(commandPrefix, ref messageArgPos) ||
                message.HasMentionPrefix(client.CurrentUser, ref messageArgPos))
                return;

            //Run the command for precondition checks
            SocketCommandContext cont = new SocketCommandContext(client, message);
            await comms.ExecuteAsync(cont, messageArgPos, null);
        }
    }
}
