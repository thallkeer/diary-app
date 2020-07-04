using DiaryApp.Core;

namespace DiaryApp.API.Models
{
    public class CommonListModel : ListModel<ListItemModel>
    {
        public int? IdeasAreaID { get; set; }
        public int? DesiresAreaID { get; set; }
    }
}
