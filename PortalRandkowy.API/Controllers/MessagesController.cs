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
    [Route("api/users/{userId}/[controller]")]
    [ApiController]

    public class MessagesController : ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        public MessagesController(IUserRepository repository, IMapper mapper)
        {
            _mapper = mapper;
            _repository = repository;
        }
        
        [HttpGet("{id}", Name = "GetMessage")]
        public async Task<IActionResult> GetMessage(int userId, int id)
        {
            if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var messageFromRepo = await _repository.GetMessage(id);

            if(messageFromRepo == null)
                return NotFound();
            
            return Ok(messageFromRepo);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMessage(int userId, MessageForCreationDto messageForCreationDto)
        {
             if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            messageForCreationDto.SenderId = userId;
            
            var recipient = await _repository.GetUser(messageForCreationDto.RecipientId);

            if(recipient == null)
                return BadRequest("Nie można znaleść użytkownika");

            var message = _mapper.Map<Message>(messageForCreationDto);

            _repository.Add(message);
            var messageToReturn = _mapper.Map<MessageForCreationDto>(message);

            if(await _repository.SaveAll())
                return CreatedAtRoute(nameof(GetMessage), new {userId, id = message.Id}, messageToReturn);
            
            throw new Exception("Utworzeniew wiadomości nie powiodło się przy zapisie");
        
        }
    }
}