using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademyBootcamp.Model
{
    public class User
    {
        public Guid Id { get; set; }
        public String Name { get; set; }
        public String Email { get; set; }
        public String Password { get; set; }
        public String Photo { get; set; }
        public IList<UserFavoriteMusic> FavoriteMusics { get; set; }

        internal void AddFavoriteMusic(Music music)
        {
            this.FavoriteMusics.Add(new UserFavoriteMusic()
            {
                Music = music,
                MusicId = music.Id,
                User = this
            });
        }

        internal void RemoveFavoriteMusic(Music music)
        {
            var favoriteMusic = this.FavoriteMusics.Where(x => x.MusicId == music.Id).FirstOrDefault();
            this.FavoriteMusics.Remove(favoriteMusic);
        }
    }
}
