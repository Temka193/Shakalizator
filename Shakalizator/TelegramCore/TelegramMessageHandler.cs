using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace Shakalizator.TelegramCore
{
    public abstract class TelegramMessageHandler
    {
        public virtual TelegramMessageHandler CommandStart(TelegramSession session, Api api, Message message, object state)
        {
            return this;
        }
        public virtual TelegramMessageHandler CommandHelp(TelegramSession session, Api api, Message message, object state)
        {
            return this;
        }
        public virtual TelegramMessageHandler HandleMessage(TelegramSession session, Api api, Message message, object state)
        {
            return this;
        }
    }
    public abstract class TelegramMessageHandler<T> : TelegramMessageHandler where T : TelegramSession
    {
        public override TelegramMessageHandler CommandStart(TelegramSession session, Api api, Message message, object state)
        {
            return CommandStart((T)session, api, message, state);
        }
        public override TelegramMessageHandler CommandHelp(TelegramSession session, Api api, Message message, object state)
        {
            return CommandHelp((T)session, api, message, state);
        }
        public override TelegramMessageHandler HandleMessage(TelegramSession session, Api api, Message message, object state)
        {
            return HandleMessage((T)session, api, message, state);
        }
        public virtual TelegramMessageHandler CommandStart(T session, Api api, Message message, object state)
        {
            return this;
        }
        public virtual TelegramMessageHandler CommandHelp(T session, Api api, Message message, object state)
        {
            return this;
        }
        public virtual TelegramMessageHandler HandleMessage(T session, Api api, Message message, object state)
        {
            return this;
        }
    }
}
