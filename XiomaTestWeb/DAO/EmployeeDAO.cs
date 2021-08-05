using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using XiomaTestWeb.Model;

namespace XiomaTestWeb.DAO
{
    public class EmployeeDAO
    {
        string conn = ConfigurationManager.ConnectionStrings["DAO_CON"].ConnectionString;
        private static object key = new object();
        public static SqlCommand cmd = new SqlCommand();

       
            public bool UpdateEmployeeByTZ(Employee emp)
            {
                int hasShipping = emp.ShipAdd.City == null ? 0 : 1;
                if (emp.FirstName == "" || emp.LastName == "" || emp.Job == "" || emp.StartDate == null || emp.HomeAdd.Number == 0 || emp.HomeAdd.City == "")
                    throw new EmpthyBoxesException($"עלייך למלא את כל השדות המסומנות!! ");
                if (int.TryParse(emp.FirstName, out int x) || int.TryParse(emp.LastName, out x) || int.TryParse(emp.Job, out x)
                    || int.TryParse(emp.Department, out x) || int.TryParse(emp.HomeAdd.City, out x) || int.TryParse(emp.HomeAdd.St, out x))
                    throw new NumberNotAllowdException(" שם פרטי, משפחה, תפקיד,מחלקה, עיר ורחוב לא יכולים להיות מספר בלבד!");
                bool wasUpdated = false;
                lock (key)
                {
                    using (SqlConnection con = new SqlConnection(conn))
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdateEmployeeByTZ", con))
                        {
                            cmd.Parameters.Add(new SqlParameter("@ADDRESS_TYPE", emp.HomeAdd.AddressType));
                            cmd.Parameters.Add(new SqlParameter("@ADDRESS_TYPE_SHIP", emp.ShipAdd.AddressType));
                            cmd.Parameters.Add(new SqlParameter("@CITY", emp.HomeAdd.City));
                            cmd.Parameters.Add(new SqlParameter("@CITY_SHIP", emp.ShipAdd.City));
                            cmd.Parameters.Add(new SqlParameter("@STREET", emp.HomeAdd.St));
                            cmd.Parameters.Add(new SqlParameter("@STREET_SHIP", emp.ShipAdd.St));
                            cmd.Parameters.Add(new SqlParameter("@NUMBER", emp.HomeAdd.Number));
                        cmd.Parameters.Add(new SqlParameter("@NUMBERSHIP", emp.ShipAdd.Number));
                        cmd.Parameters.Add(new SqlParameter("@POSTAL_CODE", emp.HomeAdd.PostalCode));
                        cmd.Parameters.Add(new SqlParameter("@POSTAL_CODE_SHIP", emp.ShipAdd.PostalCode));

                        cmd.Parameters.Add(new SqlParameter("@TZ", emp.TZ));
                            cmd.Parameters.Add(new SqlParameter("@FIRST_NAME", emp.FirstName));
                            cmd.Parameters.Add(new SqlParameter("@LAST_NAME", emp.LastName));
                            cmd.Parameters.Add(new SqlParameter("@B_DATE", emp.BD));
                            cmd.Parameters.Add(new SqlParameter("@JOB", emp.Job));
                            cmd.Parameters.Add(new SqlParameter("@DEPARTMENT", emp.Department));
                            cmd.Parameters.Add(new SqlParameter("@START_DATE", emp.StartDate));
                            cmd.Parameters.Add(new SqlParameter("@FREE_TEXT", emp.Comment));
                            cmd.Parameters.Add(new SqlParameter("@HOME_ADDRESS", 1));
                            cmd.Parameters.Add(new SqlParameter("@SHIPPING_ADDRESS", hasShipping));
                            cmd.Connection.Open();
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.ExecuteNonQuery();

                            wasUpdated = true;

                        }
                    }
            
                }
                return wasUpdated;
            }

        public DataTable GetTableByName(string name)
        {
            DataTable dt = new DataTable();
            lock (key)
            {
                using (cmd.Connection = new SqlConnection(conn))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"select ID, FIRST_NAME,LAST_NAME,JOB,DEPARTMENT,START_DATE,HOME_ADDRESS,SHIPPING_ADDRESS from Employees where First_NAME like N'%{name}%'";

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                }
            }
            return dt;
        }
        public DataTable GetEmployeeTable()
        {
            DataTable dt = new DataTable();
            lock (key)
            {

                using (cmd.Connection = new SqlConnection(conn))
                {
                    
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "select ID, FIRST_NAME,LAST_NAME,JOB,DEPARTMENT,START_DATE,HOME_ADDRESS,SHIPPING_ADDRESS from Employees";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    da.Dispose();
                }
            }
                return dt;
        }
        public bool AddEmployee(Employee emp)
        {
            int hasShipping = emp.ShipAdd.City == "" ? 0 : 1;
            if (hasShipping == 0) emp.ShipAdd.AddressType = 0;
            bool wasAdded = false;
            
            if (emp.FirstName == "" || emp.LastName == "" || emp.Job == "" || emp.StartDate == null || emp.HomeAdd.Number == 0 || emp.HomeAdd.City == "")
                throw new EmpthyBoxesException($"עלייך למלא את כל השדות המסומנות!! ");
            if (int.TryParse(emp.FirstName, out int x) || int.TryParse(emp.LastName, out x) || int.TryParse(emp.Job, out x)
                || int.TryParse(emp.Department, out x) || int.TryParse(emp.HomeAdd.City, out x) || int.TryParse(emp.HomeAdd.St, out x))
                throw new NumberNotAllowdException(" שם פרטי, משפחה, תפקיד,מחלקה, עיר ורחוב לא יכולים להיות מספר בלבד!");
            if(hasShipping == 1)
            {
                if(int.TryParse(emp.ShipAdd.City, out x) || int.TryParse(emp.ShipAdd.St, out x))
                    throw new NumberNotAllowdException(" עיר ורחוב לכתובת משלוח לא יכולים להיות מספר בלבד!");
            }
            lock (key)
            {
                using (cmd.Connection = new SqlConnection(conn))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"INSERT INTO Employees(ID, FIRST_NAME, LAST_NAME, B_DATE, JOB, DEPARTMENT, START_DATE, HOME_ADDRESS, SHIPPING_ADDRESS)" +
                    $"values('{ emp.TZ}', '{emp.FirstName}', '{ emp.LastName}', '{ emp.BD.ToString("yyyy - MM - dd HH: mm:ss.fff")}', '{emp.Job}', '{emp.Department}', '{emp.StartDate.ToString("yyyy - MM - dd HH: mm:ss.fff")}', '{1}', '{hasShipping}');" +
                    $"INSERT INTO Addresses(ADDRESS_ID, ADDRESS_TYPE, CITY, STREET, NUMBER, POSTAL_CODE)" +
                    $"values('{ emp.TZ}', {emp.HomeAdd.AddressType}, '{ emp.HomeAdd.City}', '{ emp.HomeAdd.St}', {emp.HomeAdd.Number}  , {emp.HomeAdd.PostalCode}) ;" +
                    $"INSERT INTO Addresses(ADDRESS_ID, ADDRESS_TYPE, CITY, STREET, NUMBER, POSTAL_CODE)" +
                    $"values('{ emp.TZ}', {emp.ShipAdd.AddressType}, '{ emp.ShipAdd.City}', '{ emp.ShipAdd.St}', {emp.ShipAdd.Number}  , {emp.ShipAdd.PostalCode})";
                    // string res = cmd.ExecuteScalar().ToString();
                    cmd.ExecuteNonQuery();
                    wasAdded = true;
                }
            }
            return wasAdded;
        }

    }
}