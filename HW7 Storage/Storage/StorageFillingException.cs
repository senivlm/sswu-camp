using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Storage_
{
    public class StorageFillingException : Exception
    {
        public StorageFillingException()
        {
        }

        public StorageFillingException(string message) : base(message)
        {
        }

        public StorageFillingException(string message, Exception inner) : base(message, inner)
        { 
        }
    }
}
