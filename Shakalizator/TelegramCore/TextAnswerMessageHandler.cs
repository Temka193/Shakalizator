using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace Shakalizator.TelegramCore
{
    public class TextAnswerMessageHandler : TelegramMessageHandler
    {
        public string Text { get; set; }
        public bool DisableWebPagePreview { get; set; }
        public int ReplyToMessageId { get; set; }
        public ReplyMarkup ReplyMarkup { get; set; }
        public bool IsMarkdown { get; set; }

        public TextAnswerMessageHandler() : this(string.Empty)
        {

        }
        public TextAnswerMessageHandler(string text)
        {
            Text = text;
        }

        public override TelegramMessageHandler HandleMessage(TelegramSession session, TelegramBotClient api, Message message, object state)
        {
            api.SendTextMessageAsync(message.Chat.Id, Text, IsMarkdown ? ParseMode.Markdown : ParseMode.Default, DisableWebPagePreview, false, ReplyToMessageId, ReplyMarkup);
            return this;
        }
    }
}
