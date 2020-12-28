using DiaryApp.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DiaryApp.Core
{
    public class AppUser : BaseEntity
    {
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }

        /// <summary>
        /// Хеш пароля
        /// </summary>
        [Required]
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Соль пароля
        /// </summary>
        [Required]
        public byte[] PasswordSalt { get; set; }
        public string ProfileImageUrl { get; set; }

        public override bool Equals(object obj)
        {
            return obj is AppUser user &&
                   Id == user.Id &&
                   Username == user.Username &&
                   EqualityComparer<byte[]>.Default.Equals(PasswordHash, user.PasswordHash) &&
                   EqualityComparer<byte[]>.Default.Equals(PasswordSalt, user.PasswordSalt) &&
                   ProfileImageUrl == user.ProfileImageUrl;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Username, PasswordHash, PasswordSalt, ProfileImageUrl);
        }

        public override string ToString()
        {
            return $"{Id} {Username}";
        }
    }
}
