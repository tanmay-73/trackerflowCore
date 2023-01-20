using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerFlow.BAL.Models
{
    public class Profile
    {
        public int id { get; set; }
        public string firstname { get; set; }
        public string lastname { get; set; }
        public string email { get; set; }
        public string mnumber { get; set; }
        public string password { get; set; }
        public string ProfilePhoto { get; set; }

    }
}
