using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DiaryApp.Core.Models
{
    public abstract class ListItemBase
    {
        public int ID { get; set; }
        public string Subject { get; set; }
        [Required]
        public int OwnerListID { get; set; }
        public virtual IListModel Owner { get; set; }
    }
}
