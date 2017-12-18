using HolidayCalendar.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HolidayCalendar.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetEvents(Events dr)
        {
            //Connect to your database. My database name is DefaultConnection. Please change to reflect your database name. 
            string conn = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection sqlconn = new SqlConnection(conn);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            //Query the Events table to get all of the data. 
            cmd.CommandText = "Select * from Events";
            cmd.Connection = sqlconn;
            sqlconn.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            //This matches the elements in the Events class to the database fields
            List<Events> eventsmodel = new List<Events>();
            if (sdr.HasRows)
            {
                while (sdr.Read())
                {
                    var details = new Events();
                    details.title = sdr["title"].ToString();
                    details.start = Convert.ToDateTime(sdr["start"]);
                    eventsmodel.Add(details);
                }
                dr.eventinfo = eventsmodel;
                sqlconn.Close();
            }

            //This gets the data for the calendar
            return new JsonResult { Data = eventsmodel, JsonRequestBehavior = JsonRequestBehavior.AllowGet };


        }

    }
}