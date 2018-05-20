﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPhotoSuite
{
    class DataManagement
    {
        private FileScanner fileScanner;
        private PhotoDatabase photoDatabase;
        private TagDatabase tagDatabase;

        public DataManagement()
        {

        }

        public DataManagement(string dbFilePath)
        {
            createFromFile(dbFilePath);
        }


        public void doAddTag(Photo photo)
        {

        }

        public void doRemoveTag(Photo photo)
        {

        }

        public void doBackup()
        {

        }

        public void doNewPhotoScan()
        {

        }

        public void doFullScan()
        {

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
                Logger.log("Was not able to read from db-file '" + dbFilePath + " || " + e.ToString());
                return;
            }

            int mode = 0;
            List<Tag> tags = new List<Tag>(10);
            List<string> scanPaths = new List<string>(10);
            List<Photo> photos = new List<Photo>(lines.Length);
            List<List<Tag>> tagsOnPhoto = new List<List<Tag>>(photos.Count);

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
                                photoDatabase = new PhotoDatabase(tagDatabase, photos, tagsOnPhoto);
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
                Logger.log("db-file is corrupted: '" + dbFilePath + " || " + e.ToString());
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
                Logger.log("Was not able to truncate or write to db-file '" + dbFilePath + " || " + e.ToString());
            }
        }

        private string concat(string a, string b)
        {
            return a + Constants.splitChar + b;
        }

        private void deconcat(string str, out string a, out string b)
        {
            string[] temp = str.Split('|');
            a = temp[0];
            b = temp[1];
        }

    }
}
