using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using XiosTestWebService.Model;

namespace XiosTestWebService.DAO
{
    public class EmployeeDAO
    {
        string conn = ConfigurationManager.ConnectionStrings["DAO_CON"].ConnectionString;
        private static object key = new object();
        public static SqlCommand cmd = new SqlCommand();
        //public int AddHomeAddress(Address home)
        //{
        //    int addressID = 0;

        //    return addressID;
        //}
        //public int AddShippingAddress(Address home)
        //{
        //    int addressID = 0;

        //    return addressID;
        //}
        public bool AddEmployee(Employee emp)
        {
            int hasShipping = emp.ShipAdd == null ? 0 : 1;
            bool wasAdded = false;
            lock (key)
            {
                using (cmd.Connection = new SqlConnection(conn))
                {
                    cmd.Connection.Open();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"insert into Employees(ID, FIRST_NAME, LAST_NAME, B_DATE, JOB, DEPARTMENT, START_DATE, HOME_ADDRESS, SHIPPING_ADDRESS)" +
                    $"values('{ emp.ID}', '{emp.FirstName}', '{ emp.LastName}', { emp.BD}, '{emp.Job}', '{emp.Department}', '{emp.StartDate}', '{1}', '{hasShipping}');";


                    string res = cmd.ExecuteScalar().ToString();
                    cmd.ExecuteNonQuery();
                    wasAdded = true;
                }
            }
            return wasAdded;
        }
        
    }
}
//$"insert into Addresses (ADDRESS_ID, ADDRESS_TYPE,CITY,STREET,NUMBER, POSTAL_CODE)" +
//                        $"values('{ emp.HomeAdd.AddressID}', '{emp.HomeAdd.AddressType}', '{ emp.HomeAdd.City}', { emp.HomeAdd.St}, '{emp.HomeAdd.Number}', '{emp.HomeAdd.PostalCode}');"+
//                        $"insert into Addresses (ADDRESS_ID, ADDRESS_TYPE,CITY,STREET,NUMBER, POSTAL_CODE)" +
//                        $"values('{ emp.ShipAdd.AddressID}', '{emp.ShipAdd.AddressType}', '{ emp.ShipAdd.City}', { emp.ShipAdd.St}, '{emp.ShipAdd.Number}', '{emp.ShipAdd.PostalCode}');";
//throw new AlreadyExistEmployee("Employee ID: " + emp.ID + " ALREADY EXIST");