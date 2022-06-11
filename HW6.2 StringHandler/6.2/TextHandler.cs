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
        private string _path;
        private List<string> text;
        
        public TextHandler(string path)
        {
            if (!File.Exists(path))
                throw new FileNotFoundException($"{path} file not found.");
            _path = path;
            text = new List<string>();
        }

        public void MakeSentences()
        {
            using (StreamReader sr = new StreamReader(_path))
            {
                const char dot = '.';
                string sentence = string.Empty;

                do
                {
                    string word = string.Empty;

                    //while (!sr.Peek().Equals(separators.ToArray()))
                    char character = char.MinValue;
                    while (!separators.Contains(character = (char)sr.Read()))
                    {    
                        word += character;
                    }
                    sentence += word + " ";
                  /*if (word[word.Length - 1] == dot)
                    {
                        text.Add(sentence);
                    }*/
                }
                while (!sr.EndOfStream);
            }
        }
    }
}
