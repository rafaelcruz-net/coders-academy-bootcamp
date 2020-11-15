﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademyBootcamp.ViewModel
{
    public class SignInViewModel
    {
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        
        public String Password { get; set; }
    }
}
