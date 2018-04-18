using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPhotoSuite
{
    class DatabaseImage
    {
        private Tag[] allTags;
        private Photo[] allPhotos;
        private string[] scannedPaths;
        
        public DatabaseImage(string dbFilePath)
        {
            // photos with tags, file, 
        }

        public DatabaseImage(PhotoDatabase photoDatabase, FileScanner fileScanner)
        {
            allTags = photoDatabase.getAllTagsCopy();
            allPhotos = photoDatabase.getAllPhotosCopy();
            scannedPaths = fileScanner.getAllScanPathsCopy();
        }

        public void writeToFile(string dbFilePath)
        {
            try
            {
                using (FileStream fs = new FileStream(dbFilePath, FileMode.Truncate, FileAccess.Write))
                {
                    
                }
            }
            catch (Exception e)
            {
                Logger.log("Was not able to truncate or write to db-file '" + dbFilePath + " || " + e.ToString());
            }
        }
    }
}
