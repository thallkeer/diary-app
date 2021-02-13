using System;

namespace DiaryApp.Data.Exceptions
{
    public class EntityNotFoundException<T> : Exception where T : class
    {        
        public EntityNotFoundException() : base($"Entity of type {typeof(T).Name} with such id is not found!")
        {
        }
    }
}
