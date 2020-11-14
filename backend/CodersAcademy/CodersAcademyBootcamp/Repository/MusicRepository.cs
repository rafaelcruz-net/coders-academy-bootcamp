using CodersAcademyBootcamp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademyBootcamp.Repository
{
    public class MusicRepository
    {
        private BootcampContext Context { get; init; }

        public MusicRepository(BootcampContext context)
        {
            this.Context = context;
        }

        public async Task<IList<Music>> GetMusicFromAlbum(Guid albumId) => await this.Context.Albums.Include(x => x.Musics)
                                                                                                    .Where(x => x.Id == albumId)
                                                                                                    .SelectMany(x => x.Musics)
                                                                                                    .ToListAsync();
    }
}
