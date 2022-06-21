using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _6._1
{
    public class Accounting
    {
        private const double PRICE = 0.06;
        private int quarterNumber = 0;
        public List<string> quarterMonths;
        public List<FlatAccount> data;

        public struct FlatAccount
        {
            public int number;
            public string ownerSurname;
            public int prevKilowatt;
            public int nextKilowatt;
            public List<DateTime> captureDates;
            public double debt;
            
            public static bool operator==(FlatAccount lhs, FlatAccount rhs)
            {
                return lhs.ownerSurname == rhs.ownerSurname && lhs.number == rhs.number;
            }
        
            public static bool operator!=(FlatAccount lhs, FlatAccount rhs)
            {
                return !(lhs == rhs);
            }

        }

        public Accounting(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException();
            }

            int flatCount = 0;

            data = new List<FlatAccount>();
            using (StreamReader sr = new StreamReader(path))
            {
                string? line = sr.ReadLine();
                if (line != null)
                {
                    string count = line.Substring(0, line.IndexOf(' '));
                    string quarter = line.Substring(line.IndexOf(' ') + 1);

                    int.TryParse(count, out flatCount);
                    int.TryParse(quarter, out quarterNumber);
                }
                else
                    throw new ArgumentNullException("Exception: File is empty");
                
                for (int i = 0; i < flatCount; i++)
                {
                    string[] dataLine = sr.ReadLine().Split(' ');
                    FlatAccount account = new FlatAccount();
                    int.TryParse(dataLine[0], out account.number);
                    account.ownerSurname = dataLine[1];
                    int.TryParse(dataLine[2], out account.prevKilowatt);
                    int.TryParse(dataLine[3], out account.nextKilowatt);
                    account.captureDates = new List<DateTime>();
                    for (int j = 4; j < 7; ++j)
                    {
                        account.captureDates.Add(DateTime.ParseExact(dataLine[j], "dd/MM/yyyy", System.Globalization.CultureInfo.CreateSpecificCulture("uk-UA")));
                    }

                    if (account.nextKilowatt > account.prevKilowatt)
                    {
                        account.debt = (account.nextKilowatt - account.prevKilowatt) * PRICE; 
                    }
                    else
                    {
                        account.debt = 0;
                    }

                    data.Add(account);
                }

                quarterMonths = new List<string>();
                switch (quarterNumber)
                {
                    case 1:
                        foreach (Quarter.First month in (Quarter.First[])Enum.GetValues(typeof(Quarter.First)))
                        {
                            quarterMonths.Add(month.ToString());
                        }
                        break;
                    case 2:
                        foreach (Quarter.Second month in (Quarter.First[])Enum.GetValues(typeof(Quarter.Second)))
                        {
                            quarterMonths.Add(month.ToString());
                        }
                        break;
                    case 3:
                        foreach (Quarter.Third month in (Quarter.First[])Enum.GetValues(typeof(Quarter.Third)))
                        {
                            quarterMonths.Add(month.ToString());
                        }
                        break;
                    case 4:
                        foreach (Quarter.Fourth month in (Quarter.First[])Enum.GetValues(typeof(Quarter.Fourth)))
                        {
                            quarterMonths.Add(month.ToString());
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(quarterNumber) + " can't be bigger than 4");
                }
            }
        }

        private Accounting(List<FlatAccount> data, int quarterNumber)
        {
            this.quarterNumber = quarterNumber;
            this.data = data;
        }   

        public FlatAccount GetMaxDebtAccount()
        {
            int maxDebtIndex = 0;
            for (int i = 0; i < data.Count; ++i)
            {
                maxDebtIndex = data[i].debt > data[maxDebtIndex].debt ? i : maxDebtIndex;
            }

            return data[maxDebtIndex];
        }
        
        public bool Contains(FlatAccount another)
        {
            foreach (var flat in data)
            {
                if (flat == another)
                {
                    return true; 
                }
            }

            return false;
        }

        public static Accounting operator+(Accounting lhs, Accounting rhs)
        {//Є проблеми у перевантаженні. Поясню словесно.
            
            if (lhs.quarterNumber != rhs.quarterNumber)
                throw new InvalidCastException();

            List<FlatAccount> comparedAccounts = lhs.data;
            foreach (var flat in rhs.data)
            {
                if (!lhs.Contains(flat))
                    comparedAccounts.Add(flat);
                    
            }

            return new Accounting(comparedAccounts, lhs.quarterNumber);
        }
        
        public static Accounting operator-(Accounting lhs, Accounting rhs)
        {
            if (lhs.quarterNumber != rhs.quarterNumber)
                throw new InvalidCastException();

            List<FlatAccount> comparedAccounts = new List<FlatAccount>();
            foreach (var flat in lhs.data)
            {
                if (!rhs.Contains(flat))
                    comparedAccounts.Add(flat);
            }

            return new Accounting(comparedAccounts, lhs.quarterNumber);
        }

    }
}
