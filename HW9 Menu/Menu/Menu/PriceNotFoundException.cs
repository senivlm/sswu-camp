using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Menu
{
    public class PriceNotFoundException : Exception
    {
        public PriceNotFoundException()
        {
        }

        public PriceNotFoundException(string message)
            : base(message)
        {
        }

        public PriceNotFoundException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
