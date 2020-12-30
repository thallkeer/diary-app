using Newtonsoft.Json;
using System;

namespace DiaryApp.Core.Models
{
    /// <summary>
    /// Base class for all application entities
    /// </summary>
    public abstract class BaseEntity
    {
        public int Id { get; set; }

		protected bool Equals(BaseEntity other)
		{
			return string.Equals(Id, other.Id);
		}

		public override bool Equals(object obj)
		{
			if (obj is null) return false;
			if (ReferenceEquals(this, obj)) return true;
			return obj is BaseEntity @base && Equals(@base);
		}

		public override int GetHashCode()
		{
			return Id.GetHashCode();
		}

		public override string ToString()
        {
            return base.ToString();
            return JsonConvert.SerializeObject(this);
        }
    }
}
