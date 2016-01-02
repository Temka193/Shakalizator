using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

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

        public override TelegramMessageHandler HandleMessage(TelegramSession session, Api api, Message message, object state)
        {
            api.SendTextMessage(message.Chat.Id, Text, DisableWebPagePreview, ReplyToMessageId, ReplyMarkup, IsMarkdown);
            return this;
        }
    }
}
