using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LightweightPhotoSuite
{
    class Settings
    {
        internal static string dbFilePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        internal static int imagesToPreload = 15;
        internal static BitmapImage testBmp = new BitmapImage(new Uri(@"C:\Users\Joachim\Dropbox\Praktikum Porsche\Bewerbungsfoto.JPG"));
    }
}
