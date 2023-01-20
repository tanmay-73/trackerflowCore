using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerFlow.BAL.Models
{
    public class AssetDisplay
    {
        public int c_assetid { get; set; }
        public int c_aid { get; set; }
    }
    public class AssetDisplayMore
    {
        public string c_assetname { get; set; }
        public string c_uniqueid { get; set; }
        public string c_areaname { get; set; }
        public int c_floornum { get; set; }        
        public string c_date { get; set; }
        public string c_officename { get; set; }
        public int c_aid { get; set; }
        public int c_floorid { get; set; }
        public int c_ofcid { get; set; }
    }
}
