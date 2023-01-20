using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TrackerFlow.BAL.Models;

namespace TrackerFlow.BAL
{
    public class AuditEmailHelper : Helper
    {   

        public List<AssetModel> Get_Asset(int c_floorid)
        {
            List<AssetModel> auditList = new List<AssetModel>();

            try
            {

                NpgsqlCommand com = new NpgsqlCommand(@"SELECT ass.c_desc, ass.c_assetname,ass.c_uniqueid,ar.c_areaname,f.c_floornum,ass.c_ismissing,TO_CHAR(ass.c_date,'DD-MM-YYYY') AS c_date
                                                    FROM t_audit ass
                                                    inner join t_area ar on ass.c_aid = ar.c_aid
                                                    inner join t_floor f on ar.c_floorid = f.c_floorid
                                                    where f.c_floorid = 1 and ass.c_date = (select max(c_date) from t_audit)
                                                    group by ar.c_areaname,f.c_floornum,ar.c_aid,ass.c_assetname,ass.c_uniqueid,ass.c_ismissing,ass.c_date, ass.c_desc
                                                    order by ar.c_aid", constr);
                //select count(*), c_assetname, c_id from t_assets group by c_assetname where c_aid = @c_aid
                com.Parameters.AddWithValue("@c_floorid", Convert.ToInt32(c_floorid));
                constr.Open();
                NpgsqlDataReader dr1 = com.ExecuteReader();               
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Load(dr1);
                da.Fill(ds, "Assets");
                auditList = (from DataRow dr in dt.Rows
                             select new AssetModel()
                             {
                                 c_assetname = dr["c_assetname"].ToString(),
                                 c_uniqueid = dr["c_uniqueid"].ToString(),
                                 c_desc = dr["c_desc"].ToString(),
                                 c_areaname = dr["c_areaname"].ToString(),
                                 c_floornum = Convert.ToInt32(dr["c_floornum"]),
                                 //c_floorid = Convert.ToInt32(dr["c_floorid"]),
                                 c_ismissing = Convert.ToBoolean(dr["c_ismissing"]),
                                 c_date = dr["c_date"].ToString()

                             }).ToList();
                return auditList;
            }
            catch (Exception e)
            {                
                return auditList;
            }
            finally
            {
                constr.Close();
            }
        }
        public dynamic AllMails(dynamic mails)
        {
            return mails;
        }

        public void EmailPdf(dynamic data)
        {
            int id = Convert.ToInt32(data["idd"]);
            string strhtml = string.Empty;
            NpgsqlConnection conn = new NpgsqlConnection("Server=10.20.0.31;Port=5432; User Id=postgres;Database=trackerflow;Password=Tanmay73");
            try
            {
                NpgsqlCommand cmd = new NpgsqlCommand(@"SELECT ass.c_assetname,ass.c_uniqueid,ar.c_areaname,f.c_floornum,ass.c_ismissing,ass.c_date,o.c_ofcname,ass.c_desc
                                                        FROM t_audit ass
                                                        inner join t_area ar on ass.c_aid = ar.c_aid
                                                        inner join t_floor f on ar.c_floorid = f.c_floorid
                                                        inner join t_office o on o.c_ofcid= f.c_ofcid
                                                        where f.c_floorid = @id and ass.c_date = (select max(c_date) from t_audit)
                                                        group by ar.c_areaname,f.c_floornum,ar.c_aid,ass.c_assetname,ass.c_uniqueid,ass.c_ismissing,ass.c_date,o.c_ofcname,ass.c_desc
                                                        order by ar.c_aid
                                                        ", conn);
                cmd.Parameters.AddWithValue("@id", Convert.ToInt32(id));
                conn.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();

                if (reader.HasRows)
                {
                    reader.Read();
                    string date = reader["c_date"].ToString().Split(' ')[0];
                    string office = reader["c_ofcname"].ToString();
                    string floor = reader["c_floornum"].ToString();

                    strhtml = "<div style='width: 100%'>"
                        + "<table border='1'>"
                            + "<tr bgcolor='#7E57C2' style= 'display:flex;justify-content:center;height:60px;text-align: center; width:100%'>"
                             + "<th style='color:#fff;height: 60px;'><h1>Audit</h1>Date:" + date + "  " + "Office: " + office + "  " + "Floor : " + floor + "</th>"

                            + "</tr>"
                         + "</table>"
                        + "<table border='1'>"
                            + "<tr>"
                             + "<th style='width: 60%;'><b>Area</b>" + "</th>"
                             + "<th style='width: 60%;'><b>Asset</b>" + "</th>"
                             + "<th style='width: 60%;'><b>Asset Id</b>" + "</th>"
                             + "<th style='width: 60%;'><b>Status</b>" + "</th>"
                             + "<th style='width: 60%;'><b>Notes</b>" + "</th>"
                          + "</tr>"
                         + "</table>"
                         + "</div>";

                    do
                    {


                        strhtml = strhtml + "<div style='width: 100%;'>"
                                         + "<table border='1' padding='30'>"
                                                + "<tr>"                                                                                                  
                                                 + "<td style='width: 40%;float:right;'>" + reader["c_areaname"].ToString() + "</ td > "
                                                 + "<td style='width: 40%;float:right;'>" + reader["c_assetname"].ToString() + "</ td > "
                                                 + "<td style='width: 40%;float:right;' >" + reader["c_uniqueid"].ToString() + "</ td > "
                                                 + "<td style='width: 40%;float:right;' >" + reader["c_ismissing"].ToString() + "</ td > "
                                                 + "<td style='width: 40%;float:right;' >" + reader["c_desc"].ToString() + "</ td > "

                                                 + "</tr>"

                                          + "</table>"
                                          + "</div>";

                    } while (reader.Read());

                    conn.Close();
                    // iTextSharp.text.Rectangle page = new iTextSharp.text.Rectangle(PageSize.A4);
                    // var fileName = "Audit_.pdf";
                    // var path = HttpContext.Current.Server.MapPath("~/EmailAudit/" + fileName);
                    // File.Delete(path);

                    // using (FileStream file = new FileStream(path, FileMode.CreateNew, FileAccess.Write, FileShare.None))
                    // {

                    //     Document pdf = new Document(page, 30, 30, 60, 60);
                    //     PdfWriter writer = PdfWriter.GetInstance(pdf, file);

                    //     pdf.Open();
                    //     HTMLWorker hw = new HTMLWorker(pdf);
                    //     hw.Parse(new StringReader(strhtml));
                    //     writer.CloseStream = false;
                    //     pdf.Close();

                    // }

                    // foreach (string c in data["emails"])
                    // {
                    //     SendAuditPDF(fileName, c);
                    // }
                    //BALApproveStatus helper = new BALApproveStatus();
                    //    for (int i = 0; i < data["emails"].length; i++)
                    //{
                    //    SendAuditPDF(fileName,data["emails"][0]);
                    //}

                }

            }
            catch (Exception ex)
            {
            }           
        }
        public void SendAuditPDF(string fileName, string sendto)
        {
            try
            {
                //dynamic mails = AllMails();
                //string to = "anjali.mistry05@gmail.com";
                string to = sendto;
                string subject = "Audit Data";
                //var path = HttpContext.Current.Server.MapPath("~/EmailAudit/" + fileName);
                MailMessage mm = new MailMessage();
                mm.From = new MailAddress("gauravsonwane3255@gmail.com");
                mm.To.Add(to);
                mm.Subject = subject;
                mm.Body = "";
                // mm.Attachments.Add(new Attachment(path));
                //mm.Attachments.Add(new Attachment(path));
                mm.IsBodyHtml = true;
                SmtpClient smtp = new SmtpClient("smtp.gmail.com");
                smtp.UseDefaultCredentials = true;
                smtp.Port = 587;
                smtp.EnableSsl = true;
                smtp.Credentials = new System.Net.NetworkCredential("gauravsonwane3255@gmail.com", "Gaurav@1234");
                smtp.Send(mm);
            }
            catch (Exception e)
            {                
              
            }
        }

        public List<EmailSetting> Get_Email()
        {
            List<EmailSetting> emailList = new List<EmailSetting>();

            try
            {
                NpgsqlCommand com = new NpgsqlCommand("select * from t_email", constr);
                constr.Open();
                NpgsqlDataReader dr1 = com.ExecuteReader();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);             
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Load(dr1);
                da.Fill(ds, "Email");
                emailList = (from DataRow dr in dt.Rows
                             select new EmailSetting()
                             {
                                 c_eid = Convert.ToInt32(dr["c_eid"]),
                                 c_email = dr["c_email"].ToString()
                             }).ToList();
                return emailList;
            }
            catch (Exception e)
            {                
                return emailList;
            }
            finally
            {
                constr.Close();
            }
        }


    }
}
