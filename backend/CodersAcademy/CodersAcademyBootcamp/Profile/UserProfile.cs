using CodersAcademyBootcamp.Model;
using CodersAcademyBootcamp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademyBootcamp.Profile
{
    public class UserProfile : AutoMapper.Profile
    {
        public UserProfile()
        {
            CreateMap<UserFavoriteMusic, FavoriteMusicViewModel>()
                    .ForMember(x => x.Id, m => m.MapFrom(f => f.Id))
                    .ForMember(x => x.MusicId, m => m.MapFrom(f => f.MusicId))
                    .ForMember(x => x.Name, m => m.MapFrom(f => f.Music.Name))
                    .ForMember(x => x.Duration, m => m.MapFrom(f => f.Music.Duration))
                    .ForMember(x => x.Band, m => m.MapFrom(f => f.Music.Album.Band))
                    .ForMember(x => x.AlbumName, m => m.MapFrom(f => f.Music.Album.Name))
                    .ForMember(x => x.Backdrop, m => m.MapFrom(f => f.Music.Album.Backdrop))
                    .ForMember(x => x.AlbumId, m => m.MapFrom(f => f.Music.Album.Id));

            CreateMap<User, SignInResultViewModel>();

            CreateMap<User, UserViewModel>();

            CreateMap<RegisterViewModel, User>();

            
        }

    }
}
