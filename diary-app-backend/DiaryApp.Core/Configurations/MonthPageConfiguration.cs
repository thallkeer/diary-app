using DiaryApp.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiaryApp.Core.Configurations
{
    public class MonthPageConfiguration : IEntityTypeConfiguration<MonthPage>
    {
        public void Configure(EntityTypeBuilder<MonthPage> builder)
        {
            builder.HasIndex(mp => new { mp.UserId, mp.Year, mp.Month }).IsUnique();
        }
    }
}
