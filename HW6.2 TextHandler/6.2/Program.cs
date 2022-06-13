namespace _6._2
{
    class Program
    {
        public static void Main(string[] args)
        {
            TextHandler th = new TextHandler(@"C:\Users\vital\source\repos\sswu-camp\HW6.2 StringHandler\6.2\Input.txt");
            th.Init();
            th.WriteText(@"C:\Users\vital\source\repos\sswu-camp\HW6.2 StringHandler\6.2\Result.txt");
        }
    }
}