using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryApp.Data.Exceptions
{
    public class PageDataTransferException : Exception
    {
        public PageDataTransferException()
        {

        }
        public PageDataTransferException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
