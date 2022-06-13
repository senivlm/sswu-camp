using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._2
{
    class TextHandler
    {
        private static List<char> separators = new List<char>() { ' ', '\t', '\n', '\r', '\uffff' }; 
        private static List<char> punctuation = new List<char>() { ',', '.', ' ' }; 
        private string _path;
        private List<string> text;
        
        public TextHandler(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} file not found.");
            _path = path;
            text = new List<string>();
        }

        public void Init()
        {
            using (StreamReader sr = new StreamReader(_path))
            {
                const char dot = '.';
                string sentence = string.Empty;

                do
                {
                    string word = string.Empty;

                    char character = char.MinValue;
                    while (!separators.Contains(character = (char)sr.Read()))
                    {
                        word += character;
                    }

                    if (word != string.Empty)
                        sentence += word + " ";
                    
                    if (word.EndsWith(dot) && word.Length > 1)
                    {
                        text.Add(sentence);
                        sentence = string.Empty;
                    }
                }
                while (!sr.EndOfStream);
            }
        }

        public void WriteText(string ouputhPath)
        {
            using (StreamWriter sw = new StreamWriter(ouputhPath))
            {
                foreach(var sentence in text)
                {
                    sw.WriteLine(sentence);
                }
                
                sw.WriteLine("The longest words:");
                foreach (var word in FindLongestWords())
                {
                    sw.Write(word + " ");
                }
                sw.WriteLine();
                
                sw.WriteLine("The smallest words:");
                foreach (var word in FindSmallestWords())
                {
                    sw.Write(word + " ");
                }
            }
        }

        public List<string> FindLongestWords()
        {
            List<string> longestWords = new List<string>();
            foreach (var sentence in text)
            {
                string max = string.Empty;
                foreach (var word in sentence.Split(" "))
                {
                    string temp = word;
                    if (punctuation.Any(word.Contains))
                        punctuation.ForEach(sign => temp = temp.Replace(sign.ToString(), string.Empty));
                    max = max.Length < temp.Length ? temp : max;      
                }
                longestWords.Add(max);
            }
            
            return longestWords;
        }

        public List<string> FindSmallestWords()
        {
            List<string> smallestWords = new List<string>();
            foreach (var sentence in text)
            {
                string min = string.Empty;
                foreach (var word in sentence.Split(" "))
                {
                    if (word == "") break;

                    string temp = word;
                    if (punctuation.Any(word.Contains))
                        punctuation.ForEach(sign => temp = temp.Replace(sign.ToString(), string.Empty));
                    if (min == "")
                        min = word;
                    min = min.Length > word.Length ? word : min;
                }
                smallestWords.Add(min);
            }

            return smallestWords;
        }
    }
}
