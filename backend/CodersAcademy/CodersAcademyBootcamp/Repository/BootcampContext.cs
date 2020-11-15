using CodersAcademyBootcamp.Model;
using CodersAcademyBootcamp.Repository.Mapping;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademyBootcamp.Repository
{
    public class BootcampContext : DbContext
    {
        public DbSet<Album> Albums { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserFavoriteMusic> UsersPlaylist { get; set; }

        public BootcampContext(DbContextOptions<BootcampContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AlbumMapping());
            modelBuilder.ApplyConfiguration(new MusicMapping());
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new UserFavoriteMusicMapping());

            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(LoggerFactory.Create(x => x.AddConsole()));
            base.OnConfiguring(optionsBuilder);
        }

    }
}
