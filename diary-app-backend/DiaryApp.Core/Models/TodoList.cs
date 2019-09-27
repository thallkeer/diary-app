using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiaryApp.Core.Models
{
    public class TodoList : IListModel
    {
        public int ID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Title { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<ListItemBase> Items { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int PageID { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
