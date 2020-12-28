﻿using DiaryApp.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;


namespace DiaryApp.Core
{
    /// <summary>
    /// Represents base page class for diary
    /// </summary>
    public abstract class PageBase : BaseEntity
    {
        [Required]
        [Range(2020, 9999)]
        public int Year { get; set; }

        [Required]
        [Range(1, 12)]
        public int Month { get; set; }

        [Required]
        public int UserID { get; set; }

        public virtual AppUser User { get; set; }

        public PageBase()
        {

        }

        public PageBase(int year, int month, AppUser user)
        {
            Year = year;
            Month = month;
            User = user;
        }

        /// <summary>
        /// Method for descendants to implement custom creation of page areas
        /// </summary>
        public abstract void CreateAreas();

        public override string ToString()
        {
            return $"{Id} {Year} {Month} {UserID}";
        }

        public override bool Equals(object obj)
        {
            return obj is PageBase @base &&
                   Id == @base.Id &&
                   Year == @base.Year &&
                   Month == @base.Month &&
                   UserID == @base.UserID &&
                   EqualityComparer<AppUser>.Default.Equals(User, @base.User);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Year, Month, UserID, User);
        }
    }
}
