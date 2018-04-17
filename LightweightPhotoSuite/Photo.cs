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
        private int width_px;
        private int height_px;

        public Photo(string filePath, DateTime exposureDate, int width_px, int height_px)
        {
            this.filePath = filePath;
            this.exposureDate = exposureDate;
            this.width_px = width_px;
            this.height_px = height_px;
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
