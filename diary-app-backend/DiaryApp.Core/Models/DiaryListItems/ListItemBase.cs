using System;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models
{
    /// <summary>
    /// Represents base class for diary list item.
    /// </summary>
    public abstract class ListItemBase : BaseEntity
    {   
        /// <summary>
        /// Outer link for list item
        /// </summary>
        [DataType(DataType.Url)]
        public string Url { get; set; }

        /// <summary>
        /// Text content of item
        /// </summary>
        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Id of list which contains item
        /// </summary>
        [Required]
        public int OwnerID { get; set; }

        public ListItemBase()
        {

        }

        protected ListItemBase(ListItemBase original)
        {
            Subject = original.Subject;
            Url = original.Url;
        }

        public abstract ListItemBase GetCopy();

        public override string ToString()
        {
            return Subject;
        }

        public override bool Equals(object obj)
        {
            return obj is ListItemBase @base &&
                   Id == @base.Id &&
                   Url == @base.Url &&
                   Subject == @base.Subject &&
                   OwnerID == @base.OwnerID;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Url, Subject, OwnerID);
        }
    }    
}
