using Newtonsoft.Json;

namespace DiaryApp.Data.DTO
{
    public class BaseDto
    {
        public int Id { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
