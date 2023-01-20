using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using TrackerFlow.BAL.Models;

namespace TrackerFlow.BAL
{
    public class AssetHelper : Helper
    {         

        public bool InsertExcel(List<InsertExcel> data)
        {

            try
            {
                //DataTable dt = new DataTable();
                //dt.Load(data);
                constr.Open();               

                foreach (var row in data)
                {
                    NpgsqlCommand com = new NpgsqlCommand(@"SELECT * FROM t_assets WHERE c_uniqueid=@c_uniqueid;", constr);  // check if row exists
                    com.Parameters.AddWithValue("@c_uniqueid", row.c_uniqueid);
                    //NpgsqlDataReader datadr = comcheck.ExecuteReader();
                    using (NpgsqlDataReader datadr = com.ExecuteReader())
                    {
                        if (datadr.HasRows)
                        {
                            continue;
                        }
                    }
                    //com = new NpgsqlCommand(@"INSERT INTO t_asset(c_assestid,c_assestname,c_uniqueid,c_ofcid,c_floorid,c_aid,c_date) 
                    //                                    VALUES (@c_assestid,@c_assestname,@c_uniqueid,@c_ofcid,@c_floorid,@c_aid,@c_date)", constr);  // add data to t_register
                    //com = new NpgsqlCommand(@"(Select c_aid From t_area
                    //                                                                                               WHERE c_floorid =
                    //                                                                                               (Select c_floornum from t_floor
                    //                                                                                               WHERE c_ofcid = (SELECT c_ofcid
                    //                                                                                               FROM t_office where c_ofcname = 'Casepoint' ) and c_floornum = 2) and c_areaname = 'Conference Room1') );
                    //", constr);  // add data to t_register
                    //com = new NpgsqlCommand(@"Select c_aid From t_area
                    //                            WHERE c_floorid =
                    //                            (Select c_floornum from t_floor
                    //                            WHERE c_ofcid = (SELECT c_ofcid
                    //                            FROM t_office where c_ofcname =@c_ofcname  ) and c_floornum = @c_floornum) and c_areaname =@c_areaname", constr);
                    com = new NpgsqlCommand(@"SELECT c_ofcid  FROM t_office where c_ofcname =@c_ofcname ;", constr);
                    com.Parameters.AddWithValue("@c_ofcname", row.c_officename);
                    NpgsqlDataReader dr1 = com.ExecuteReader();
                    dr1.Read();
                    int office_id = Convert.ToInt32(dr1["c_ofcid"].ToString());
                    constr.Close();
                    constr.Open();
                    com = new NpgsqlCommand(@"Select c_floorid from t_floor WHERE c_ofcid =@office_id and c_floornum = @c_floornum;", constr);
                    com.Parameters.AddWithValue("@office_id", office_id);
                    com.Parameters.AddWithValue("@c_floornum", row.c_floornumber);
                    dr1 = com.ExecuteReader();
                    dr1.Read();
                    int floor_id = Convert.ToInt32(dr1["c_floorid"].ToString());
                    constr.Close();
                    constr.Open();
                    com = new NpgsqlCommand(@"Select c_aid from t_area WHERE c_floorid =@floor_id and c_areaname = @c_areaname;", constr);
                    com.Parameters.AddWithValue("@floor_id", floor_id);
                    com.Parameters.AddWithValue("@c_areaname", row.c_areaname);
                    dr1 = com.ExecuteReader();
                    dr1.Read();
                    int area_id = Convert.ToInt32(dr1["c_aid"].ToString());
                    constr.Close();
                    constr.Open();
                    com = new NpgsqlCommand(@"Insert into t_assets(c_assetname, c_uniqueid, c_aid , c_date,c_isdeleted) values(@c_assetname ,@c_uniqueid ,@area_id,@c_date,@c_deleted);", constr);  // add data to t_register
                    com.Parameters.AddWithValue("@c_assetname", row.c_assestname);
                    com.Parameters.AddWithValue("@c_deleted", false);
                    com.Parameters.AddWithValue("@c_uniqueid", row.c_uniqueid);
                    com.Parameters.AddWithValue("@area_id", area_id);
                    DateTime parsedDate = DateTime.Parse(row.c_date);
                    com.Parameters.AddWithValue("@c_date", parsedDate);
                    com.ExecuteNonQuery();
                    //DateTime d = Convert.ToDateTime(row.c_date);
                    //com.Parameters.AddWithValue("@c_date", d);
                    // com.Parameters.AddWithValue("@c_time", row.c_time);
                    //com.ExecuteNonQuery();

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
        public bool UAsset(InsertAsset row)
        {
            try
            {
                constr.Open();

                NpgsqlCommand com = new NpgsqlCommand(@"Update t_assets set c_assetname=@c_assetname, c_aid=@area_id,c_date=@c_date WHERE c_uniqueid=@c_uniqueid ", constr);  // add data to t_register
                com.Parameters.AddWithValue("@c_assetname", row.c_assetname.Trim());
                com.Parameters.AddWithValue("@c_uniqueid", row.c_uniqueid.Trim());
                com.Parameters.AddWithValue("@area_id", row.c_areaid);
                if (row.c_date != null)
                {
                    DateTime parsedDate = DateTime.Parse(row.c_date);
                    com.Parameters.AddWithValue("@c_date", parsedDate);
                }                

                com.ExecuteNonQuery();
                constr.Close();
                return true;
            }
            catch (Exception e )
            {                
                return false;

            }
            finally
            {
                constr.Close();
            }

        }
        public bool IAsset(InsertAsset row)
        {

            try
            {
                //DataTable dt = new DataTable();
                //dt.Load(data);
                constr.Open();
                NpgsqlCommand com = new NpgsqlCommand(@"SELECT * FROM t_assets WHERE c_uniqueid=@c_uniqueid;", constr);  // check if row exists
                com.Parameters.AddWithValue("@c_uniqueid", row.c_uniqueid.Trim());
                //NpgsqlDataReader datadr = comcheck.ExecuteReader();
                using (NpgsqlDataReader datadr = com.ExecuteReader())
                {
                    if (datadr.HasRows)
                    {
                        return false;
                    }
                }
                constr.Close();
                constr.Open();
                com = new NpgsqlCommand(@"Insert into t_assets(c_assetname, c_uniqueid, c_aid,c_date,c_isDeleted ) values(@c_assetname ,@c_uniqueid ,@area_id,@c_date,@isdeleted);", constr);  // add data to t_register
                com.Parameters.AddWithValue("@c_assetname", row.c_assetname.Trim());
                com.Parameters.AddWithValue("@c_uniqueid", row.c_uniqueid.Trim());
                com.Parameters.AddWithValue("@area_id", row.c_areaid);
                com.Parameters.AddWithValue("@isdeleted", false);
                DateTime parsedDate = DateTime.Parse(row.c_date);
                com.Parameters.AddWithValue("@c_date", parsedDate);                

                //com.ExecuteNonQuery();
                //DateTime d = Convert.ToDateTime(row.c_date);
                //com.Parameters.AddWithValue("@c_date", d);
                // com.Parameters.AddWithValue("@c_time", row.c_time);
                com.ExecuteNonQuery();
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
        public bool delete(string row)
        {
            try
            {
                constr.Open();
                NpgsqlCommand com = new NpgsqlCommand(@"UPDATE t_assets SET c_isdeleted=true WHERE c_uniqueid=@uniid", constr);
                com.Parameters.AddWithValue("@uniid", row.Trim());                
                com.ExecuteNonQuery();
                constr.Close();
                return true;
            }
            catch(Exception e)
            {                

                return false;
            }
            finally
            {
                constr.Close();
            }
        }
        public bool sdelete(string row)
        {
            try
            {
                constr.Open();
                NpgsqlCommand com = new NpgsqlCommand(@"UPDATE t_assets SET c_isdeleted=true WHERE c_uniqueid=@uniid", constr);
                com.Parameters.AddWithValue("@uniid", row);                
                com.ExecuteNonQuery();
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
    }
}

