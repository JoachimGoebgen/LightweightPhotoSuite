using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPhotoSuite
{
    class DataManagement
    {
        public static Logger logger;

        private FileScanner fileScanner;
        private PhotoDatabase photoDatabase;
        private TagDatabase tagDatabase;


        public DataManagement()
        {
            logger = new Logger(Constants.logFolderPath, Constants.logFileName);
            fileScanner = new FileScanner();
            tagDatabase = new TagDatabase();
            photoDatabase = new PhotoDatabase();
        }

        public DataManagement(string dbFilePath) : this()
        {
            createFromFile(dbFilePath);
        }

        public void doAddTag(Photo photo, string tag)
        {
            photoDatabase.addTag(photo, tagDatabase.GiveTag(tag));
        }

        public void doRemoveTag(Photo photo)
        {
            photoDatabase.remove(photo);
        }

        public void doBackup()
        {
            saveToFile(Settings.dbFilePath, fileScanner.getScanPathsCopy(), tagDatabase.getAllTagsCopy(), photoDatabase.getAllPhotosCopy());
        }

        public void doNewPhotoScan(String path)
        {
            photoDatabase.addPhotos(fileScanner.scan(path));
        }

        public void doFullScan()
        {
            photoDatabase.addPhotos(fileScanner.scanAllPaths());
        }

        public Photo[] getPhotos(IEnumerable<Tag> tags)
        {
            return photoDatabase.getPhotos(tags.ToArray());
        }
        

        private void createFromFile(string dbFilePath)
        {
            string[] lines;

            try
            {
                lines = File.ReadAllLines(dbFilePath);
            }
            catch (Exception e)
            {
                logger.log("Was not able to read from db-file '" + dbFilePath + " || " + e.ToString());
                return;
            }

            int mode = 0;
            List<Tag> tags = new List<Tag>(10);
            List<string> scanPaths = new List<string>(10);
            List<Photo> photos = new List<Photo>(lines.Length);
            List<HashSet<Tag>> tagsOnPhoto = new List<HashSet<Tag>>(photos.Count);

            try
            {
                string element;
                for (int i = 0; i < lines.Length; i++)
                {
                    element = lines[i];

                    if (element == Constants.lineSeparator)
                    {
                        switch (mode)
                        {
                            case 0:
                                tagDatabase = new TagDatabase(tags);
                                break;

                            case 1:
                                photoDatabase = new PhotoDatabase(photos, tagsOnPhoto);
                                break;

                            case 2:
                                fileScanner = new FileScanner(scanPaths, photos);
                                break;
                        }

                        mode++;
                        continue;
                    }

                    switch (mode)
                    {
                        case 0:
                            tags.Add(Tag.FromString(element));
                            break;

                        case 1:
                            photos.Add(Photo.FromString(element, tagDatabase, tagsOnPhoto[i]));
                            break;

                        case 2:
                            scanPaths.Add(element);
                            break;
                    }
                }
            }
            catch (Exception e)
            {
                logger.log("db-file is corrupted: '" + dbFilePath + " || " + e.ToString());
            }

        }

        private void saveToFile(string dbFilePath, string[] scannedPaths, Tag[] allTags, Photo[] allPhotos)
        {
            int totalLines = allTags.Length + allPhotos.Length + scannedPaths.Length + 2;
            string[] lines = new string[totalLines];

            int from = 0;
            int to = allTags.Length;

            // 0: tags
            for (int i = from; i < to; i++)
                lines[i] = allTags[i].ToString();

            // 1: photos
            from = to + 1;
            to = from + allPhotos.Length;
            for (int i = from; i < to; i++)
                lines[i] = allPhotos[i].ToString();

            // 2: scanPaths
            from = to + 1;
            to = from + scannedPaths.Length;
            for (int i = from; i < to; i++)
                lines[i] = scannedPaths[i];

            try
            {
                File.WriteAllLines(dbFilePath, lines);
            }
            catch (Exception e)
            {
                logger.log("Was not able to truncate or write to db-file '" + dbFilePath + " || " + e.ToString());
            }
        }



        private static string concat(string a, string b)
        {
            return a + Constants.splitChar + b;
        }

        private static void deconcat(string str, out string a, out string b)
        {
            string[] temp = str.Split('|');
            a = temp[0];
            b = temp[1];
        }

    }
}
