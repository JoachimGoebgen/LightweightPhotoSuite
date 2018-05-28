using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPhotoSuite
{
    static class Constants
    {
        internal static string logFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
        internal static string logFileName = "LPS-log";

        internal const char splitChar = '|';
        internal const char subSplitChar = ',';
        internal const string lineSeparator = "";
    }
}
