using Spotify.Model;
using Spotify.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Spotify.Profile
{
    public class AlbumProfile : AutoMapper.Profile
    {

        public AlbumProfile()
        {
            this.CreateMap<AlbumViewModel, Album>();
            this.CreateMap<MusicViewModel, Music>();
        }

    }
}
