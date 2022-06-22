namespace SiteStats
{
    class Program
    {
        public static void Main(string[] args)
        {
            IpStats stats = new IpStats(@"C:\Users\vital\source\repos\sswu-camp\HW8 SiteStats\SiteStats\SiteStats\Ip_List.txt");
            //Console.WriteLine(stats);
            using (StreamWriter sw = new StreamWriter(@"C:\Users\vital\source\repos\sswu-camp\HW8 SiteStats\SiteStats\SiteStats\Result.txt"))
                sw.Write(stats);
        }
    }
}