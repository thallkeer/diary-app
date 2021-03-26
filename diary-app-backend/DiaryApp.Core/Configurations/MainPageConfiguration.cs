using DiaryApp.Core.Entities.Pages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiaryApp.Core.Configurations
{
    public class MainPageConfiguration : IEntityTypeConfiguration<MainPage>
    {
        public void Configure(EntityTypeBuilder<MainPage> builder)
        {
            builder.HasIndex(mp => new { mp.UserId, mp.Year, mp.Month }).IsUnique();
        }
    }
}
