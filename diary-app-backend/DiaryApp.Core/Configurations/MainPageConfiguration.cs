using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
