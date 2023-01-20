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
    public class AuditHelper : Helper
    {        
        public List<Office> Get_Office()
        {
            List<Office> officeList = new List<Office>();

            try
            {
                NpgsqlCommand com = new NpgsqlCommand("select * from t_office", constr);
                constr.Open();
                NpgsqlDataReader dr1 = com.ExecuteReader();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);               
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Load(dr1);
                da.Fill(ds, "Office");
                officeList = (from DataRow dr in dt.Rows
                              select new Office()
                              {
                                  c_ofcid = Convert.ToInt32(dr["c_ofcid"]),
                                  c_ofcname = dr["c_ofcname"].ToString()
                              }).ToList();
                return officeList;
            }
            catch (Exception e)
            {              
                return officeList;
            }
            finally
            {
                constr.Close();
            }
        }

        public List<Floor> Get_floor(int c_ofcid)
        {
            List<Floor> floorList = new List<Floor>();


            try
            {

                NpgsqlCommand com = new NpgsqlCommand("select * from t_floor where c_ofcid=@c_ofcid", constr);
                com.Parameters.AddWithValue("@c_ofcid", Convert.ToInt32(c_ofcid));
                constr.Open();
                NpgsqlDataReader dr1 = com.ExecuteReader();

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);                


                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Load(dr1);
                da.Fill(ds, "Floor");


                floorList = (from DataRow dr in dt.Rows
                             select new Floor()
                             {
                                 c_floorid = Convert.ToInt32(dr["c_floorid"]),
                                 c_floornum = Convert.ToInt32(dr["c_floornum"]),
                                 c_ofcid = Convert.ToInt32(dr["c_ofcid"])
                             }).ToList();
                return floorList;
            }
            catch (Exception e)
            {                
                return floorList;
            }
            finally
            {
                constr.Close();
            }
        }

        public List<Area> Get_area(int c_floorid)
        {
            List<Area> areaList = new List<Area>();
            try
            {

                NpgsqlCommand com = new NpgsqlCommand("select * from t_area where c_floorid=@c_floorid", constr);
                com.Parameters.AddWithValue("@c_floorid", Convert.ToInt32(c_floorid));
                constr.Open();
                NpgsqlDataReader dr1 = com.ExecuteReader();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Load(dr1);
                da.Fill(ds, "Area");                

                areaList = (from DataRow dr in dt.Rows
                            select new Area()
                            {
                                c_aid = Convert.ToInt32(dr["c_aid"]),
                                c_areaname = dr["c_areaname"].ToString(),
                                c_floorid = Convert.ToInt32(dr["c_floorid"])
                            }).ToList();
                return areaList;
            }
            catch (Exception e)
            {                
                return areaList;
            }
            finally
            {
                constr.Close();
            }
        }
        public List<AssetModel> Get_Asset(int c_aid)
        {
            try
            {
                //NpgsqlCommand com = new NpgsqlCommand("SELECT c_assetid,c_assetname,c_uniqueid,c_aid FROM t_assets where c_aid=@c_aid", constr);
                //NpgsqlCommand com = new NpgsqlCommand("SELECT * FROM t_audit where c_aid=@c_aid", constr);

                //select count(*), c_assetname, c_id from t_assets group by c_assetname where c_aid = @c_aid
                //com.Parameters.AddWithValue("@c_aid", Convert.ToInt32(c_aid));
                NpgsqlCommand com = new NpgsqlCommand(@"SELECT c_auditid,c_assetname, c_uniqueid, c_aid, c_date, c_desc, 'audit' as c_status, c_ismissing
                                                    FROM t_audit
                                                    WHERE c_date = (SELECT MAX(c_date) FROM t_audit) AND c_aid = @aid
                                                    UNION ALL
                                                    SELECT c_assetid,c_assetname, c_uniqueid, c_aid, c_date,c_desc, 'asset' as c_status, c_isdeleted
                                                    FROM t_assets
                                                    WHERE c_uniqueid not in (SELECT  c_uniqueid FROM t_audit
                                                    WHERE c_date = (SELECT MAX(c_date) FROM t_audit) AND c_aid = @aid)  AND c_aid = @aid AND c_isdeleted = false
                                                    ORDER By c_aid", constr);
                com.Parameters.AddWithValue("@aid", c_aid);
                com.Parameters.AddWithValue("@del", false);
                constr.Open();
                NpgsqlDataReader dr1 = com.ExecuteReader();                

                if (dr1.HasRows)
                {
                    NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                    DataSet ds = new DataSet();
                    DataTable dt = new DataTable();
                    dt.Load(dr1);
                    da.Fill(ds, "Assets");
                    List<AssetModel> assetList = new List<AssetModel>();
                    assetList = (from DataRow dr in dt.Rows
                                 select new AssetModel()
                                 {
                                     c_assetname = dr["c_assetname"].ToString(),
                                     c_uniqueid = dr["c_uniqueid"].ToString(),
                                     c_desc = dr["c_desc"].ToString(),
                                     // c_ismissing 
                                     //c_date =dr["c_date"].ToString(),
                                     c_status = dr["c_status"].ToString(),
                                     c_ismissing = Convert.ToBoolean(dr["c_ismissing"].ToString()),
                                     c_aid = Convert.ToInt32(dr["c_aid"])
                                 }).ToList();
                    return assetList;
                }
                else
                {
                    constr.Close();
                    constr.Open();
                    NpgsqlCommand com1 = new NpgsqlCommand(@"SELECT *
                                                        FROM t_assets WHERE c_aid = @aid AND c_isdeleted= @del ", constr);
                    com1.Parameters.AddWithValue("@aid", c_aid);
                    com1.Parameters.AddWithValue("@del", false);
                    NpgsqlDataReader dr11 = com.ExecuteReader();

                    NpgsqlDataAdapter da1 = new NpgsqlDataAdapter(com1);
                    DataSet ds1 = new DataSet();
                    DataTable dt1 = new DataTable();
                    dt1.Load(dr11);
                    da1.Fill(ds1, "New Assets");
                    List<AssetModel> assetList = new List<AssetModel>();
                    assetList = (from DataRow dr in dt1.Rows
                                 select new AssetModel()
                                 {
                                     c_assetname = dr["c_assetname"].ToString(),
                                     c_uniqueid = dr["c_uniqueid"].ToString(),
                                     //c_desc = dr["c_desc"].ToString(),
                                     // c_ismissing 
                                     c_status = dr["c_status"].ToString(),
                                     //c_ismissing = Convert.ToBoolean(dr["c_ismissing"].ToString()),
                                     c_aid = Convert.ToInt32(dr["c_aid"])
                                 }).ToList();
                    return assetList;

                }
            }
            catch (Exception e)
            {
                List<AssetModel> assetList = new List<AssetModel>();                
                return assetList;
            }

            finally
            {
                constr.Close();
            }
        }
        //public void SendEmail()
        //{
        //    SmtpSection smtpSection = (SmtpSection)ConfigurationManager.GetSection("system.net/mailSettings/smtp");


        //    string email = "gauravsonvane3255@gmail.com";
        //    string pass = "Gaurav@1234";
        //    using (MailMessage mm = new MailMessage(email, "pateltanmay73@gmail.com"))
        //    {
        //        mm.Subject = "Audit";
        //        string htmlString = string.Empty;
        //        var filename = "The Audit #[{date}]# is succesfull at #[{time}]#";

        //        DateTime now = DateTime.Today;
        //        string time = DateTime.Now.ToShortTimeString();
        //        filename = filename.Replace("#[{date}]#", now.ToString("dd/MM/yyyy"));
        //        filename = filename.Replace("#[{time}]#", time.ToString());
        //        mm.Body =
        //         filename;
        //        // mm.Body = "this is body";
        //        mm.IsBodyHtml = true;
        //        using (SmtpClient smtp = new SmtpClient())
        //        {
        //            smtp.Host = "smtp.gmail.com";
        //            smtp.EnableSsl = true;
        //            NetworkCredential networkCred = new NetworkCredential(email, pass);
        //            smtp.UseDefaultCredentials = true;
        //            smtp.Credentials = networkCred;
        //            smtp.Port = 587;
        //            smtp.Send(mm);

        //            //smtp.Host = smtpSection.Network.Host;
        //            //smtp.EnableSsl = smtpSection.Network.EnableSsl;
        //            //NetworkCredential networkCred = new NetworkCredential(smtpSection.Network.UserName, smtpSection.Network.Password);
        //            //smtp.UseDefaultCredentials = true;
        //            //smtp.Credentials = networkCred;
        //            //smtp.Port = smtpSection.Network.Port;
        //            //smtp.Send(mm);
        //        }
        //    }

        //    // return "Email sent sucessfully.";

        //}

        public bool Audit_insert(Models.Root data)
        {
            try
            {

                constr.Open();
                foreach (var row in data.assetlist)
                {
                    NpgsqlCommand com = new NpgsqlCommand(@"INSERT INTO t_audit(c_assetname,c_uniqueid,c_aid, c_desc, c_ismissing, c_date) 
                                                    VALUES (@c_assetname,@c_uniqueid,@c_aid, @c_desc, @c_ismissing, @c_date)", constr);  // add data to t_register
                    com.Parameters.AddWithValue("@c_assetname", row.c_assetname);
                    com.Parameters.AddWithValue("@c_uniqueid", row.c_uniqueid);
                    com.Parameters.AddWithValue("@c_aid", row.c_aid);

                    if (row.c_desc.ToString() != string.Empty)
                    {
                        com.Parameters.AddWithValue("@c_ismissing", false);
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@c_ismissing", true);
                    }
                    //com.Parameters.AddWithValue("@c_status", row.c_status);
                    if (row.c_desc == null)
                    {
                        com.Parameters.AddWithValue("@c_desc", "");
                    }
                    else
                    {
                        com.Parameters.AddWithValue("@c_desc", row.c_desc);
                    }
                    var now = DateTime.Now.ToString("yyyy'-'MM'-'dd");
                    DateTime date = DateTime.Parse(now);
                    com.Parameters.AddWithValue("@c_date", date);                    

                    com.ExecuteNonQuery();
                }
                constr.Close();
                return true;
            }
            catch (Exception e)
            {
                
                return false;

            }
            finally
            {
                constr.Close();
            }
        }


        //public string Get_date()
        //{
        //    try
        //    {
        //        NpgsqlCommand com = new NpgsqlCommand(@"SELECT c_date FROM t_audit WHERE c_auditid = (SELECT MAX(c_auditid) FROM t_audit)", constr);
        //        constr.Open();
        //        NpgsqlDataReader datar = com.ExecuteReader();
        //        if (datar.Read())
        //        {
        //            var date = datar["c_date"].ToString();
        //            constr.Close();
        //            return date;
        //        }
        //        else
        //        {
        //            return "Date Not Found";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}


        //public List<Audit> Get_audit(Audit data)
        //{
        //    //try
        //    //{
        //    var date = "";
        //    NpgsqlCommand com1 = new NpgsqlCommand("SELECT c_date FROM t_audit WHERE c_auditid= (SELECT MAX(c_auditid) FROM t_audit)", constr);
        //    constr.Open();
        //    NpgsqlDataReader datar1 = com1.ExecuteReader();
        //    //if (datar.Read())
        //    //{
        //    date = datar1["c_date"].ToString();

        //    NpgsqlCommand com = new NpgsqlCommand("SELECT * FROM t_audit WHERE c_date = @date and c_aid =@aid");
        //    com.Parameters.AddWithValue("@date", date);
        //    com.Parameters.AddWithValue("@aid", data.c_aid);

        //    NpgsqlDataReader datar = com.ExecuteReader();
        //    if (datar.Read())
        //    {
        //        return false;
        //    }
        //    else
        //    {


        //        DataTable dt = new DataTable();
        //        constr.Close();

        //        NpgsqlCommand com2 = new NpgsqlCommand("SELECT * FROM t_audit WHERE c_aid = @aid and c_date = @date ", constr);
        //        com2.Parameters.AddWithValue("@aid", data.c_aid);
        //        com2.Parameters.AddWithValue("@aid", data.c_date);
        //        constr.Open();
        //        NpgsqlDataReader datar2 = com.ExecuteReader();
        //        List<Audit> auditList = new List<Audit>();
        //        if (datar2.Read())
        //        {
        //            dt.Load(datar2);

        //            auditList = (from DataRow dr in dt.Rows
        //                         where Convert.ToDateTime(dr["c_date"].ToString()) == Convert.ToDateTime(data.c_date)
        //                         select new Audit()
        //                         {
        //                             c_assetname = dr["c_assetname"].ToString(),
        //                             c_uniqueid = dr["c_uniqueid"].ToString(),
        //                             c_desc = dr["c_desc"].ToString(),
        //                             c_ismissing = Convert.ToBoolean(dr["c_ismissing"]),
        //                             c_aid = Convert.ToInt32(dr["c_aid"])
        //                         }).ToList();
        //            constr.Close();

        //        }
        //        return auditList;
        //    }

        //}


        //public List<AssetModel> Get_assets()
        //{
        //    try
        //    {
        //        var date = "";
        //        DataTable dt = new DataTable();
        //        NpgsqlCommand com = new NpgsqlCommand("SELECT c_date FROM t_audit WHERE c_auditid= (SELECT MAX(c_auditid) FROM t_audit)", constr);
        //        constr.Open();
        //        NpgsqlDataReader datar = com.ExecuteReader();
        //        //if (datar.Read())
        //        //{
        //        date = datar["c_date"].ToString();
        //        constr.Close();

        //        NpgsqlCommand com2 = new NpgsqlCommand("SELECT * FROM t_assets ", constr);
        //        constr.Open();
        //        NpgsqlDataReader datar2 = com.ExecuteReader();
        //        List<AssetModel> asset = new List<AssetModel>();
        //        if (datar2.Read())
        //        {
        //            dt.Load(datar2);

        //            asset = (from DataRow dr in dt.Rows
        //                     where dr["c_date"].ToString() == date
        //                     select new AssetModel()
        //                     {
        //                         c_assetname = dr["c_assetname"].ToString(),
        //                         c_uniqueid = dr["c_uniqueid"].ToString(),
        //                         c_aid = Convert.ToInt32(dr["c_aid"])
        //                     }).ToList();
        //            constr.Close();

        //        }
        //        return asset;

        //    }
        //    catch (Exception ex)
        //    {
        //        List<AssetModel> asset = new List<AssetModel>();
        //        return asset;  
        //    }
        //}


        //1
        public DateTime Get_last_date(int c_aid)
        {

            DateTime date = new DateTime();

            try
            {

                NpgsqlCommand com = new NpgsqlCommand("SELECT MAX(c_date) as c_date FROM t_audit WHERE c_aid= @aid", constr);
                com.Parameters.AddWithValue("@aid", c_aid);
                constr.Open();
                NpgsqlDataReader datar = com.ExecuteReader();                

                if (datar.Read())
                {
                    //string date1 = datar["c_date"].ToString();
                    date = Convert.ToDateTime(datar["c_date"].ToString());
                    constr.Close();
                    return date;
                }
                else
                {
                    constr.Close();
                    return date;
                }
            }
            catch (Exception e)
            {              
                return date;

            }
            finally
            {
                constr.Close();
            }

        }
        public bool updateStatus(dynamic value)
        {
            try
            {
                constr.Open();
                NpgsqlCommand com = new NpgsqlCommand(@"UPDATE t_audit SET c_ismissing=true WHERE c_uniqueid=@uniid", constr);
                com.Parameters.AddWithValue("@uniid", value.ToString());
                com.ExecuteNonQuery();
                constr.Close();
                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                constr.Close();
            }
        }
    }
}