using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Shakalizator.TelegramCore
{
    public abstract class TelegramSession
    {
        private TelegramMessageHandler currentHandler;
        public virtual TelegramLogic Logic { get; set; }
        public virtual TelegramMessageHandler DefaultHandler { get; }
        public virtual TelegramMessageHandler HelpHandler { get; }
        public virtual TelegramMessageHandler StartHandler { get; }
        public virtual TelegramMessageHandler CurrentHandler
        {
            get
            {
                if (currentHandler == null)
                {
                    return DefaultHandler;
                }
                else
                {
                    return currentHandler;
                }
            }
            set
            {
                currentHandler = value;
            }
        }

        public virtual void HandleMessage(Api api, Message message, object state)
        {
            if (message.Type == MessageType.TextMessage)
            {
                var text = message.Text.Trim();
                if (text == "/start")
                {
                    StartHandler.HandleMessage(this, api, message, state);
                    return;
                }
                else if (text == "/help")
                {
                    HelpHandler.HandleMessage(this, api, message, state);
                    return;
                }
            }

            var handler = CurrentHandler;
            var next = handler.HandleMessage(this, api, message, state);
            CurrentHandler = next;
        }
    }
}
