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

        public Photo(string filePath, string exposureDate)
        {
            this.tags = new List<Tag>();
            this.filePath = filePath;
            Logger.log(exposureDate);
            this.exposureDate = new DateTime();
        }

        /// <summary>
        /// Must only be called by PhotoDatabase or we'll end up with inconsistent data!
        /// </summary>
        /// <param name="tag"></param>
        public void addTag(Tag tag)
        {
            tags.Add(tag);
        }

        public Tag[] getTagsCopy()
        {
            return tags.ToArray();
        }

        public string getFilePath()
        {
            return filePath;
        }
    }
}
