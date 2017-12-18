using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolidayCalendar.Models
{
    public class Events
    {
        public int Id { get; set; }
        public String title { get; set; }
        public DateTime start { get; set; }

        public List<Events> eventinfo { get; set; }
    }
}