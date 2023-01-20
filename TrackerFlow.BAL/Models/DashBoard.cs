using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerFlow.BAL.Models
{
    public class DashBoard
    {
        public int casepoint2 { get; set; }
        public int casepoint4 { get; set; }
        public int clarent { get; set; }
        public string new_item { get; set; }
        public string c_status { get; set; }

        public string c_assetcount { get; set; }
        public string c_officecount { get; set; }
        public int c_aid { get; set; }
        public string c_areaname { get; set; }
        public int c_floorid { get; set; }
        public string c_floornum { get; set; }
        public int c_ofcid { get; set; }
        public string c_ofcname { get; set; }
        public string c_a_asset { get; set; }
        public string c_l_asset { get; set; }
        public string c_date { get; set; }
        public string c_auditor { get; set; }

    }
}
