using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPhotoSuite
{
    class Settings
    {
        internal static string dbFilePath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        internal static int imagesToPreload = 15;
    }
}
