using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shakalizator.TelegramCore
{
    public static class Emoji
    {
        public static string GetEmoji(int id)
        {
            return char.ConvertFromUtf32(id);
        }
    }
}
