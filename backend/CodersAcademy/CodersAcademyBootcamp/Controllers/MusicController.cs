using CodersAcademyBootcamp.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademyBootcamp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private AlbumRepository AlbumRepository { get; init; }

        public MusicController(AlbumRepository albumRepository)
        {
            this.AlbumRepository = albumRepository;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetMusic(Guid id)
        {
            var result = await this.AlbumRepository.GetAlbumByIdAsync(id);
            return (result != null ? Ok(result) : NotFound());
        }
    }
}
