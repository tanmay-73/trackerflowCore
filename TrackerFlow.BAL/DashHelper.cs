using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TrackerFlow.BAL.Models;
namespace TrackerFlow.BAL
{
    public class DashHelper:Helper
    {
        public Profile GetPro(int Id)
        {
            Profile pro = null;
            try
            {
                DataTable dt = new DataTable();
                NpgsqlCommand com = new NpgsqlCommand("select c_id,c_fname,c_lname,c_email,c_contact from t_register", constr);
                constr.Open();
                NpgsqlDataReader datar = com.ExecuteReader();
                if (datar.HasRows)
                {
                    dt.Load(datar);
                }                                        
                else
                {
                    //FileName = null;
                }
                // pro = (from DataRow dr in dt.Rows
                //        where int.Parse(dr["c_id"].ToString()) == Id
                //        select new Profile()
                //        {
                //            firstname = dr["c_fname"].ToString(),
                //            lastname = dr["c_lname"].ToString(),
                //            email = dr["c_email"].ToString(),
                //            mnumber = dr["c_contact"].ToString(),
                //            ProfilePhoto = FileName
                //        }).ToList().FirstOrDefault();
                // constr.Close();
            }
            catch (Exception ex)
            {
                throw ex;    
            }
            finally
            {
                constr.Close();
            }
            return pro;
        }
        public void Update(Profile data, int id)
        {            
            NpgsqlCommand com = new NpgsqlCommand(@"UPDATE t_register set c_fname=@pfname,c_lname=@plname,c_email=@pemail,c_contact=@pmnumber where c_id=@pid", constr);
            com.Parameters.AddWithValue("@pid", id);            
            com.Parameters.AddWithValue("@pfname", data.firstname);
            com.Parameters.AddWithValue("@pemail", data.email);
            com.Parameters.AddWithValue("@plname", data.lastname);            
            com.Parameters.AddWithValue("@pmnumber", data.mnumber);            
            constr.Open();
            com.ExecuteNonQuery();
            constr.Close();
        }        
        public bool ChangePasswd(Password pd,int id)
        {
            NpgsqlCommand com = new NpgsqlCommand("SELECT c_password FROM t_register WHERE c_id=@id", constr);
            com.Parameters.AddWithValue("@id", id);
            constr.Open();
            NpgsqlDataReader datar = com.ExecuteReader();
            if (datar.HasRows)
            {
                datar.Read();
                if (Decrypt(datar["c_password"].ToString()) == pd.curpassword)
                {
                    constr.Close();
                    com.CommandText = "UPDATE t_register SET c_password=@pwd WHERE c_id=@id";
                    com.Parameters.AddWithValue("@id", id);
                    com.Parameters.AddWithValue("@pwd", Encrypt(pd.newpassword));
                    constr.Open();
                    com.ExecuteNonQuery();
                    constr.Close();
                    return true;
                }
            }
            constr.Close();
            return false;
        }
        public int AddImage(ImageModel data)
        {
            try
            {
                //HttpPostedFile postedFile = data.request.Files[0];
                // string fileName2 = data.id.ToString() + "." + postedFile.ContentType.Split('/')[1];
                // fileName2 = Path.Combine(data.path, fileName2);
               // postedFile.SaveAs(fileName2);
                return 1;
            }
            catch (Exception ex)
            {                
                return -1;
            }
        }
        // public int RemoveImage(int id)
        // {
        //     try
        //     {             

        //     }
        //     catch (Exception ex)
        //     {
        //         return -1;
        //     }
        // }
        public List<TblOffice> GetAll()
        {
            DataTable dt = new DataTable();
            NpgsqlCommand com = new NpgsqlCommand("select * from t_office", constr);
            constr.Open();
            NpgsqlDataReader datar = com.ExecuteReader();
            if (datar.HasRows)
            {
                dt.Load(datar);
            }
            List<TblOffice> contactlist = new List<TblOffice>();
            contactlist = (from DataRow dr in dt.Rows
                           select new TblOffice()
                           {
                               c_ofcid = Convert.ToInt32(dr["c_ofcid"]),
                               c_ofcname = dr["c_ofcname"].ToString()
                           }).ToList();
            constr.Close();
            return contactlist;
        }

    }
}
