using EmpServiceApp.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace EmpServiceApp.Controllers
{
    [Authorize]
    public class EmpController : ApiController
    {
        //public int Get()
        //{

        //    return 1;
        //}
        [HttpGet]
        //[Route("api/Emp/{EmailID}")]
        public IEnumerable<Employee> GetLoademp() //string EmailID -parameter value
        {
            string connectionstr = "server=XXXXX,1433;database=dias;Trusted_Connection=true;Connect Timeout=1200; User ID = dias_user; Password = XXXXXX";
            SqlDataAdapter adapter;
            DataSet ds = new DataSet();
            var EmpList=new List<Employee>();
            try
            {
                SqlConnection con = new SqlConnection(connectionstr);
                string query = "select * from users";
                   // where email='" + EmailID + "'"*/
                con.Open();
                adapter = new SqlDataAdapter(query, con);
                adapter.Fill(ds);

                EmpList = ds.Tables[0].AsEnumerable().Select(dataRow => new Employee
                { Id = dataRow.Field<Int32>("id"),
                    firstName = dataRow.Field<string>("last_name"),
                    LastName = dataRow.Field<string>("first_name"),
                    emailId = dataRow.Field<string>("email")
                }).ToList();
            }
            catch(Exception ex)
            {

            }
            // return 1;

            return EmpList;
           // return ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        }
    }
}
