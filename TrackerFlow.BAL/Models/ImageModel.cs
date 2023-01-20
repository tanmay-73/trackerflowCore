using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerFlow.BAL.Models
{
    public class ImageModel
    {
        public int id { get; set; }
        public string path { get; set; }
        public dynamic request { get; set; }
    }
}
