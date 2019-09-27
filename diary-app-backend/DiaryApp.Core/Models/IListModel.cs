using System;
using System.Collections.Generic;
using System.Text;

namespace DiaryApp.Core.Models
{
    public interface IListModel
    {
        int ID { get; set; }
        string Title { get; set; }
        List<ListItemBase> Items { get; set; }        
        int PageID { get; set; }
    }
}
