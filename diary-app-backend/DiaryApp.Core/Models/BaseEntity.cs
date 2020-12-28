using Newtonsoft.Json;

namespace DiaryApp.Core.Models
{
    /// <summary>
    /// Base class for all application entities
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
