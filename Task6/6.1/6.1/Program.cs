namespace _6._1
{
    class Program
    { 
        public static void Main(string[] args)
        {
            /*TableHandler table = new TableHandler();

            table.Headers = new List<string> 
            { "test1", "test2", "_______test3", "__test4" };

            table.AddRow(new List<string> { "0dgsgdsffsd", "", "fsdd", "dsdsaasdsa" });
            table.AddRow(new List<string> { "0dgsgdsffsd", "", "fsdd", "dsdsaasdsa" });

            table.PrintTable(@"C:\Users\vital\source\repos\sswu-camp\Task 6\6.1\6.1\output.txt");*/

            Accounting accounting = new Accounting(@"C:\Users\vital\source\repos\sswu-camp\Task 6\6.1\6.1\Input.txt");
        }
    }
}