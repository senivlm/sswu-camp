namespace Product
{
    public class Program
    {
        public static void Main(string[] args)
        {

            Check check = new Check(); 

            List<Buy> shopping_list = new List<Buy>();

            shopping_list.Add(new Buy("Tomato", 10, 0.05, 6));
            shopping_list.Add(new Buy("Bread", 40, 0.25, 1));
            shopping_list.Add(new Buy("Cucumber", 15, 0.07, 10));
            shopping_list.Add(new Buy("Cheese", 10, 0.01, 20));
                                
           foreach (Buy item in shopping_list)
           {
                check.CheckPurchases(item);
           }
        }
    }
}
