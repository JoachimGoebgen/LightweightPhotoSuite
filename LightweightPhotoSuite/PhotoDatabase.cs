using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPhotoSuite
{
    class PhotoDatabase
    {
        object lockObj;

        private List<Tag> allTags;
        private List<Photo> allPhotos;
        private Dictionary<Tag, List<Photo>> tagToPhoto;

        public Tag[] getAllTagsCopy()
        {
            Tag[] allTagsCopy;

            lock (lockObj)
                allTagsCopy = allTags.ToArray();
            
            return allTagsCopy;
        }

        public Photo[] getAllPhotosCopy()
        {
            Photo[] allPhotosCopy;

            lock (lockObj)
                allPhotosCopy = allPhotos.ToArray();

            return allPhotosCopy;
        }

        public void addPhoto(Photo photo)
        {

        }

        public void addPhotos(IEnumerable<Photo> photos)
        {

        }

        public void addPhotoWithTags(Photo photo, IEnumerable<Tag> tags)
        {

        }

        public void addPhotosWithTags(IEnumerable<Photo> photos, IEnumerable<Tag> tags)
        {

        }

        public void addTag(Photo photo, Tag tag)
        {

        }

        public void addTags(Photo photo, IEnumerable<Tag> tags)
        {

        }

        public void addTag(IEnumerable<Photo> photos, Tag tag)
        {

        }

        public void addTags(IEnumerable<Photo> photos, IEnumerable<Tag> tags)
        {

        }
    }
}
