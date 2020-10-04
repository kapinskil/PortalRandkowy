using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PortalRandkowy.API.Data;
using PortalRandkowy.API.Dtos;
using PortalRandkowy.API.Helpers;
using PortalRandkowy.API.Models;

namespace PortalRandkowy.API.Controllers
{
    [Authorize]
    [Route("api/users/{userId}/photos")]
    [ApiController]
    
    public class PhotosController: ControllerBase
    {
        private readonly IUserRepository _repository;
        private readonly IMapper _mapper;
        private readonly IOptions<CloudinarySettings> _cloudinaryConfig;
        private Cloudinary _cloudinary;

        public PhotosController(IUserRepository repository, IMapper mapper, IOptions<CloudinarySettings> cloudinaryConfig)
        {
            _cloudinaryConfig = cloudinaryConfig;
            _mapper = mapper;
            _repository = repository;

            Account account = new Account(
            _cloudinaryConfig.Value.CluoudName,
            _cloudinaryConfig.Value.ApiKey,
            _cloudinaryConfig.Value.ApiSecret
            );

            _cloudinary = new Cloudinary(account);
        }

        [HttpPost]
        public async Task<IActionResult> AddPhotoForUser(int userid, [FromForm]PhotoForCreationDto photoForCreationDto)
        {
               if(userid != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();
            
            var userFromRepo = await _repository.GetUser(userid);

            var file = photoForCreationDto.File;
            var uplodeResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using(var stream = file.OpenReadStream())
                {
                    var uploadParameters = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };

                    uplodeResult = _cloudinary.Upload(uploadParameters);
                }
            }

            photoForCreationDto.Url = uplodeResult.Uri.ToString();
            photoForCreationDto.PublicId = uplodeResult.PublicId;

            var photo = _mapper.Map<Photo>(photoForCreationDto);

            if (!userFromRepo.Photos.Any(p => p.IsMain))
                photo.IsMain = true;

            userFromRepo.Photos.Add(photo);

            if (await  _repository.SaveAll())
            {
                var photoToReturn = _mapper.Map<PhotoForRetuenDto>(photo);
                //return CreatedAtRoute("GetPhoto", new { id = photo.Id}, photoToReturn);
                return CreatedAtRoute("GetPhoto", new { userid, id = photo.Id}, photoToReturn);
            }
                

            return BadRequest("Nie można dodać zdjęcia");
        }

        [HttpGet("{id}", Name = "GetPhoto")]
        public async Task<IActionResult> GetPhoto(int id)
        {
            var photoFromRepo = await _repository.GetPhoto(id);

            var photoForReturn = _mapper.Map<PhotoForRetuenDto>(photoFromRepo);

            return Ok(photoForReturn);
        }

        [HttpPost("{id}/setMain")]
        public async Task<IActionResult> SetMainPhoto(int userId, int id)
        {
             if(userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var user = await _repository.GetUser(userId);

            if(!user.Photos.Any(p => p.Id == id))
                return Unauthorized();

            var pohotoFromRepo = await _repository.GetPhoto(id);

            if(pohotoFromRepo.IsMain)
                return BadRequest("To już jest główne zdjęcie");
            
            var currentMainPhoto = await _repository.GetMeinPhotoForUser(userId);
            currentMainPhoto.IsMain = false;
            pohotoFromRepo.IsMain = true;

            if(await _repository.SaveAll())
                return NoContent();

            return BadRequest("Nie mozna ustawić zdjęcia jako głównego"); 
        }

    }
}