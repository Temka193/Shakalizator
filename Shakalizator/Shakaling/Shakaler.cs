using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Shakalizator.Shakaling
{
    public static class Shakaler
    {
        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {

            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            ImageCodecInfo result = null;

            foreach (var codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    Console.WriteLine(codec.CodecName);
                    result = codec;
                }
            }
            return result;
        }
        public static Stream ShakalPhoto(Stream photo, int shakalLevel)
        {
            var bitmap = new Bitmap(photo);
            var stream = (Stream)new MemoryStream();

            var codec = GetEncoder(ImageFormat.Jpeg);
            var encoder = Encoder.Quality;
            var parameters = new EncoderParameters(1);
            parameters.Param[0] = new EncoderParameter(encoder, (long)(100 - shakalLevel));

            bitmap.Save(stream, codec, parameters);

            return stream;
        }
    }
}
