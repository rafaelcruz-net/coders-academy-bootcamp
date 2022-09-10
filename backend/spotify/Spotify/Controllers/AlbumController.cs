using AutoMapper;
using Spotify.Model;
using Spotify.Repository;
using Spotify.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using Spotify.Infrastructure;
using Microsoft.AspNetCore.Authorization;

namespace Spotify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AlbumController : ControllerBase
    {
        private AlbumRepository AlbumRepository { get; init; }
        private IMapper Mapper { get; init; }

        private IHttpClientFactory httpClientFactory;

        private AzureBlobStorage storage;

        public AlbumController(AlbumRepository albumRepository, IMapper mapper, IHttpClientFactory httpClientFactory, AzureBlobStorage storage)
        {
            this.AlbumRepository = albumRepository;
            this.Mapper = mapper;
            this.httpClientFactory = httpClientFactory;
            this.storage = storage;
        }

        [HttpGet]
        public async Task<IActionResult> GetAlbuns()
        {
            return Ok(await this.AlbumRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAlbum(Guid id)
        {
            var result = await this.AlbumRepository.GetAlbumByIdAsync(id);

            return result is not null ? Ok(result) : NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> SaveAlbuns(AlbumViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            Album album = this.Mapper.Map<Album>(model);

            HttpClient httpClient = this.httpClientFactory.CreateClient();
            
            using var response = await httpClient.GetAsync(album.Backdrop);

            if (response.IsSuccessStatusCode)
            {
                using var stream = await response.Content.ReadAsStreamAsync();

                var fileName = $"{Guid.NewGuid()}.jpg";

                var pathStorage = await this.storage.UploadFile(fileName, stream);

                album.Backdrop = pathStorage;
                
            }
            
            await this.AlbumRepository.SaveAsync(album);

            return Created($"/{album.Id}", album);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAlbum(Guid id)
        {
            var result = await this.AlbumRepository.GetAlbumByIdAsync(id);

            if (result is null)
                return NotFound();

            await this.AlbumRepository.DeleteAsync(result);

            return Ok();
        }
    }
}
