﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core.Entities
{
    /// <summary>
    /// Represents base class for a diary list item.
    /// </summary>
    public abstract class DiaryListItem : BaseEntity
    {
        public DiaryListItem()
        { }

        protected DiaryListItem(DiaryListItem original)
        {
            ArgumentNullException.ThrowIfNull(original);
            Subject = original.Subject;
            Url = original.Url;
        }

        /// <summary>
        /// Outer link for a list item
        /// </summary>
        [DataType(DataType.Url)]
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Text content of an item
        /// </summary>
        [MaxLength(200)]
        public string Subject { get; set; } = string.Empty;

        /// <summary>
        /// Id of list which contains item
        /// </summary>
        [Required]
        public int OwnerID { get; set; }

        /// <summary>
        /// Creates new item as copy of this, but without an owner
        /// </summary>
        /// <returns></returns>
        public abstract DiaryListItem GetCopy();

        public override string ToString()
        {
            return $"{Id} {Subject}";
        }
    }
}
