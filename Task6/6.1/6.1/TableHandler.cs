using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._1
{
    public class TableHandler
    {
        private const char ROW_SEP = '-';
        private const char COLUMN_SEP = '|';
        private const char JOIN_SEP = '+';
        private List<string> _headers;
        private List<List<string>> _rows;

        public TableHandler()
        {
            _headers = new List<string>();
            _rows = new List<List<string>>();
        }

        public List<string> Headers { private get => _headers; set => _headers = value; }

        public void AddRow(List<string> row) => _rows.Add(row);

        public void PrintTable(string path)
        {
            List<int> tableWidths = new List<int>();
            if (Headers.Count != 0)
                foreach(var item in Headers)
                {
                    tableWidths.Add(item.Length);
                }

            foreach (var cells in _rows)
            {
                if (tableWidths == null)
                {
                    tableWidths = new List<int>(cells.Count);
                }
                if (cells.Count != tableWidths.Count)
                {
                    throw new ArgumentException("Number of row-cells should be consistent");
                }
                for (int i = 0; i < cells.Count; i++)
                {
                    tableWidths[i] = Math.Max(tableWidths[i], cells[i].Length);
                }
            }

            using (StreamWriter sw = new StreamWriter(path))
            {
                if (Headers != null)
                {
                    sw.WriteLine(PrintLine(tableWidths));
                    sw.WriteLine(PrintRow(Headers, tableWidths));
                    sw.WriteLine(PrintLine(tableWidths));
                }
                foreach (var cells in _rows)
                {
                    sw.WriteLine(PrintRow(cells, tableWidths));
                }
                if (Headers != null)
                {
                    sw.WriteLine(PrintLine(tableWidths));
                }
            }
        }

        private string PrintLine(List<int> columnWidths)
        {
            string temp = string.Empty;
            for (int i = 0; i < columnWidths.Count; ++i)
            {
                StringBuilder line = new StringBuilder();
                line.Append(ROW_SEP, columnWidths[i] + 2);
                temp += (JOIN_SEP + line.ToString() + (i == columnWidths.Count - 1 ? JOIN_SEP : ""));
            }

            return temp;
        }

        private string PrintRow(List<string> cells, List<int> tableWidths)
        {
            StringBuilder temp = new StringBuilder(COLUMN_SEP.ToString());
            for (int i = 0; i < cells.Count; ++i)
            {
                temp.Append(cells[i]);
                temp.Append(' ', (tableWidths[i] + 2 - cells[i].Length));
                temp.Append(COLUMN_SEP);
            }

            return temp.ToString();
        }
    }
}
