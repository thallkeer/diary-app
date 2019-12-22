using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiaryApp.Core.Models
{
    public interface IListModel<T>
    {
        [ScaffoldColumn(false)]
        int ID { get; set; }
        [Required]
        int PageID { get; set; }
        [Required]
        ///TODO: add unique constaint
        string Title { get; set; }
        [Required]
        ///TODO: add unique constaint
        int Month { get; set; }
        List<T> Items { get; set; }
    }
}
