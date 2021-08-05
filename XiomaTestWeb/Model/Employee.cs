using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace XiomaTestWeb.Model
{
    public class Employee
    {
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int TZ { get; set; }
        public DateTime BD { get; set; }
        public string Job { get; set; }
        public string Department { get; set; }
        public DateTime StartDate { get; set; }
        public string Comment { get; set; }
        public Address ShipAdd { get; set; }
        public Address HomeAdd { get; set; }


        public Employee()
        {
        }
        public Employee(int tz, string firstName, string lastName, DateTime bd, string job, string department, DateTime startDate, string comment, Address shipAdd, Address homeAdd)
        {
            this.TZ = tz;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.BD = bd;
            this.Job = job;
            this.Department = department;
            this.StartDate = startDate;
            this.Comment = comment;
            this.ShipAdd = shipAdd;
            this.HomeAdd = homeAdd;
        }
        public static bool operator ==(Employee a, Employee b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
            {
                return true;
            }
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
            {
                return false;
            }
            if (a.ID == b.ID)
            {
                return true;
            }
            return false;
        }
        public static bool operator !=(Employee a, Employee b)
        {
            if (ReferenceEquals(a, null) && ReferenceEquals(b, null))
                return false;
            if (ReferenceEquals(a, null) || ReferenceEquals(b, null))
                return true;
            return !(a.ID == b.ID);
        }
        public override bool Equals(object obj)
        {
            if (ReferenceEquals(obj, null))
                return false;
            Employee country = obj as Employee;
            return this.ID == country.ID;
        }
        public override int GetHashCode()
        {
            return (int)this.ID;
        }
    }
}