using CodersAcademyBootcamp.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademyBootcamp.Repository.Mapping
{
    public class UserFavoriteMusicMapping : IEntityTypeConfiguration<UserFavoriteMusic>
    {
        public void Configure(EntityTypeBuilder<UserFavoriteMusic> builder)
        {
            builder.ToTable("UserFavoriteMusic");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).ValueGeneratedOnAdd();
            builder.HasOne(x => x.Music).WithOne().HasForeignKey<UserFavoriteMusic>(x => x.MusicId);
        }
    }
}
