using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LightweightPhotoSuite
{
    class PhotoStub
    {
        public string filePath;
        public DateTime exposureDate;

        public PhotoStub(string filePath, DateTime exposureDate)
        {
            this.filePath = filePath;
            this.exposureDate = exposureDate;
        }
    }
}
