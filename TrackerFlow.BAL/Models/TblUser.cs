using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrackerFlow.BAL.Models
{
    public class TblUser
    {
        public int c_id { get; set; }
        public string c_fname { get; set; }
        public string c_lname { get; set; }
        public string c_email { get; set; }
        public string c_password { get; set; }
        public string c_cpassword { get; set; }
    }
}