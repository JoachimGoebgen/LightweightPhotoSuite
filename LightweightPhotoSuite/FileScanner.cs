using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPhotoSuite
{
    class FileScanner
    {
        private List<string> pathsToScan;
        private Dictionary<string, Photo> allScannedPhotos;

        public FileScanner()
        {
            pathsToScan = new List<string>();
            allScannedPhotos = new Dictionary<string, Photo>();
        }

        public FileScanner(DataManagement db)
        {

        }

        private void startScan()
        {

        }
    }
}
