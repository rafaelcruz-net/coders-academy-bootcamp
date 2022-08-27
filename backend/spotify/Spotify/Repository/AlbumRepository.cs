using Spotify.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Repository
{
    public class AlbumRepository
    {
        private SpotifyContext Context { get; init; }

        public AlbumRepository( SpotifyContext context)
        {
            this.Context = context;
        }

        public async Task<IList<Album>> GetAllAsync() => await this.Context.Albums.ToListAsync();

        public async Task<Album> GetAlbumByIdAsync(Guid id) => await this.Context.Albums.Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task SaveAsync(Album album)
        {
            await this.Context.AddAsync(album);
            await this.Context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Album album)
        {
            this.Context.Remove(album);
            await this.Context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Album album)
        {
            this.Context.Albums.Update(album);
            await this.Context.SaveChangesAsync();
        }
    }
}
