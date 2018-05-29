using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPhotoSuite
{
    class PhotoDatabase
    {
        object lockObj = new object();
        private Dictionary<Tag, HashSet<Photo>> tagToPhotos;
        private Dictionary<Photo, HashSet<Tag>> photoToTags;

        public PhotoDatabase()
        {
            tagToPhotos = new Dictionary<Tag, HashSet<Photo>>();
            photoToTags = new Dictionary<Photo, HashSet<Tag>>();
        }

        public PhotoDatabase(List<Photo> photos, List<HashSet<Tag>> containedTags) : this()
        {
            for (int i = 0; i < photos.Count; i++)
            {
                photoToTags.Add(photos[i], containedTags[i]);
                addTags(photos[i], containedTags[i]);
            }
        }

        public bool remove(Photo photo)
        {
            try
            {
                lock (lockObj)
                {
                    foreach (Tag tag in photoToTags[photo])
                    {
                        tagToPhotos[tag].Remove(photo);
                    }

                    photoToTags.Remove(photo);
                }
            }
            catch (Exception e)
            {
                DataManagement.logger.log("An error occurred while removing a photo: " + photo.ToString());
                return false;
            }

            return true;
        }

        public Photo[] getAllPhotosCopy()
        {
            Photo[] allPhotosCopy;

            lock (lockObj)
                allPhotosCopy = photoToTags.Keys.ToArray();

            return allPhotosCopy;
        }

        public Photo[] getPhotos(Tag[] tags)
        {
            if (tags.Length == 0)
                return getAllPhotosCopy();

            HashSet<Photo> photos = new HashSet<Photo>(tagToPhotos[tags[0]]);

            for (int i = 1; i < tags.Length; i++)
            {
                photos.RemoveWhere(x => !tagToPhotos[tags[i]].Contains(x)); // removes all photos which are not included in all the photo-lists of the other tags
            }

            return photos.ToArray();
        }

        public void addPhotos(IEnumerable<PhotoStub> photoStubs)
        {
            foreach (PhotoStub photoStub in photoStubs)
                addPhoto(photoStub);
        }

        public void addPhoto(PhotoStub photoStub)
        {
            HashSet<Tag> tagListRef = new HashSet<Tag>();
            Photo photo = new Photo(photoStub, tagListRef);
            photoToTags.Add(photo, tagListRef);
        }

        public void addTags(Photo photo, IEnumerable<Tag> tags)
        {
            foreach (Tag tag in tags)
                addTag(photo, tag);
        }

        public void addTag(IEnumerable<Photo> photos, Tag tag)
        {
            foreach (Photo photo in photos)
                addTag(photo, tag);
        }

        public void addTags(IEnumerable<Photo> photos, IEnumerable<Tag> tags)
        {
            foreach (Photo photo in photos)
                addTags(photo, tags);
        }

        /// <summary>
        /// Adds a new tag to an existing photo.
        /// </summary>
        /// <param name="photo"></param>
        /// <param name="tag"></param>
        /// <returns>true if the tag was not already contained</returns>
        public void addTag(Photo photo, Tag tag)
        {
            lock (lockObj)
            {
                if (photoToTags.ContainsKey(photo))
                {
                    if (!photoToTags[photo].Contains(tag))
                        photoToTags[photo].Add(tag);
                }
                else
                    throw new InvalidOperationException("Photo was not contained in the database: " + photo.ToString());

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
