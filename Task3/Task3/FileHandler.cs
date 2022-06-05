using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public class FileHandler
    {
        public FileHandler(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} Not Found.");
            this.path = path;
        }

        public IEnumerable<int> GetValuesFromFile()
        {
            IEnumerable<int> values = new List<int>();
            values = File.ReadAllText(path).Split(' ').Select(i => Convert.ToInt32(i)).ToList();      
 
            return values;
        }

        public void WriteFile(ref int[] values)
        {
            using (StreamWriter streamWriter = new StreamWriter(path))
            {
                foreach (int value in values)
                {
                    streamWriter.Write(value + " ");
                } 
            }            
        }

        public string path;
    }
}
