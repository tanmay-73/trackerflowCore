using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerFlow.BAL.Models;

namespace TrackerFlow.BAL
{
    public class DashBoardHelper : Helper
    {
        
        public DashBoard AssetCount()
        {


            NpgsqlCommand com = new NpgsqlCommand("SELECT COUNT(*) AS c_assetcount FROM t_assets WHERE c_isdeleted = false", constr);
            constr.Open();
            NpgsqlDataReader dr1 = com.ExecuteReader();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Load(dr1);
            da.Fill(ds, "DashBoard");          

            DashBoard count = (from DataRow dr in dt.Rows
                               select new DashBoard()
                               {
                                   c_assetcount = dr["c_assetcount"].ToString(),
                               }).ToList().FirstOrDefault();

            return count;
        }




        public DashBoard OfficeCount()
        {

            NpgsqlCommand com = new NpgsqlCommand("SELECT COUNT(*) AS c_officecount FROM t_office", constr);
            constr.Open();
            NpgsqlDataReader dr1 = com.ExecuteReader();
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            dt.Load(dr1);
            da.Fill(ds, "DashBoard");

            DashBoard count = (from DataRow dr in dt.Rows
                               select new DashBoard()
                               {
                                   c_officecount = dr["c_officecount"].ToString(),
                               }).ToList().FirstOrDefault();
            return count;
        }

        public List<DashBoard> GetCol()
        {
            List<DashBoard> pieList = new List<DashBoard>();

            try
            {
                NpgsqlCommand com = new NpgsqlCommand(@"SELECT 
                                                        COUNT(CASE WHEN f.c_floorid=1 THEN ass.c_assetname END) AS casepoint2,
                                                        COUNT(CASE WHEN f.c_floorid=2 THEN ass.c_assetname END) AS casepoint4,
                                                        COUNT(CASE WHEN f.c_floorid=3 THEN ass.c_assetname END) AS clarent
                                                        FROM t_assets ass
                                                        INNER JOIN t_area ar ON ass.c_aid=ar.c_aid
                                                        INNER JOIN t_floor f ON ar.c_floorid=f.c_floorid
                                                        WHERE ass.c_isdeleted=false
                                                        ", constr);
                constr.Open();
                NpgsqlDataReader dr1 = com.ExecuteReader();                

                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Load(dr1);
                da.Fill(ds, "DashBoard");
                pieList = (from DataRow dr in dt.Rows
                           select new DashBoard()
                           {
                               casepoint2 = Convert.ToInt32(dr["casepoint2"]),
                               casepoint4 = Convert.ToInt32(dr["casepoint4"]),
                               clarent = Convert.ToInt32(dr["clarent"]),
                           }).ToList();
                return pieList;
            }
            catch (Exception e)
            {
                
                return pieList;
            }


        }

        public List<DashBoard> GetLine()
        {
            List<DashBoard> lineList = new List<DashBoard>();

            try
            {

                DashBoard db = new DashBoard();
                NpgsqlCommand com = new NpgsqlCommand(@"SELECT 
                                                    COUNT(CASE WHEN f.c_floorid=1 THEN au.c_assetname END) AS casepoint2,
                                                    COUNT(CASE WHEN f.c_floorid=2 THEN au.c_assetname END) AS casepoint4,
                                                    COUNT(CASE WHEN f.c_floorid=3 THEN au.c_assetname END) AS clarent,
                                                    TO_CHAR(au.c_date,'MM-DD-YYYY') AS c_date 
                                                    FROM t_audit au
                                                    INNER JOIN t_area ar ON au.c_aid=ar.c_aid
                                                    INNER JOIN t_floor f ON ar.c_floorid=f.c_floorid
                                                    WHERE au.c_ismissing IS FALSE
                                                    GROUP BY au.c_date
                                                    ORDER BY au.c_date DESC LIMIT 5", constr);
                constr.Open();
                NpgsqlDataReader dr1 = com.ExecuteReader();


                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Load(dr1);
                da.Fill(ds, "DashBoard");
                lineList = (from DataRow dr in dt.Rows
                            select new DashBoard()
                            {
                                casepoint2 = Convert.ToInt32(dr["casepoint2"]),
                                casepoint4 = Convert.ToInt32(dr["casepoint4"]),
                                clarent = Convert.ToInt32(dr["clarent"]),
                                c_date = dr["c_date"].ToString()
                            }).ToList();
                return lineList;
            }
            catch (Exception e)
            {
                
                return lineList;

            }

        }

        public List<DashBoard> GetGrid()
        {
            List<DashBoard> gridList = new List<DashBoard>();

            try
            {

                NpgsqlCommand com = new NpgsqlCommand(@"SELECT o.c_ofcname,f.c_floornum,ar.c_areaname,
                                                        COUNT(ass.c_uniqueid) AS c_assetcount,
                                                        CASE WHEN COUNT(CASE WHEN au.c_ismissing IS TRUE AND au.c_date = (SELECT MAX(c_date) FROM t_audit) THEN au.c_uniqueid END) = 0         
                                                                THEN NULL			   
                                                                   ELSE COUNT(CASE WHEN au.c_ismissing IS TRUE AND au.c_date = (SELECT MAX(c_date) FROM t_audit) THEN au.c_uniqueid END) END AS c_a_asset,
                                                        CASE WHEN COUNT(CASE WHEN au.c_ismissing IS FALSE AND au.c_date = (SELECT MAX(c_date) FROM t_audit) THEN au.c_uniqueid END) = 0 
                                                                THEN NULL
                                                                   ELSE COUNT(CASE WHEN au.c_ismissing IS FALSE AND au.c_date = (SELECT MAX(c_date) FROM t_audit) THEN au.c_uniqueid END) END AS c_l_asset,
                                                        CASE WHEN COUNT(CASE WHEN ass.c_isdeleted IS FALSE AND ass.c_date > (SELECT MAX(c_date) FROM t_audit) THEN 'new' END) = 0 
                                                                THEN NULL
                                                                   ELSE COUNT(CASE WHEN ass.c_isdeleted IS FALSE AND ass.c_date > (SELECT MAX(c_date) FROM t_audit) THEN 'new' END) END AS new_item,
                                                        CASE WHEN COUNT(
                                                                        CASE WHEN au.c_ismissing IS FALSE AND au.c_date = (SELECT MAX(c_date) FROM t_audit) THEN au.c_uniqueid 
                                                                             WHEN au.c_ismissing IS TRUE AND au.c_date = (SELECT MAX(c_date) FROM t_audit) THEN au.c_uniqueid
                                                                             WHEN ass.c_isdeleted IS FALSE AND ass.c_date > (SELECT MAX(c_date) FROM t_audit) THEN 'new' END
                                                                       ) != COUNT(ass.c_uniqueid) 
                                                                THEN 'Incomplete' ELSE 'Complete' END AS c_status
                                                        FROM t_assets ass
                                                        FULL OUTER JOIN 
                                                            (
                                                            SELECT c_ismissing,c_uniqueid,c_date FROM t_audit 
                                                                where c_date = (SELECT MAX(c_date) FROM t_audit) 
                                                            ) au ON ass.c_uniqueid = au.c_uniqueid
                                                        INNER JOIN t_area ar ON ass.c_aid = ar.c_aid
                                                        INNER JOIN t_floor f ON ar.c_floorid = f.c_floorid
                                                        INNER JOIN t_office o ON f.c_ofcid = o.c_ofcid
                                                        WHERE ass.c_isdeleted IS FALSE
                                                        GROUP BY ar.c_areaname,f.c_floornum,o.c_ofcname
                                                        ORDER BY f.c_floornum;", constr);
                constr.Open();
                NpgsqlDataReader dr1 = com.ExecuteReader();
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(com);
                DataSet ds = new DataSet();
                DataTable dt = new DataTable();
                dt.Load(dr1);
                da.Fill(ds, "DashBoard");
                gridList = (from DataRow dr in dt.Rows
                            select new DashBoard()
                            {
                            c_areaname = dr["c_areaname"].ToString(),
                            c_floornum = dr["c_floornum"].ToString(),
                            c_ofcname = dr["c_ofcname"].ToString(),
                            c_assetcount = dr["c_assetcount"].ToString(),
                            c_a_asset = dr["c_a_asset"].ToString(),
                            c_l_asset = dr["c_l_asset"].ToString(),
                            new_item = dr["new_item"].ToString(),
                            c_status = dr["c_status"].ToString()
                            }).ToList();
                return gridList;

            }
            catch (Exception e)
            {
                
                return gridList;

            }
        }
    }
}
