using CodersAcademyBootcamp.Model;
using CodersAcademyBootcamp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademyBootcamp.Profile
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
