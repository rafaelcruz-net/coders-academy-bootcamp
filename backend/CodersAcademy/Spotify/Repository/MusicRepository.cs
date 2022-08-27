using Spotify.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Repository
{
    public class MusicRepository
    {
        private SpotifyContext Context { get; init; }

        public MusicRepository(SpotifyContext context)
        {
            this.Context = context;
        }

        public async Task<IList<Music>> GetMusicFromAlbum(Guid albumId) => await this.Context.Albums.Include(x => x.Musics)
                                                                                                    .Where(x => x.Id == albumId)
                                                                                                    .SelectMany(x => x.Musics)
                                                                                                    .ToListAsync();

        public async Task<Music> GetMusicAsync(Guid musicId) => await this.Context.Musics.Where(x => x.Id == musicId).FirstOrDefaultAsync();

    }
}
