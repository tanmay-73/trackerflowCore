using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerFlow.BAL.Models
{
    public class AssetModel
    {
        public string c_assetid { get; set; }
        public string c_assetname { get; set; }
        public string c_uniqueid { get; set; }
        public int c_aid { get; set; }
        public string c_desc { get; set; }
        public bool c_ismissing { get; set; }
        public string c_areaname { get; set; }
        public string c_status { get; set; }
        public int c_floornum { get; set; }
        public int c_floorid { get; set; }
        public string c_date { get; set; }
    }
}
