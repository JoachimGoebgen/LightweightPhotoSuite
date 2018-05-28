using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPhotoSuite
{
    class Photo : PhotoStub
    {
        private HashSet<Tag> tags;

        public Photo(PhotoStub stub, HashSet<Tag> tagListRef) : base(stub.filePath, stub.exposureDate)
        {
            tags = tagListRef; // reference! Tags get changed in this list if changed in original list.
        }

        private Photo(string filePath, DateTime exposureDate, HashSet<Tag> tagListRef) : base(filePath, exposureDate)
        {
            tags = tagListRef; // reference! Tags get changed in this list if changed in original list.
        }

        public Tag[] getTagsCopy()
        {
            Tag[] ret;

            lock (tags)
                ret = tags.ToArray();

            return ret;
        }

        public string getFilePath()
        {
            return filePath;
        }

        public override string ToString()
        {
            string str = filePath + Constants.splitChar;
            lock (tags)
            {
                foreach (Tag tag in tags)
                    str += tag.id + Constants.subSplitChar;
            }
            str += Constants.splitChar + exposureDate.ToString();
            return str;
        }

        // tagList gets filled!
        internal static Photo FromString(string element, TagDatabase tagDB, HashSet<Tag> emptyTagList)
        {
            string[] parts = element.Split(Constants.splitChar);
            string[] tagIDs = parts[1].Split(Constants.subSplitChar);
            
            for (int i = 0; i < tagIDs.Length; i++)
                emptyTagList.Add(tagDB.GiveTag(Int32.Parse(tagIDs[i])));

            return new Photo(parts[0], DateTime.Parse(parts[2]), emptyTagList);
        }

    }
}
