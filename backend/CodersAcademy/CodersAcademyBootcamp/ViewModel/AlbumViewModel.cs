using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademyBootcamp.ViewModel
{
    public class AlbumViewModel : IValidatableObject
    {
        public Guid Id { get; set; }
        
        [Required]
        public String Name { get; set; }


        [Required]
        public String Band { get; set; }


        [Required]
        public String Description { get; set; }
        
        [Required]
        public String Backdrop { get; set; }

        [Required]
        public List<MusicViewModel> Musics { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (this.Musics == null)
                results.Add(new ValidationResult("Album must contain at least one music"));

            foreach (var item in this.Musics)
                Validator.TryValidateObject(item, new ValidationContext(item), results);

            return results;
            
        }
    }
}
