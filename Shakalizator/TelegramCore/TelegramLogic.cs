using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;


namespace Shakalizator.TelegramCore
{
    public abstract class TelegramLogic
    {
        public Dictionary<long, TelegramSession> Sessions { get; private set; }
        public TelegramLogic()
        {
            Sessions = new Dictionary<long, TelegramSession>();
        }
        protected virtual TelegramSession CreateSession()
        {
            throw new Exception("CreateSession in abstract class");
        }
        public virtual void HandleMessage(Api api, Message message, object state)
        {
            var chatId = message.Chat.Id;
            TelegramSession session;
            if (!Sessions.TryGetValue(chatId, out session))
            {
                session = CreateSession();
                Sessions[chatId] = session;
            }

            session.HandleMessage(api, message, state);
        }
    }
    public class TelegramLogic<T> : TelegramLogic where T : TelegramSession, new()
    {
        protected override TelegramSession CreateSession()
        {
            return new T
            {
                Logic = this
            };
        }
    }
}
