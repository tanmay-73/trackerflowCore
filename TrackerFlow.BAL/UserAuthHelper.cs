using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerFlow.BAL;
using Npgsql;
using System.Data;
using System.Net.Mail;
using System.Configuration;
using System.Net;
using System.IO;
using TrackerFlow.BAL.Models;
using System.Security.Cryptography;
using System.Web;

namespace TrackerFlow.BAL
{
    public class UserAuthHelper : Helper
    {        

        public void SendEmail(TblUser data)
        {
            try
            {
                //SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");                
            var uname = data.c_fname;
            string email = "newinterns2022@gmail.com";
            string pass = "Interns$123";                        
                        using (MailMessage mm = new MailMessage(email, data.c_email))
            {
                mm.Subject = "Registration";
                string htmlString = string.Empty;
                string filename = System.AppDomain.CurrentDomain.BaseDirectory + "/Registration_Email.html";
                using (StreamReader read = new StreamReader(filename))
                {
                    htmlString = read.ReadToEnd();
                }
                htmlString = htmlString.Replace("#[{FirstName}]#", data.c_fname);
                htmlString = htmlString.Replace("#[{UserName}]#", data.c_email);
                htmlString = htmlString.Replace("#[{url}]#", "http://trackerflow.clarent.institute:55006/Sign/login");
                mm.Body =
                  htmlString;
                // mm.Body = "this is body";
                mm.IsBodyHtml = true;
                using (SmtpClient smtp = new SmtpClient())
                {
                    smtp.Host = "smtp.gmail.com";
                    smtp.EnableSsl = true;
                    NetworkCredential networkCred = new NetworkCredential(email, pass);
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = networkCred;
                    smtp.Port = 587;
                    smtp.Send(mm);

                        //smtp.Host = smtpSection.Network.Host;
                        //smtp.EnableSsl = smtpSection.Network.EnableSsl;
                        //NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
                        //smtp.UseDefaultCredentials = true;
                        //smtp.Credentials = networkCred;
                        //smtp.Port = smtpSection.Network.Port;
                        //smtp.Send(mm);
                    }
                }
            }
            catch (Exception e)
            {                
            }



        }

        public bool Register(Models.TblUser data)
        {
            try
            {
                NpgsqlCommand comcheck = new NpgsqlCommand(@"SELECT * FROM t_register WHERE c_email = @c_email ;", constr);
                comcheck.Parameters.AddWithValue("@c_email", data.c_email);
                constr.Open();
                //NpgsqlDataReader datadr = comcheck.ExecuteReader();                
                using (NpgsqlDataReader datadr = comcheck.ExecuteReader())
                {
                    if (datadr.HasRows)
                    {
                        constr.Close();
                        return false;
                    }
                    else
                    {
                        // string encPassword = encrypt(data.c_password);

                        constr.Close();
                        NpgsqlCommand com = new NpgsqlCommand(@"INSERT INTO t_register(c_fname,c_lname,c_email,c_password) 
                                                    VALUES (@c_fname,@c_lname,@c_email,@c_password)", constr);  // add data to t_register
                        com.Parameters.AddWithValue("@c_fname", data.c_fname);
                        com.Parameters.AddWithValue("@c_lname", data.c_lname);
                        com.Parameters.AddWithValue("@c_email", data.c_email);
                        com.Parameters.AddWithValue("@c_password", Encrypt(data.c_password));


                        constr.Open();
                        com.ExecuteNonQuery();
                        constr.Close();
                        return true;
                    }
                }
            }
            catch (Exception e)
            {                
                return false;
            }


        }
        //login
        public int GetOne(TrackerFlow.BAL.Models.TblLogin data)
        {

            try
            {
              

                DataTable dt = new DataTable();
                NpgsqlCommand com = new NpgsqlCommand("select * from t_register WHERE c_email = @email AND c_password = @pass", constr);
                com.Parameters.AddWithValue("@email", data.c_email);
                com.Parameters.AddWithValue("@pass", Encrypt(data.c_password));
                //GlobalContext.Properties["SessionID"] = data.c_email;
                constr.Open();

                NpgsqlDataReader datar = com.ExecuteReader();
                //Log.Info("Login Sucessfully");
                if (datar.Read())
                {
                    //constr.Close();

                    if (data.c_RememberMe == true)
                    {
                        // HttpCookie mycookie = new HttpCookie("mycookie");
                        // mycookie["email"] = datar["c_email"].ToString();
                        // mycookie["password"] = Decrypt(datar["c_password"].ToString());
                        // mycookie.Expires = DateTime.Now.AddSeconds(180);
                        // HttpContext.Current.Response.Cookies.Add(mycookie);
                    }

                    return Convert.ToInt32(datar["c_id"].ToString());
                    
                }
                else
                {
                    return 0;
                }
            }
            catch (Exception e)
            {
                
                return 0;
            }
            finally
            {
                constr.Close();
            }

        }



        //For Logout Purpose 
        public string GetDetails(int idd)
        {
            try
            {
                NpgsqlCommand com = new NpgsqlCommand("select * from t_register WHERE c_id = @idd ", constr);
                com.Parameters.AddWithValue("@idd", idd);
                constr.Open();
                NpgsqlDataReader rdr = com.ExecuteReader();                
                string email = "";
                if (rdr.HasRows)
                {
                    rdr.Read();
                    email = rdr["c_email"].ToString();
                }

                constr.Close();
                return email;

            }
            catch (Exception e)
            {                


                return string.Empty;
            }


        }



    }
}



