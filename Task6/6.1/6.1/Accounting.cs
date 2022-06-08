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
        private DateTime _date = DateTime.MinValue;
        public List<string> quartalMonths;
        public List<FlatAccount> data;

        public struct FlatAccount
        {
            public int number;
            public string ownerSurname;
            public int prevKilowatt;
            public int nextKilowatt;
            public List<DateTime> captureDates;
            public double debt;
        }

        public Accounting(string path)
        {
            int flatCount = 0;
            int quartalNumber = 0;

            data = new List<FlatAccount>();
            using (StreamReader sr = new StreamReader(path))
            {
                string? line = sr.ReadLine();
                if (line != null)
                {
                    string count = line.Substring(0, line.IndexOf(' '));
                    string quartal = line.Substring(line.IndexOf(' ') + 1);

                    int.TryParse(count, out flatCount);
                    int.TryParse(quartal, out quartalNumber);
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

                quartalMonths = new List<string>();
                switch (quartalNumber)
                {
                    case 1:
                        foreach (Quartal.First month in (Quartal.First[])Enum.GetValues(typeof(Quartal.First)))
                        {
                            quartalMonths.Add(month.ToString());
                        }
                        break;
                    case 2:
                        foreach (Quartal.Second month in (Quartal.First[])Enum.GetValues(typeof(Quartal.Second)))
                        {
                            quartalMonths.Add(month.ToString());
                        }
                        break;
                    case 3:
                        foreach (Quartal.Third month in (Quartal.First[])Enum.GetValues(typeof(Quartal.Third)))
                        {
                            quartalMonths.Add(month.ToString());
                        }
                        break;
                    case 4:
                        foreach (Quartal.Fourth month in (Quartal.First[])Enum.GetValues(typeof(Quartal.Fourth)))
                        {
                            quartalMonths.Add(month.ToString());
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(quartalNumber) + " can't be bigger than 4");
                }
            }
        }
    }
}
