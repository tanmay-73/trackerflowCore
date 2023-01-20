using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using TrackerFlow.BAL.Models;

namespace TrackerFlow.BAL
{
    public class ForgotHelper : Helper
    {    
        //                Log.Error("Email Failed , Error :- " + e.Message);

        //  Log.Info("InsertExcel Sucessfully");
        public string ExistEmail(string forgotEmail)
        {
            string fname = "";

            try
            {

                NpgsqlCommand comcheck = new NpgsqlCommand(@"SELECT c_email,c_fname FROM t_register WHERE c_email = @c_email ;", constr);
                comcheck.Parameters.AddWithValue("@c_email", forgotEmail);
                constr.Open();
                NpgsqlDataReader rdr = comcheck.ExecuteReader();                
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        fname = rdr["c_fname"].ToString();
                        constr.Close();
                        NpgsqlCommand com = new NpgsqlCommand(@"DELETE FROM t_token WHERE c_registermailid=c_registermailid ;
                                                            INSERT INTO t_token(c_registermailid) VALUES (@c_registermailid)", constr);
                        // First Delete and then add email to c_token for token updation and expiry
                        // because if user click two times forgot password so 2 mails so to delete previous 
                        // otherwise duplicate entries of same mail will be there in c_token table
                        com.Parameters.AddWithValue("@c_registermailid", forgotEmail);
                        constr.Open();
                        com.ExecuteNonQuery();
                        constr.Close();
                    }

                    return fname;
                    //return 1;
                }
                else
                {
                    constr.Close();
                    //return 0;
                    return fname;
                }

            }
            catch (Exception e)
            {                

                return fname;
            }
            finally
            {
                constr.Close();
            }
        }

        public string GenerateToken(string email)
        {
            try
            {
                var allChar = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                var random = new Random();               

                var resultToken = new string(Enumerable.Repeat(allChar, 8).Select(token => token[random.Next(token.Length)]).ToArray());
                //updatetoken(resultToken.ToString(), email);
                updatetoken("FTE7SDS6", email);//FTE7SDS6
                                               //return resultToken.ToString();
                return "FTE7SDS6";
            }
            catch (Exception e)
            {               
                return string.Empty;
            }

        }

        public void updatetoken(string token, string email)
        {

            try
            {
                NpgsqlCommand com = new NpgsqlCommand("UPDATE t_token SET c_token=@authToken , c_timestamp=@c_timestamp where c_registermailid=@c_registermailid", constr);
                Int32 unixTimestamp = (Int32)(DateTime.Now.Subtract(new DateTime(2022, 1, 1)).TotalSeconds);            


                com.Parameters.AddWithValue("@authToken", token);
                com.Parameters.AddWithValue("@c_registermailid", email);
                com.Parameters.AddWithValue("@c_timestamp", unixTimestamp);
                constr.Open();
                com.ExecuteNonQuery();
                constr.Close();
            }
            catch (Exception e)
            {               

            }


        }
        public int Reset(dynamic value, string decryptedToken)
        {

            try
            {

                Int32 newUnixTimestamp = (Int32)(DateTime.Now.Subtract(new DateTime(2022, 1, 1))).TotalSeconds;
                NpgsqlCommand comcheck = new NpgsqlCommand(@"SELECT * FROM t_token WHERE c_token = @c_token ;", constr);
                comcheck.Parameters.AddWithValue("@c_token", decryptedToken);
                constr.Open();

              

                NpgsqlDataReader rdr = comcheck.ExecuteReader();
                string registermailid = "";
                if (rdr.HasRows)
                {
                    while (rdr.Read())
                    {
                        registermailid = rdr["c_registermailid"].ToString();
                        if (newUnixTimestamp - Convert.ToInt32(rdr["c_timestamp"].ToString()) > 1000) // time stamp check for 150 seconds for token expiry
                        {
                            constr.Close();
                            NpgsqlCommand cm2 = new NpgsqlCommand("DELETE FROM t_token WHERE c_registermailid=@c_registermailid", constr);
                            cm2.Parameters.AddWithValue("@c_registermailid", registermailid);
                            constr.Open();
                            cm2.ExecuteNonQuery();
                            constr.Close();
                            return 0;
                        }
                        else if (rdr["c_token"].ToString() != decryptedToken) // Token not matching
                        {
                            constr.Close();
                            return -1;
                        }
                        else
                        {
                            constr.Close();
                            NpgsqlCommand checkpass = new NpgsqlCommand("SELECT * FROM t_register WHERE c_password=@c_password;", constr);
                            checkpass.Parameters.AddWithValue("@c_password", Encrypt(Convert.ToString(value["UpdateForgotPassword"])));
                            constr.Open();
                            NpgsqlDataReader checkrdr = checkpass.ExecuteReader();
                            if (checkrdr.HasRows)
                            {
                                constr.Close();
                                return -2;
                            }
                            constr.Close();
                            NpgsqlCommand cm = new NpgsqlCommand("UPDATE t_register SET c_password=@c_password WHERE c_email=@c_email", constr);
                            cm.Parameters.AddWithValue("@c_password", Encrypt(Convert.ToString(value["UpdateForgotPassword"])));
                            cm.Parameters.AddWithValue("@c_email", registermailid);
                            //cm.Parameters.AddWithValue("@c_token", Convert.ToString(value["Token"])); 
                            NpgsqlCommand cm2 = new NpgsqlCommand("DELETE FROM t_token WHERE c_registermailid=@c_registermailid", constr);
                            cm2.Parameters.AddWithValue("@c_registermailid", registermailid);
                            constr.Open();
                            int ans = cm.ExecuteNonQuery();
                            cm2.ExecuteNonQuery();
                            constr.Close();
                            return 1;
                        }
                    }
                    return 0;
                }
                else
                {
                    constr.Close();
                    return 0;
                }

            }
            catch (Exception e)
            {
              
                return 0;

            }
        }

        public int SendEmail(string email, string fname)
        {
            try
            {              

                //var uname = data.c_fname;
                using (MailMessage mm = new MailMessage("newinterns2022@gmail.com", email))
                {
                    mm.Subject = "Change Password";
                    string generatedToken = GenerateToken(email);
                    string htmlString = string.Empty;

                    string filename = System.AppDomain.CurrentDomain.BaseDirectory + "/ForgetPassword_Email.html";
                    using (StreamReader read = new StreamReader(filename))
                    {
                        htmlString = read.ReadToEnd();
                    }
                    htmlString = htmlString.Replace("#[{FirstName}]#", fname);
                    //htmlString = htmlString.Replace("#[{url}]#", "http://localhost:55006/Sign/Reset?Token="+generatedToken);
                    htmlString = htmlString.Replace("#[{url}]#", "http://trackerflow.clarent.institute:96/Sign/Reset?" + Encrypt(generatedToken));
                    mm.Body =
                      htmlString;
                    // mm.Body = "this is body";
                    mm.Body =
                      htmlString;

                    mm.IsBodyHtml = true;

                    using (SmtpClient smtp = new SmtpClient())
                    {
                        smtp.Host = "smtp.gmail.com";
                        smtp.EnableSsl = true;
                        NetworkCredential networkCred = new NetworkCredential("newinterns2022@gmail.com", "Interns$123");
                        smtp.UseDefaultCredentials = true;
                        smtp.Credentials = networkCred;
                        smtp.Port = 587;
                        smtp.Send(mm);
                        return 1;
                    }
                }
            }
            catch
            {
                return 0;
            }
        }
    }
}
