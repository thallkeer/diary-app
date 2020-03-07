using DiaryApp.Core.Models;

namespace DiaryApp.API.Models
{
    public class TransferDataRequestParams
    {
        public PageParams PageParams { get; set; }
        public TransferDataModel TransferDataModel { get; set; }
    }
}
