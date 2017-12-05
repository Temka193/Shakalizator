using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shakalizator.TelegramCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Shakalizator.Shakaling
{
    public class PhotoShakalingCommand : TelegramMessageHandler<ShakalSession>
    {
        public File[] Files { get; private set; }
        public PhotoShakalingCommand(File[] files)
        {
            Files = files;
        }
        public override TelegramMessageHandler CommandStart(ShakalSession session, TelegramBotClient api, Message message, object state)
        {
            foreach (var file in Files)
            {
                if (file.FileSize > Config.ImageMaxSize)
                {
                    api.SendTextMessageAsync(message.Chat.Id, "Изображение превысило допустимый размер");
                    return session.DefaultHandler;
                }
            }
            api.SendTextMessageAsync(message.Chat.Id, "Отлично, а теперь укажите уровень шакализации (целое число от 0 до 100). Чем больше уровень, тем больше шакалов. Для отмены введите /cancel",
                ParseMode.Default,
                false,
                false,
                0,
                new ReplyKeyboardMarkup
                {
                    Keyboard = new KeyboardButton[][]
                    {
                        new [] {new KeyboardButton("49"),  },
                        new [] {new KeyboardButton("80") },
                        new [] {new KeyboardButton("90") },
                        new [] { new KeyboardButton("95"), new KeyboardButton("96"), new KeyboardButton("97"), new KeyboardButton("98"), new KeyboardButton("99"), new KeyboardButton("100") }
                    },
                    OneTimeKeyboard = true
                });
            return this;
        }
        public override TelegramMessageHandler CommandHelp(ShakalSession session, TelegramBotClient api, Message message, object state)
        {
            api.SendTextMessageAsync(message.Chat.Id, "Укажите уровень шакализации (целое число от 0 до 100). Чем больше уровень, тем больше шакалов. Для отмены введите /cancel");
            return this;
        }
        public override TelegramMessageHandler HandleMessage(ShakalSession session, TelegramBotClient api, Message message, object state)
        {
            if (message.Type == MessageType.TextMessage)
            {
                var text = message.Text.Trim();
                if (text == "/cancel")
                {
                    return session.DefaultHandler.CommandHelp(session, api, message, state);
                }

                var shakalLevel = 0;
                if (int.TryParse(text, out shakalLevel) && shakalLevel >= 0 && shakalLevel <= 100)
                {
                    return new ShakalProcessor(Files, shakalLevel).CommandStart(session, api, message, state);
                }
                else
                {
                    return CommandHelp(session, api, message, state);
                }
            }
            else
            {
                return CommandHelp(session, api, message, state);
            }
        }
    }
}
