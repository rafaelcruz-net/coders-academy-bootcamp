using AutoMapper;
using CodersAcademyBootcamp.Model;
using CodersAcademyBootcamp.Repository;
using CodersAcademyBootcamp.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodersAcademyBootcamp.Controllers
{
    [Route("api/Album/{albumId}/[controller]/")]
    [ApiController]
    public class MusicController : ControllerBase
    {
        private MusicRepository MusicRepository { get; init; }
        private AlbumRepository AlbumRepository { get; init; }
        private IMapper Mapper { get; init; }

        public MusicController(MusicRepository musicRepository, AlbumRepository albumRepository, IMapper mapper)
        {
            this.MusicRepository = musicRepository;
            this.AlbumRepository = albumRepository;
            this.Mapper = mapper;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetAllMusic(Guid albumId)
        {
            var music = await this.MusicRepository.GetMusicFromAlbum(albumId);

            return (music is null or { Count: 0 }) ? NoContent() : Ok(music);
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> SaveMusic(Guid albumId, MusicViewModel model)
        {
            if (ModelState.IsValid is false)
                return BadRequest(ModelState);

            var album = await this.AlbumRepository.GetAlbumByIdAsync(albumId);

            if (album is null)
                return NotFound();

            var music = Mapper.Map<Music>(model);

            album.AddMusic(music);

            await this.AlbumRepository.UpdateAsync(album);

            return Ok();
        }


    }
}
