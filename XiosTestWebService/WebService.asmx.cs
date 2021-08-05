using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using XiosTestWebService.DAO;
using XiosTestWebService.Model;

namespace XiosTestWebService
{
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService
    {
        

        [WebMethod]
        public string HelloWorld()
        {
            return "Hello World";
        }
        
        [WebMethod]
        public string AddEmployee(Employee emp, Address ad)
        {
            //Employee emp = new Employee(tz, firstName, lastName, bd, job, department, startDate, comment, shipAdd, homeAdd);
            EmployeeDAO dao = new EmployeeDAO();
            try
            {
                dao.AddEmployee(emp);
                return "employee: " + emp.FirstName + " " + emp.LastName + " was added ";
            }
            catch (Exception e)
            {
                return e.ToString();
            }

        }
    }
}
