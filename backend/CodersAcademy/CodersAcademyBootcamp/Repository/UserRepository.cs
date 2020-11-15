using CodersAcademyBootcamp.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademyBootcamp.Repository
{
    public class UserRepository
    {
        private BootcampContext Context { get; init; }

        public UserRepository(BootcampContext context)
        {
            this.Context = context;
        }

        public async Task<User> AuthenticateAsync(string email, string password)
        {
            return await this.Context.Users.Where(x => x.Password == password && x.Email == email).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(User user)
        {
            await this.Context.Users.AddAsync(user);
            await this.Context.SaveChangesAsync();
        }

        public async Task<IList<UserFavoriteMusic>> GetFavoriteMusicAsync(Guid id) => await this.Context.Users
                                                                                                   .Include(x => x.FavoriteMusics)
                                                                                                   .Where(x => x.Id == id)
                                                                                                   .SelectMany(x => x.FavoriteMusics)
                                                                                                   .ToListAsync();

        public async Task<User> GetUserAsync(Guid id) => await this.Context.Users.Include(x => x.FavoriteMusics).Where(x => x.Id == id).FirstOrDefaultAsync();

        public async Task UpdateAsync(User user)
        {
            this.Context.Users.Update(user);
            await this.Context.SaveChangesAsync();

        }
    }
}
