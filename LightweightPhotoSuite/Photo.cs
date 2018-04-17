using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPhotoSuite
{
    class Photo
    {
        private List<string> tags;
        private string filePath;
        private DateTime exposureDate;

        public Photo(string filePath, string exposureDate)
        {
            Logger.log(exposureDate);
            this.filePath = filePath;
            this.exposureDate = new DateTime();
        }

        public bool addTag(string tag)
        {
            if (!tags.Contains(tag))
            {
                tags.Add(tag);
                return true;
            }

            return false;
        }

        public List<string> getTags()
        {
            return tags.ToList(); // shallow copy
        }

        public string getFilePath()
        {
            return filePath;
        }
    }
}
