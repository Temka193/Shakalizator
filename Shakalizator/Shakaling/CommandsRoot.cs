﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shakalizator.TelegramCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace Shakalizator.Shakaling
{
    public class CommandsRoot : TelegramMessageHandler<ShakalSession>
    {
        public Dictionary<string, TelegramMessageHandler> Commands { get; private set; }
        public CommandsRoot()
        {
            Commands = new Dictionary<string, TelegramMessageHandler>();
        }

        public override TelegramMessageHandler CommandHelp(ShakalSession session, TelegramBotClient api, Message message, object state)
        {
            session.HelpHandler.HandleMessage(session, api, message, state);
            return this;
        }
        public override TelegramMessageHandler HandleMessage(ShakalSession session, TelegramBotClient api, Message message, object state)
        {
            if (message.Type == MessageType.PhotoMessage)
            {
                return new PhotoShakalingCommand(message.Photo).CommandStart(session, api, message, state);
            }
            if (message.Type == MessageType.DocumentMessage)
            {
                return new PhotoShakalingCommand(new[] { message.Document }).CommandStart(session, api, message, state);
            }

            if (message.Chat.Type == ChatType.Private) return CommandHelp(session, api, message, state);
            else return this;
        }
    }
}
