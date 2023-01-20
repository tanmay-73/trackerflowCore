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
using RabbitMQ.Client;

namespace TrackerFlow.BAL
{
   

    public class CRUDHelper : Helper
    {      
        public List<AssetDisplayMore> GetAll()
        {
            List<AssetDisplayMore> assetsList = new List<AssetDisplayMore>();


            try
            {
                //NpgsqlCommand cmd = new NpgsqlCommand("SELECT c_assetid,c_aid,c_isdeleted FROM t_assets", constr);
                //constr.Open();
                //NpgsqlDataReader dr1 = cmd.ExecuteReader();
                //NpgsqlDataAdapter da = new NpgsqlDataAdapter(cmd);
                //DataSet ds = new DataSet();
                //DataTable dt = new DataTable();
                //dt.Load(dr1);
                //da.Fill(ds, "Assets");
                //List<AssetDisplay> assetList = new List<AssetDisplay>();
                //assetList = (from DataRow dr in dt.Rows 
                //             where Convert.ToBoolean(dr["c_isdeleted"]) == false
                //             select new AssetDisplay()
                //             {
                //                 c_assetid = Convert.ToInt32(dr["c_assetid"]),
                //                 c_aid = Convert.ToInt32(dr["c_aid"])
                //             }).ToList();
                //constr.Close();         
                //List<AssetDisplayMore> assetLists = new List<AssetDisplayMore>();
                //foreach (var raw in assetList)
                //{
                //    constr.Open();
                //    cmd = new NpgsqlCommand("SELECT c_assetname,c_uniqueid,c_date FROM t_assets WHERE c_assetid=@aid and c_isdeleted=@ids ORDER BY c_uniqueid;", constr);
                //    cmd.Parameters.AddWithValue("@aid", raw.c_assetid);
                //    cmd.Parameters.AddWithValue("@ids", false);
                //    NpgsqlDataReader dr11 = cmd.ExecuteReader();
                //    dr11.Read();
                //    string assetname = dr11["c_assetname"].ToString();
                //    string uniqueid = dr11["c_uniqueid"].ToString();
                //    string parsedDate = (dr11["c_date"].ToString()).Split(' ')[0];
                //    //var a=parsedDate.ToShortDateString();
                //    constr.Close();

                //        constr.Open();
                //        cmd = new NpgsqlCommand("SELECT c_floorid,c_areaname from t_area WHERE c_aid=@t_area;", constr);
                //        cmd.Parameters.AddWithValue("@t_area", raw.c_aid);
                //        NpgsqlDataReader dr2 = cmd.ExecuteReader();
                //        dr2.Read();
                //        string c_areaname = dr2["c_areaname"].ToString();
                //        int floorid = Convert.ToInt32(dr2["c_floorid"].ToString());
                //        constr.Close();

                //        constr.Open();
                //        cmd = new NpgsqlCommand("SELECT c_floornum,c_ofcid from t_floor WHERE c_floorid=@floorid", constr);
                //        cmd.Parameters.AddWithValue("@floorid", floorid);
                //        NpgsqlDataReader dr3 = cmd.ExecuteReader();
                //        dr3.Read();
                //        int floornum = Convert.ToInt32(dr3["c_floornum"]);
                //        int officeid = Convert.ToInt32(dr3["c_ofcid"]);
                //        constr.Close();


                //        constr.Open();
                //        cmd = new NpgsqlCommand("SELECT c_ofcname from t_office WHERE c_ofcid=@officeeid", constr);
                //        cmd.Parameters.AddWithValue("@officeeid", officeid);
                //        NpgsqlDataReader dr4 = cmd.ExecuteReader();
                //        dr4.Read();
                //        string officename = (dr3["c_ofcname"].ToString());
                //        assetLists.Add(new AssetDisplayMore { c_assetname = assetname, c_uniqueid = uniqueid, c_areaname = c_areaname, c_floornum = floornum, c_officename = officename, c_date=parsedDate });
                //        constr.Close();
                //    }
                NpgsqlCommand com = new NpgsqlCommand(@"SELECT ar.c_areaname,ar.c_aid,f.c_floornum,f.c_floorid,o.c_ofcname,o.c_ofcid,ass.c_assetid,ass.c_assetname,ass.c_uniqueid,ass.c_date from t_assets ass
                                                    INNER JOIN t_area ar ON ass.c_aid = ar.c_aid
                                                    INNER JOIN t_floor f ON ar.c_floorid = f.c_floorid
                                                    INNER JOIN t_office o ON f.c_ofcid = o.c_ofcid
                                                    WHERE c_isdeleted=false
                                                    ORDER BY ass.c_assetid", constr);
                constr.Open();

                NpgsqlDataReader dr1 = com.ExecuteReader();   

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Load(dr1);
                da.Fill(ds, "Assets");
                assetsList = (from DataRow dr in dt.Rows
                              select new AssetDisplayMore()
                              {
                                  c_assetname = dr["c_assetname"].ToString(),
                                  c_uniqueid = dr["c_uniqueid"].ToString(),
                                  c_floornum = Convert.ToInt32(dr["c_floornum"]),
                                  c_areaname = dr["c_areaname"].ToString(),
                                  c_officename = dr["c_ofcname"].ToString(),
                                  c_aid = Convert.ToInt32(dr["c_aid"]),
                                  c_floorid = Convert.ToInt32(dr["c_floorid"]),
                                  c_ofcid = Convert.ToInt32(dr["c_ofcid"]),
                                  c_date = (dr["c_date"].ToString()).Split(' ')[0]
                              }).ToList();
                return assetsList;
            }
            catch (Exception e)
            {               
                return assetsList;
            }
            finally
            {
                constr.Close();
            }

        }
        //public void addqueue(int id)
        //{
        //    RabbitMQBll obj = new RabbitMQBll();
        //    IConnection con = obj.GetConnection();            
        //    string message = "Are mere bhai Audit ho gaya";
        //    bool flag = obj.send(con, message,id.ToString());            
        //}
        //public string removequeue(int id)
        //{
        //    try
        //    {
        //        RabbitMQBll obj = new RabbitMQBll();
        //        IConnection con = obj.GetConnection();
        //        string message = obj.receive(con, id.ToString());
        //        return message;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}
    }
}

