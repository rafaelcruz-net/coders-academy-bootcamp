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
            CreateMap<User, SignInResultViewModel>();

            CreateMap<RegisterViewModel, User>();
        }

    }
}
