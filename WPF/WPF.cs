using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WPF
{
    // Image = 350x350
    // 1 Console character = 50px
    public static class WPF
    {
        private static Dictionary<string, Bitmap> bitmapDictionary;
        public static Bitmap GetImage(string name)
        {
            Bitmap bmp = null;

            bitmapDictionary.TryGetValue(name, out bmp);

            if (bmp == null)
            {
                bmp = new Bitmap(name);
                bitmapDictionary.Add(name, bmp);
            }

            return bmp;
        }

        public static void ClearCache()
        {
            bitmapDictionary.Clear();
        }
    }
}
