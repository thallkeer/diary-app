using DiaryApp.Core.Entities.Users;

namespace DiaryApp.Services.Exceptions
{
    public class EntityNotFoundException<T> : ApiException where T : class
    {        
        public EntityNotFoundException() : base(System.Net.HttpStatusCode.NotFound, new { Entity = $"Entity of type {typeof(T).Name} with such id is not found!" })
        {}
    }

    public class UserNotExistsException : EntityNotFoundException<AppUser>
    {}
}
