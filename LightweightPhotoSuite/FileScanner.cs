﻿using System;
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
        private Dictionary<string, Photo> allScannedPhotos;
        private HashSet<string> allScannedPhotoPaths;

        public FileScanner()
        {
            scanPaths = new List<string>();
            allScannedPhotos = new Dictionary<string, Photo>();
        }

        public FileScanner(DataManagement db)
        {

        }

        private List<PhotoStub> scanNewPhotos()
        {
            List<PhotoStub> newPhotos = new List<PhotoStub>();
            string[] paths = new string[scanPaths.Count];
            scanPaths.CopyTo(paths);

            for (int i = 0; i < paths.Length; i++)
            {
                IEnumerable<string> allPhotoFilePaths;
                try
                {
                    allPhotoFilePaths = Directory.EnumerateFiles(paths[i], "*.*", SearchOption.AllDirectories).Where(s => s.EndsWith(".jpg") || s.EndsWith(".jpeg")).ToArray();
                }
                catch (Exception e)
                {
                    Logger.log("Was not able to open folder '" + paths[i] + " || " + e.ToString());
                    continue;
                }
                
                IEnumerable<string> newPhotoFilePaths = allPhotoFilePaths.Where(p => !allScannedPhotoPaths.Contains(p));
                foreach (string filePath in newPhotoFilePaths)
                {
                    try
                    {
                        FileInfo file = new FileInfo(filePath);
                        using (FileStream fs = new FileStream(file.FullName, FileMode.Open, FileAccess.Read, FileShare.Read))
                        {
                            BitmapSource img = BitmapFrame.Create(fs);
                            BitmapMetadata md = (BitmapMetadata)img.Metadata;
                            newPhotos.Add(new PhotoStub(filePath, DateTime.Parse(md.DateTaken)));
                        }
                    }
                    catch (Exception e)
                    {
                        Logger.log("Was not able to create photo-instance from file '" + filePath + " || " + e.ToString());
                        continue;
                    }
                }
            }
            
            return newPhotos;
        }

        public string[] getAllScanPathsCopy()
        {
            return scanPaths.ToArray();
        }
    }
}
