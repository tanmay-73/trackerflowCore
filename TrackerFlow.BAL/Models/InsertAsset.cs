using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerFlow.BAL.Models
{
    public class InsertAsset
    {
        public string c_assetname { get; set; }
        public string c_uniqueid { get; set; }
        public string c_date { get; set; }
        public int c_areaid { get; set; }
    }
}
