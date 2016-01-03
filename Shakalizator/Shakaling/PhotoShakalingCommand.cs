using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shakalizator.TelegramCore;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Shakalizator.Shakaling
{
    public class PhotoShakalingCommand : TelegramMessageHandler<ShakalSession>
    {
        public File[] Files { get; private set; }
        public PhotoShakalingCommand(File[] files)
        {
            Files = files;
        }
        public override TelegramMessageHandler CommandStart(ShakalSession session, Api api, Message message, object state)
        {
            foreach (var file in Files)
            {
                if (file.FileSize > Config.ImageMaxSize)
                {
                    api.SendTextMessage(message.Chat.Id, "Изображение превысило допустимый размер");
                    return session.DefaultHandler;
                }
            }
            api.SendTextMessage(message.Chat.Id, "Отлично, а теперь укажите уровень шакализации (целое число от 0 до 100). Чем больше уровень, тем больше шакалов. Для отмены введите /cancel",
                false,
                0,
                new ReplyKeyboardMarkup
                {
                    Keyboard = new string[][]
                    {
                        new [] {"49" },
                        new [] {"80" },
                        new [] {"90" },
                        new [] { "95", "96", "97", "98", "99", "100" }
                    },
                    OneTimeKeyboard = true
                });
            return this;
        }
        public override TelegramMessageHandler CommandHelp(ShakalSession session, Api api, Message message, object state)
        {
            api.SendTextMessage(message.Chat.Id, "Укажите уровень шакализации (целое число от 0 до 100). Чем больше уровень, тем больше шакалов. Для отмены введите /cancel");
            return this;
        }
        public override TelegramMessageHandler HandleMessage(ShakalSession session, Api api, Message message, object state)
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
