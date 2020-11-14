using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademyBootcamp.ViewModel
{
    public class MusicViewModel
    {

        public Guid Id { get; set; }
        
        [Required]
        public String Name { get; set; }
        
        [Required]
        public int Duration { get; set; }

    }
}
