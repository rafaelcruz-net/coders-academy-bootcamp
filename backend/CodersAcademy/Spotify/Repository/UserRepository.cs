using Spotify.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Repository
{
    public class UserRepository
    {
        private SpotifyContext Context { get; init; }

        public UserRepository(SpotifyContext context)
        {
            this.Context = context;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            return await this.Context.Users
                                     .Include(x => x.FavoriteMusics)
                                     .ThenInclude(x => x.Music)
                                     .ThenInclude(x => x.Album)
                                     .Where(x => x.Password == password && x.Email == email)
                                     .FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            await this.Context.Users.AddAsync(user);
            await this.Context.SaveChangesAsync();
        }

        public async Task<IList<UserFavoriteMusic>> GetFavoriteMusicAsync(Guid id) => await this.Context.Users
                                                                                                   .Include(x => x.FavoriteMusics)
                                                                                                   .ThenInclude(x => x.Music)
                                                                                                   .ThenInclude(x => x.Album) 
                                                                                                   .Where(x => x.Id == id)
                                                                                                   .SelectMany(x => x.FavoriteMusics)
                                                                                                   .ToListAsync();

        public async Task<User> GetUserAsync(Guid id) => await this.Context.Users
                                                                           .Include(x => x.FavoriteMusics)
                                                                           .ThenInclude(x => x.Music)
                                                                           .ThenInclude(x => x.Album)
                                                                           .Where(x => x.Id == id)
                                                                           .FirstOrDefaultAsync();

        public async Task UpdateAsync(User user)
        {
            this.Context.Users.Update(user);
            await this.Context.SaveChangesAsync();

        }
    }
}
