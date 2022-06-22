using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteStats
{
    record class Statistics
    {
        public int quantity = 0;
        public List<DayOfWeek> days;
        public List<TimeSpan> hours;

        public Statistics()
        {
            days = new List<DayOfWeek>();
            hours = new List<TimeSpan>();
        }

        public void Add(TimeSpan hour, DayOfWeek day)
        {
            ++quantity;
            hours.Add(hour);
            days.Add(day);
        }
    }
}
