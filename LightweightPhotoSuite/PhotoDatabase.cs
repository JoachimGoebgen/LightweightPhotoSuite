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
        private TagDatabase tagDB;
        private Dictionary<Tag, HashSet<Photo>> tagToPhotos;
        private Dictionary<Photo, HashSet<Tag>> photoToTags;

        public PhotoDatabase(TagDatabase tagDB)
        {
            this.tagDB = tagDB;
            tagToPhotos = new Dictionary<Tag, HashSet<Photo>>();
            photoToTags = new Dictionary<Photo, HashSet<Tag>>();
        }

        public PhotoDatabase(TagDatabase tagDB, List<Photo> photos, List<List<Tag>> containedTags) : this(tagDB)
        {
            for (int i = 0; i < photos.Count; i++)
                add(photos[i], containedTags[i]);
        }

        public Tag[] getAllTagsCopy()
        {
            Tag[] allTagsCopy;

            lock (lockObj)
                allTagsCopy = tagDB.getAllTagsCopy();
            
            return allTagsCopy;
        }

        public Photo[] getAllPhotosCopy()
        {
            Photo[] allPhotosCopy;

            lock (lockObj)
                allPhotosCopy = photoToTags.Keys.ToArray();

            return allPhotosCopy;
        }

        public void add(Photo photo)
        {
            add(photo, tagDB.GiveTag(Constants.todoTag));
        }

        public void add(IEnumerable<Photo> photos)
        {
            foreach (Photo photo in photos)
                add(photo);
        }

        public void add(Photo photo, IEnumerable<Tag> tags)
        {
            foreach (Tag tag in tags)
                add(photo, tag);
        }

        public void add(IEnumerable<Photo> photos, Tag tag)
        {
            foreach (Photo photo in photos)
                add(photo, tag);
        }

        public void add(IEnumerable<Photo> photos, IEnumerable<Tag> tags)
        {
            foreach (Photo photo in photos)
                add(photo, tags);
        }

        public void add(Photo photo, Tag tag)
        {
            lock (lockObj)
            {
                if (photoToTags.ContainsKey(photo))
                {
                    if (!photoToTags[photo].Contains(tag))
                        photoToTags[photo].Add(tag);
                }
                else
                    photoToTags.Add(photo, new HashSet<Tag>(new Tag[1] { tag }));

                if (tagToPhotos.ContainsKey(tag))
                {
                    if (!tagToPhotos[tag].Contains(photo))
                        tagToPhotos[tag].Add(photo);
                }
                else
                    tagToPhotos.Add(tag, new HashSet<Photo>(new Photo[1] { photo }));
            }
            
        }
        
    }
}
