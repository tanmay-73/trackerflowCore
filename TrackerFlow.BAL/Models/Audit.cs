using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerFlow.BAL.Models
{
    public class Audit
    {

        //public object c_assetid { get; set; }
        public string c_assetname { get; set; }
        public string c_uniqueid { get; set; }
        public int c_aid { get; set; }
        public string c_desc { get; set; }
        public string c_status { get; set; }
        public bool c_ismissing { get; set; }
        public DateTime c_date { get; set; }


    }
    public class Root
    {
        public List<Audit> assetlist { get; set; }
    }
}
