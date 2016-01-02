using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Shakalizator.TelegramCore;
using System.Threading;

namespace Shakalizator.Shakaling
{
    public class ShakalBot
    {
        public int LastUpdateId { get; private set; }
        public Api Api { get; private set; }

        public TelegramLogic<ShakalSession> Logic { get; private set; }
        public string Token { get; private set; }

        public ShakalBot(string token)
        {
            Token = token;

            Api = new Api(token);
            Logic = new TelegramLogic<ShakalSession>();
        }
        public bool Start()
        {
            try
            {
                var me = Api.GetMe().Result;
                Console.WriteLine("{0}: {1}", me.Username, me.Id);
            }
            catch
            {
                return false;
            }
            new Thread(ThreadTick).Start();
            return true;
        }

        private void ThreadTick()
        {
            var offset = 0;
            while (true)
            {
                try
                {
                    var updates = Api.GetUpdates(offset).Result;
                    foreach (var update in updates)
                    {
                        offset = update.Id + 1;
                        Console.WriteLine("[{0}] {1}", DateTime.Now.ToString("u"), update.Message.Type);
                        try
                        {
                            Logic.HandleMessage(Api, update.Message, this);
                        }
                        //*
                        catch (Exception ex)
                        {
                            Console.Error.WriteLine(ex.Message);
                            Console.Error.WriteLine(ex.StackTrace);
                            Console.Error.WriteLine(ex.Source);
                        }
                        //*/
                    }
                }
                //*
                catch (Exception ex)
                {
                    Console.Error.WriteLine(ex.Message);
                    Console.Error.WriteLine(ex.StackTrace);
                    Console.Error.WriteLine(ex.Source);
                }
                //*/
            }
        }
    }
}
