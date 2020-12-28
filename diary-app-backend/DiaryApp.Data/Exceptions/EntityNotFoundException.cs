using System;

namespace DiaryApp.Data.Exceptions
{
    public class EntityNotFoundException : Exception
    {        
        public EntityNotFoundException(string message) : base(message)
        {
        }
    }
}
