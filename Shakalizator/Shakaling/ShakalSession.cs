using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shakalizator.TelegramCore;
using Telegram.Bot;
using Telegram.Bot.Types;
using System.Threading;
using System.Drawing;
using System.IO;

namespace Shakalizator.Shakaling
{
    public class ShakalSession : TelegramSession
    {
        private CommandsRoot root = new CommandsRoot();
        public override TelegramMessageHandler DefaultHandler
        {
            get
            {
                return root;
            }
        }
        public override TelegramMessageHandler StartHandler
        {
            get
            {
                return new TextAnswerMessageHandler(Resources.Start);
            }
        }
        public override TelegramMessageHandler HelpHandler
        {
            get
            {
                return new TextAnswerMessageHandler(Resources.Help);
            }
        }
    }
}
