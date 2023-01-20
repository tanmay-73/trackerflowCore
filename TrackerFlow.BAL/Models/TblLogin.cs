using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackerFlow.BAL.Models
{
    public class TblLogin
    {
        //[Key]
        public int c_id { get; set; }

        //[Required(ErrorMessage = "Enter Email")]
        //[Display(Name = "Email Address")]
        //[DataType(DataType.EmailAddress)]
        public string c_email { get; set; }

        //[Required(ErrorMessage = "Enter Password")]
        //[Display(Name = "Password")]
        //[DataType(DataType.Password)]
        public string c_password { get; set; }
        public bool c_RememberMe { get; set; }
    }
}
