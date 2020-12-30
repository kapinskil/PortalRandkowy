using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Dtos;
using PortalRandkowy.API.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using AutoMapper;

namespace PortalRandkowy.API.Controllers
{
    
    //http://localhost:5000/api
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        public AuthController(IAuthRepository repository, IConfiguration configuration, IMapper mapper) 
        {
            _repository = repository;
            _configuration = configuration;
            _mapper = mapper;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserForRegisterDto userForRegisterDto)
        { 

             userForRegisterDto.UserName = userForRegisterDto.UserName.ToLower();

             if(await _repository.UserExists(userForRegisterDto.UserName))
                return BadRequest("Użytkownik o takiej nazwie już istnieje");

            var userToCreate = _mapper.Map<User>(userForRegisterDto);

            var creareduser = await _repository.Register(userToCreate,userForRegisterDto.Password);
            
            var userToReturn = _mapper.Map<UserForDetailsDto>(creareduser);

            return CreatedAtRoute("GetUser", new {Controller = "Users", Id = creareduser.Id}, userToReturn );
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(UserForLoginDto userForLoginDto)
        {

            var userFromRepo = await _repository.Login(userForLoginDto.Username.ToLower(), userForLoginDto.Password);

            if(userFromRepo == null)
                return Unauthorized();

            //create Token
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name, userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSetings:Tokien").Value));
           
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha512Signature);

            var tokienDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddHours(12),
                SigningCredentials = creds
            };

            var tokenHendler = new JwtSecurityTokenHandler();
            var token = tokenHendler.CreateToken(tokienDescriptor);
            var user = _mapper.Map<UserForListDto>(userFromRepo);

            return Ok(new 
            { 
                token = tokenHendler.WriteToken(token),
                user
            });
        }
    }
}