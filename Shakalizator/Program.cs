using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using Shakalizator.Shakaling;

namespace Shakalizator
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args == null || args.Length == 0)
            {
                args = new string[]
                {
                    "api.key"
                };
            }
            var token = File.ReadAllText(args[0], Encoding.UTF8).Trim();

            var bot = new ShakalBot(token);
            bot.Start();

            while (true) Thread.Sleep(1000000); 
        }
    }
}
