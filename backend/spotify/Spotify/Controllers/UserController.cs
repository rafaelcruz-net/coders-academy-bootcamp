using AutoMapper;
using Spotify.Model;
using Spotify.Repository;
using Spotify.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Spotify.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private UserRepository UserRepository { get; init; }
        private MusicRepository MusicRepository { get; init; }
        private IMapper Mapper { get; init; }

        public UserController(UserRepository userRepository, IMapper mapper, MusicRepository musicRepository)
        {
            this.UserRepository = userRepository;
            this.Mapper = mapper;
            this.MusicRepository = musicRepository;
        }

        [HttpPost("authenticate")]
        public async Task<IActionResult> SignIn(SignInViewModel model)
        {
            if (ModelState is { IsValid: false })
                return BadRequest();

            var user = await this.UserRepository.AuthenticateAsync(model.Email, Convert.ToBase64String(Encoding.UTF8.GetBytes(model.Password)));

            if (user is null)
                return UnprocessableEntity();

            var result = this.Mapper.Map<SignInResultViewModel>(user);

            return Ok(result);

        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState is { IsValid: false })
                return BadRequest();

            var user = this.Mapper.Map<User>(model);
            user.Photo = $"https://robohash.org/{Guid.NewGuid()}.png?bgset=any";
            user.Password = Convert.ToBase64String(Encoding.UTF8.GetBytes(user.Password));

            await this.UserRepository.CreateAsync(user);

            var result = this.Mapper.Map<SignInResultViewModel>(user);

            return Created($"{result.Id}", result);

        }

        [HttpGet("{id}/favorite-music")]
        public async Task<IActionResult> GetUserFavoriteMusic(Guid id)
        {
            return Ok(await this.UserRepository.GetFavoriteMusicAsync(id));
        }

        [HttpPost("{id}/favorite-music/{musicId}")]
        public async Task<IActionResult> SaveUserFavoriteMusic(Guid id, Guid musicId)
        {
            var music = await this.MusicRepository.GetMusicAsync(musicId);

            var user = await this.UserRepository.GetUserAsync(id);

            user.AddFavoriteMusic(music);

            await this.UserRepository.UpdateAsync(user);

            return Ok(this.Mapper.Map<UserViewModel>(user));
        }

        [HttpDelete("{id}/favorite-music/{musicId}")]
        public async Task<IActionResult> RemoveUserFavoriteMusic(Guid id, Guid musicId)
        {
            var music = await this.MusicRepository.GetMusicAsync(musicId);

            var user = await this.UserRepository.GetUserAsync(id);

            user.RemoveFavoriteMusic(music);

            await this.UserRepository.UpdateAsync(user);

            return Ok(this.Mapper.Map<UserViewModel>(user));
        }

    }
}
