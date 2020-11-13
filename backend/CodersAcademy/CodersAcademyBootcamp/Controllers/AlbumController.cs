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
    public class AlbumController : ControllerBase
    {
        private AlbumRepository AlbumRepository { get; init; }

        public AlbumController(AlbumRepository albumRepository)
        {
            this.AlbumRepository = albumRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbuns()
        {
            return Ok(await this.AlbumRepository.GetAllAsync());
        }
    }
}
