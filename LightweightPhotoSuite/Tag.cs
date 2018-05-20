using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPhotoSuite
{
    class Tag
    {
        public static int idCounter;

        public string name { get; private set; }
        public readonly int id;

        public Tag(string name)
        {
            this.name = name;
            id = idCounter++;
        }

        private Tag(string name, int id)
        {
            this.name = name;
            this.id = id;
        }

        public override string ToString()
        {
            return name + Constants.splitChar + id;
        }

        public static Tag FromString(string str)
        {
            string[] temp = str.Split(Constants.splitChar);
            return new Tag(temp[0], Int32.Parse(temp[1]));
        }
    }
}
