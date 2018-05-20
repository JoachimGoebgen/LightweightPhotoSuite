using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPhotoSuite
{
    class TagDatabase
    {
        private object lockObj = new object();

        private Dictionary<string, Tag> nameToTag;
        private Dictionary<int, Tag> idToTag;
        
        public TagDatabase(List<Tag> tags)
        {
            foreach (Tag tag in tags)
            {
                nameToTag.Add(tag.name, tag);
                idToTag.Add(tag.id, tag);
            }
        }

        public Tag GiveTag(string name)
        {
            if (nameToTag.ContainsKey(name))
            {
                return nameToTag[name];
            }
            else
            {
                Tag newTag = new Tag(name);
                lock (lockObj)
                {
                    nameToTag.Add(name, newTag);
                    idToTag.Add(newTag.id, newTag);
                }
                return newTag;
            }
        }

        public Tag GiveTag(int id)
        {
            if (idToTag.ContainsKey(id))
                return idToTag[id];
            else
                return null;
        }

        public bool RenameTag(Tag tag, string newName)
        {
            if (nameToTag.ContainsKey(newName))
            {
                return false;
            }
            else
            {

                name = newName;
                nameToTag.Remove(name);
                nameToTag.Add(newName, this);
                return true;
            }
        }

        public Tag[] getAllTagsCopy()
        {
            Tag[] allTagsCopy;

            lock (lockObj)
                allTagsCopy = nameToTag.Values.ToArray();

            return allTagsCopy;
        }
        
    }
}
