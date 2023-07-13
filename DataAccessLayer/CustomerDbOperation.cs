using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using CommonLayer.Models;
using Microsoft.Data.SqlClient;

namespace DataAccessLayer
{
    public class CustomerDbOperation
    {
        public DbConnect dbconnect;
        public CustomerDbOperation()
        {
            dbconnect = new DbConnect();
        }
        public List<Customer> GetCustomers()
        {
            List<Customer> cusList = new List<Customer>();
            //string sql = "Select * from tbl_Customer";
            SqlCommand cmd = new SqlCommand("sp_Customer", dbconnect.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "Show");
            dbconnect.connection.Open();
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Customer cus = new Customer();
                cus.Cid = (int)dr["cid"];
                cus.CName = dr["cname"].ToString();
                cus.CAge = (int)dr["cage"];
                cus.City = dr["city_name"].ToString();
                cus.CSalary = (decimal)dr["csalary"];
                cus.custImg = dr["custimg"].ToString();
                cusList.Add(cus);
            }
            dbconnect.connection.Close();
            return cusList;
        }
        public List<SelectListItem> bindCity()
        {
            //string sql = "select * from tbl_city";
            SqlCommand cmd = new SqlCommand("sp_getcity", dbconnect.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            if(dbconnect.connection.State==ConnectionState.Closed)
            {
                dbconnect.connection.Open();
            }
            SqlDataReader dr = cmd.ExecuteReader();
            List<SelectListItem> cityList = new List<SelectListItem>();
            while(dr.Read())
            {
                var ctylist = new SelectListItem
                {
                    Value = dr.GetValue(0).ToString(),
                    Text = dr.GetString(1)
                };
                cityList.Add(ctylist);
            }
            dbconnect.connection.Close();
            return cityList;
        }
        public bool addCustomer(Customer cusObj,string filename)
        {
            //string sql = "Insert into tbl_Customer(cname,cage,city,csalary)values(@name,@age,@city,@salary)";
            SqlCommand cmd = new SqlCommand("sp_Customer", dbconnect.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", cusObj.CName);
            cmd.Parameters.AddWithValue("@age", cusObj.CAge);
            cmd.Parameters.AddWithValue("@city", cusObj.City);
            cmd.Parameters.AddWithValue("@salary", cusObj.CSalary);
            cmd.Parameters.AddWithValue("@custImg", filename);
            cmd.Parameters.AddWithValue("@action", "Create");
            dbconnect.connection.Open();
            int n = cmd.ExecuteNonQuery(); // No. of Rows affected. if success then return 1 else 0.
            dbconnect.connection.Close();
            if (n != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public Customer getCustomer(int id)
        {
            dbconnect.connection.Open();
            //string str = "select * from tbl_Customer where cid=" + id;
            SqlCommand cmd = new SqlCommand("sp_Customer", dbconnect.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action", "Show_Single");
            cmd.Parameters.AddWithValue("@cid",id);
            SqlDataReader dr = cmd.ExecuteReader();
            Customer cus = new Customer();
            if (dr.Read())
            {
                cus.Cid = (int)dr["cid"];
                cus.CName = dr["cname"].ToString();
                cus.CAge = (int)dr["cage"];
                cus.City = dr["city"].ToString();
                cus.CSalary = (decimal)dr["csalary"];
            }
            dbconnect.connection.Close();
            return cus;
        }
        public bool updateCustomer(Customer cusObj,string filename)
        {
            //string sql = "update tbl_Customer set cname=@name,cage=@age,city=@city,csalary=@salary where cid=@cid";
            SqlCommand cmd = new SqlCommand("sp_Customer", dbconnect.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@name", cusObj.CName);
            cmd.Parameters.AddWithValue("@age", cusObj.CAge);
            cmd.Parameters.AddWithValue("@city", cusObj.City);
            cmd.Parameters.AddWithValue("@salary", cusObj.CSalary);
            cmd.Parameters.AddWithValue("@custImg",filename);
            cmd.Parameters.AddWithValue("@cid", cusObj.Cid);
            cmd.Parameters.AddWithValue("@action", "Update");
            dbconnect.connection.Open();
            int n = cmd.ExecuteNonQuery(); // No. of Rows affected. if success then return 1 else 0.
            dbconnect.connection.Close();
            if (n != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public bool deleteCustomer(int id)
        {
            //string sql = "delete from tbl_Customer where cid=@cid";
            SqlCommand cmd = new SqlCommand("sp_Customer", dbconnect.connection);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@action","Delete");
            cmd.Parameters.AddWithValue("@cid", id);
            dbconnect.connection.Open();
            int n = cmd.ExecuteNonQuery(); // No. of Rows affected. if success then return 1 else 0.
            dbconnect.connection.Close();
            if (n != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}

