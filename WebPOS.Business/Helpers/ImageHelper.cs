using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebPOS.Business.Helpers
{
    public class ImageHelper
    {
        private static byte[] ConvertImageToByte(string stringBase64) {
            if (stringBase64 is null) return null;

            return Convert.FromBase64String(stringBase64);
        }
        public static string GetImageSrcString(string type,byte[] Image) {
            return string.Format("data:{0};base64,{1}", type, Convert.ToBase64String(Image));
        }
    }
}
