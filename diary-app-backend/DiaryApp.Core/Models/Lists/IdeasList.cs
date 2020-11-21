using DiaryApp.Core.Models.PageAreas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DiaryApp.Core.Models.Lists
{
    public class IdeasList : ICommonListWrapper
    {
        public int ID { get; set; }
        [Required]
        public int ListID { get; set; }
        public virtual CommonList List { get; set; }
        [Required]
        public int IdeasAreaID { get; set; }
        public virtual IdeasArea IdeasArea { get; set; }
        [NotMapped]
        public List<ListItem> Items { get => List.Items; set => List.Items = value; }

        public IdeasList(string title)
        {
            List = new CommonList(title);
        }

        public IdeasList()
        {

        }
    }
}
