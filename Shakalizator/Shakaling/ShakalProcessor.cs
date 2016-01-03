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

namespace Shakalizator.Shakaling
{
    public class ShakalProcessor : TelegramMessageHandler<ShakalSession>
    {
        public Document Document { get; private set; }
        public File[] Files { get; private set; }
        public int ShakalLevel { get; private set; }
        public ShakalProcessor(File[] files, int shakalLevel)
        {
            Files = files;
            ShakalLevel = shakalLevel;
        }
        public ShakalProcessor(Document document, int shakalLevel)
        {
            Document = document;
            ShakalLevel = shakalLevel;
        }
        public override TelegramMessageHandler CommandStart(ShakalSession session, Api api, Message message, object state)
        {
            api.SendTextMessage(message.Chat.Id, "Зашакаливание скоро будет завершено. Обычно это занимает несколько секунд, но если я не буду отвечать, то попробуйте зашакалить картинку снова" + Emoji.GetEmoji(0x1F60C),
                false,
                0,
                new ReplyKeyboardHide
                {
                    HideKeyboard = true
                });
            BeginShakal(session, api, message.Chat.Id, Files, ShakalLevel);
            return this;
        }
        public override TelegramMessageHandler HandleMessage(ShakalSession session, Api api, Message message, object state)
        {
            api.SendTextMessage(message.Chat.Id, "Подождите, зашакаливание еще не завершено");
            return base.HandleMessage(session, api, message, state);
        }

        public void BeginShakal(ShakalSession session, Api api, long chatId, File[] files, int shakalLevel)
        {
            new Thread(() =>
            {
                try
                {
                    var photo = files.Last();

                    var file = api.GetFile(photo.FileId).Result;

                    var fileName = chatId + ".jpg";
                    var shakaled = Shakaler.ShakalPhoto(file.FileStream, shakalLevel);
                    shakaled.Position = 0;

                    var res = api.SendDocument(chatId, new FileToSend(fileName, shakaled)).Result;
                    Console.WriteLine(res);

                    api.SendTextMessage(chatId, "Зашакаливание завершено" + Emoji.GetEmoji(0x1F438));
                }
                catch
                {
                    try
                    {
                        api.SendTextMessage(chatId, "Ошибка при зашакаливании" + Emoji.GetEmoji(0x1F614));
                    }
                    catch
                    {

                    }
                }

                session.CurrentHandler = session.DefaultHandler;
            }).Start();
        }
    }
}
