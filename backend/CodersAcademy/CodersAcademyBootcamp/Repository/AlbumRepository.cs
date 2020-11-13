using CodersAcademyBootcamp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademyBootcamp.Repository
{
    public class AlbumRepository
    {
        private BootcampContext Context { get; init; }

        public AlbumRepository( BootcampContext context)
        {
            this.Context = context;
        }

        public async Task<IList<Album>> GetAllAsync() => await this.Context.Albums.ToListAsync();

        public async Task<Album> GetAlbumByIdAsync(Guid id) => await this.Context.Albums.Include(x => x.Musics).Where(x => x.Id == id).FirstOrDefaultAsync();
    }
}
