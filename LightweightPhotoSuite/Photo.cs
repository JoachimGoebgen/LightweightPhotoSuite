using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPhotoSuite
{
    class Photo
    {
        private List<Tag> tags;
        private string filePath;
        private DateTime exposureDate;

        public Photo(PhotoStub stub, List<Tag> tagListRef)
        {
            filePath = stub.filePath;
            exposureDate = stub.exposureDate;
            tags = tagListRef; // reference! Tags get changed in this list if changed in original list.
        }

        public Tag[] getTagsCopy()
        {
            Tag[] ret;
            lock (tags)
            {
                ret = tags.ToArray();
            }
            return ret;
        }

        public string getFilePath()
        {
            return filePath;
        }

        public override string ToString()
        {
            string str = filePath + '|';
            lock (tags)
            {
                for (int i = 0; i < tags.Count; i++)
                    str += tags[i].name + ',';
            }
            str += '|' + exposureDate.ToString();
            return str;
        }
    }
}
