using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteStats
{
    public class IpStats
    {
        private Dictionary<string, Statistics> _ipStats;

        public IpStats(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException();

            _ipStats = new Dictionary<string, Statistics>();
            
            using (StreamReader sr = new StreamReader(path))
            {   
                while (!sr.EndOfStream)
                {
                    string[] line = sr.ReadLine().Split(" ");

                    string ipAdress = line[0];

                    TimeSpan hour = TimeSpan.Parse(line[1]);
                    DayOfWeek day = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), line[2].Replace(line[2][0], char.ToUpper(line[2][0])));

                    if (_ipStats.ContainsKey(ipAdress))
                    {
                        _ipStats[ipAdress].Add(hour, day);
                    }
                    else
                    {
                        _ipStats[ipAdress] = new Statistics();
                        _ipStats[ipAdress].Add(hour, day);
                    }
                }
            }
        }

        public override string ToString()
        {
            string temp = string.Empty;
            foreach (var ip in _ipStats)
            {
                string line = $"{ip.Key} \nVisit count: {ip.Value.quantity}" +
                    $"\nThe most popular day: {GetPopularDay(ip.Value)}" +
                    $"\nThe most popular hour: {GetPopularHourOfDay(ip.Value)}";

                temp += line + "\n";
            }

            return temp;
        }

        private DayOfWeek GetPopularDay(Statistics stats)
        {
            return (from day in stats.days group day by day into grp orderby grp.Count() descending select grp.Key).First();
        }

        private int GetPopularHourOfDay(Statistics stats)
        {
            return (from hour in stats.hours
                    group hour.Hours by hour.Hours into grp
                    orderby grp.Count() descending
                    select grp.Key).First();
                    
        }
    }
}
