using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiaryApp.Data.Exceptions
{
    public class UserNotExistsException : Exception
    {
        public UserNotExistsException(string message) : base(message)
        {
        }
    }
}
