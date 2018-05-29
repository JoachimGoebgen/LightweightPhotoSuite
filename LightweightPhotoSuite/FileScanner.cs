using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace LightweightPhotoSuite
{
    class FileScanner
    {
        private List<string> scanPaths;
        private HashSet<string> scannedPhotoPaths;

        public FileScanner()
        {
            scanPaths = new List<string>();
            scannedPhotoPaths = new HashSet<string>();
        }

        public FileScanner(List<string> scanPaths, List<Photo> photos)
        {
            this.scanPaths = scanPaths;
            scannedPhotoPaths = new HashSet<string>();
            for (int i = 0; i < photos.Count; i++)
            {
                scannedPhotoPaths.Add(photos[i].filePath);
            }
        }

        public List<PhotoStub> scanAllPaths()
        {
            List<PhotoStub> newPhotos = new List<PhotoStub>();
            string[] paths = getScanPathsCopy();

            for (int i = 0; i < paths.Length; i++)
            {
                newPhotos.AddRange(scan(paths[i]));
            }
            
            return newPhotos;
        }

        public bool addAndScanPath(string path, out List<PhotoStub> newPhotos)
        {
            if (addPath(path))
            {
                newPhotos = scan(path);
                return true;
            }

            newPhotos = new List<PhotoStub>();
            return false;
        }

        public bool addPath(string path)
        {
            bool success = true;
            lock (scanPaths)
            {
                if (pathIsSubPath(path))
                    success = false;
                else
                    scanPaths.Add(path);
            }
            return success;
        }

        public string[] getScanPathsCopy()
        {
            string[] scanPathsCopy;

            lock (scanPaths)
                scanPathsCopy = scanPaths.ToArray();

            return scanPathsCopy;
        }

        public bool addScanPathAndScan(string path, out List<PhotoStub> photos)
        {
            bool success = true;
            lock (scanPaths)
            {
                if (pathIsSubPath(path))
                    success = false;
                else
                    scanPaths.Add(path);
            }

            if (success)
                photos = scan(path);
            else
                photos = new List<PhotoStub>();

            return success;
        }

        public List<PhotoStub> scan(string path)
        {
            if (!scanPaths.Contains(path))
            {
                DataManagement.logger.log("Scanning a path that is not contained in the list of all sacn-paths is not possible: '" + path);
            }

            List<PhotoStub> newPhotos = new List<PhotoStub>();
            
            IEnumerable<string> allPhotoFilePaths;
            try
            {
                allPhotoFilePaths = Directory.EnumerateFiles(path, "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".jpg") || s.EndsWith(".jpeg")).ToArray();
            }
            catch (Exception e)
            {
                DataManagement.logger.log("Was not able to open folder '" + path + " || " + e.ToString());
                return null;
            }

            IEnumerable<string> newPhotoFilePaths = allPhotoFilePaths.Where(p => !scannedPhotoPaths.Contains(p));
            foreach (string filePath in newPhotoFilePaths)
            {
                try
                {
                    FileInfo file = new FileInfo(filePath);
                    using (FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                    {
                        BitmapSource img = BitmapFrame.Create(fs);
                        BitmapMetadata md = (BitmapMetadata)img.Metadata;
                        PhotoStub stub = new PhotoStub(filePath, DateTime.Parse(md.DateTaken));
                        newPhotos.Add(stub);
                        scannedPhotoPaths.Add(filePath);
                    }
                }
                catch (Exception e)
                {
                    DataManagement.logger.log("Was not able to create photo-instance from file '" + filePath + " || " + e.ToString());
                    continue;
                }
            }
            
            return newPhotos;
        }

        private bool pathIsSubPath(string path)
        {
            for (int i = 0; i < scanPaths.Count; i++)
            {
                if (path.StartsWith(scanPaths[i]))
                {
                    DataManagement.logger.log('\'' + path + "' is sub-path from '" + scanPaths[i] + '\'');
                    return true;
                }
            }
            return false;
        }
    }
}


