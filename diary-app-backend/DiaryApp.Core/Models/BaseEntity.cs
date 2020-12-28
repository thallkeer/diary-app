using Newtonsoft.Json;

namespace DiaryApp.Core.Models
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
