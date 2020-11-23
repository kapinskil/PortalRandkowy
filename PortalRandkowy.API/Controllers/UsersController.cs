using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Dtos;
using System;
using System.Security.Claims;
using PortalRandkowy.API.Helpers;
using PortalRandkowy.API.Models;


namespace PortalRandkowy.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    //http://localhost:5000/api
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(IUserRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers([FromQuery]UserParams userParams)
        {

                var currentUserId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value);
                var userFromRepo = await _repo.GetUser(currentUserId);

                userParams.UserId = currentUserId;

                if(string.IsNullOrEmpty(userParams.Gender))
                {
                    userParams.Gender = userFromRepo.Gender == "mężczyzna" ? "kobieta": "mężczyzna";
                }

                var users = await _repo.GetUsers(userParams);

                var usrsReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);
                
                Response.AddPagination(users.CurrentPage, users.CurrentPage, users.TotalCount, users.TotalPages);

                return Ok(usrsReturn);
        }

        [HttpGet("{id}", Name="GetUser")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailsDto>(user);

            return Ok(userToReturn);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if(await _repo.SaveAll())
                return NoContent();
            throw new Exception($"Aktualizacja uzytkownika o id {id} nie powiodła się");
        }

        [HttpPost("{id}/like/{recipientId}")]
        public async Task<IActionResult> LikeUser(int id, int recipientId)
        {
             if(id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

                var like = await _repo.GetLike(id,recipientId);

                if(like != null)
                    return BadRequest("Juz lubisz tego użytkownika");

                if(await _repo.GetUser(recipientId) == null)
                    return NotFound();
                
                like = new Like
                {
                    UserLikesId = id,
                    UserIsLikedId = recipientId
                };

                _repo.Add<Like>(like);

                if(await _repo.SaveAll())
                    return Ok();
                    
                return BadRequest("Nie możan polubic użytkownika");
        }
    }
}