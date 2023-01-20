using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackerFlow.BAL.Models;

namespace TrackerFlow.BAL
{
    public class SettingsHelper : Helper
    {
        public bool InsOffice(Office data)
        {
            NpgsqlCommand cmd1 = new NpgsqlCommand("select * from t_office where c_ofcname=@c_ofcname", constr);
            cmd1.Parameters.AddWithValue("@c_ofcname", data.c_ofcname);
            constr.Open();
            NpgsqlDataReader dr = cmd1.ExecuteReader();
            if (dr.HasRows)
            {
                constr.Close();
                return false;
            }
            constr.Close();
            NpgsqlCommand cm = new NpgsqlCommand("INSERT INTO t_office (c_ofcname) VALUES (@c_ofcname)", constr);
            cm.Parameters.AddWithValue("@c_ofcname", data.c_ofcname);
            constr.Open();
            bool result = cm.ExecuteNonQuery() > 0;
            constr.Close();
            return result;
        }

        public bool InsFloor(Floor data, int c_ofcid)
        {
            NpgsqlCommand cmd1 = new NpgsqlCommand("select * from t_floor where c_floornum=@c_floornum and c_ofcid=@c_ofcid", constr);
            cmd1.Parameters.AddWithValue("@c_floornum", data.c_floornum);
            cmd1.Parameters.AddWithValue("@c_ofcid", c_ofcid);
            constr.Open();
            NpgsqlDataReader dr = cmd1.ExecuteReader();
            if (dr.HasRows)
            {
                constr.Close();
                return false;
            }
            constr.Close();
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO t_floor (c_floornum,c_ofcid) VALUES (@c_floornum,@c_ofcid) ", constr);
            cmd.Parameters.AddWithValue("@c_floornum", data.c_floornum);
            cmd.Parameters.AddWithValue("@c_ofcid",c_ofcid);
            constr.Open();
            bool result = cmd.ExecuteNonQuery()>0;
            constr.Close();
            return result;
        }

        public bool InsArea(Area data, int c_floorid)
        {
            NpgsqlCommand cmd1 = new NpgsqlCommand("select * from t_area where c_areaname=@c_areaname and c_floorid=@c_floorid", constr);
            cmd1.Parameters.AddWithValue("@c_areaname", data.c_areaname);
            cmd1.Parameters.AddWithValue("@c_floorid", c_floorid);
            constr.Open();
            NpgsqlDataReader dr = cmd1.ExecuteReader();
            if (dr.HasRows)
            {
                constr.Close();
                return false;
            }
            constr.Close();
            NpgsqlCommand cmd = new NpgsqlCommand("INSERT INTO t_area (c_areaname,c_floorid) VALUES (@c_areaname,@c_floorid) ", constr);
            cmd.Parameters.AddWithValue("@c_areaname", data.c_areaname);
            cmd.Parameters.AddWithValue("@c_floorid", c_floorid);
            constr.Open();
            bool result = cmd.ExecuteNonQuery() > 0;
            constr.Close();
            return result;
        }

        public bool InsEmailId(EmailSetting data)
        {
            NpgsqlCommand cmd1 = new NpgsqlCommand("select * from t_email where c_email=@c_email", constr);
            cmd1.Parameters.AddWithValue("@c_email", data.c_email);
            constr.Open();
            NpgsqlDataReader dr = cmd1.ExecuteReader();
            if (dr.HasRows)
            {
                constr.Close();
                return false;
            }
            constr.Close();
            NpgsqlCommand cm = new NpgsqlCommand("INSERT INTO t_email (c_email) VALUES (@c_email)", constr);
            cm.Parameters.AddWithValue("@c_email", data.c_email);
            constr.Open();
            bool result = cm.ExecuteNonQuery() > 0;
            constr.Close();
            return result;
        }
    }
}

