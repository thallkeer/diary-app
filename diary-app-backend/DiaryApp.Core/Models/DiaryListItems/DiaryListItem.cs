using System;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Models
{
    /// <summary>
    /// Represents base class for diary list item.
    /// </summary>
    public abstract class DiaryListItem : BaseEntity
    {
        public DiaryListItem()
        { }

        protected DiaryListItem(DiaryListItem original)
        {
            if (original is null) throw new ArgumentNullException(nameof(original));
            Subject = original.Subject;
            Url = original.Url;
        }

        /// <summary>
        /// Outer link for list item
        /// </summary>
        [DataType(DataType.Url)]
        public string Url { get; set; } = string.Empty;

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

        /// <summary>
        /// List which contains item
        /// </summary>
        public object Owner { get; set; }

        /// <summary>
        /// Creates new item as copy of this, but without owner
        /// </summary>
        /// <returns></returns>
        public abstract DiaryListItem GetCopy();

        public override string ToString()
        {
            return $"{Id} {Subject}";
        }
    }    
}
