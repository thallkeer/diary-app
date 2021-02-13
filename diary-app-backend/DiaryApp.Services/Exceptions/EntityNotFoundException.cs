using DiaryApp.Core.Entities;
using System;

namespace DiaryApp.Services.Exceptions
{
    public class EntityNotFoundException<T> : Exception where T : class
    {        
        public EntityNotFoundException() : base($"Entity of type {typeof(T).Name} with such id is not found!")
        {}
    }

    public class UserNotExistsException : EntityNotFoundException<AppUser>
    {}
}
