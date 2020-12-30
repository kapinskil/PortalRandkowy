using System;
using System.ComponentModel.DataAnnotations;

namespace PortalRandkowy.API.Dtos
{
    public class UserForRegisterDto
    {
        [Required(ErrorMessage="Nazwa użytkownika jest wymagana")]
        public string UserName { get; set; }
        [Required(ErrorMessage="hasło jest wymagana")]
        [StringLength(12,MinimumLength=6,ErrorMessage="hasło musi się skłądać z 4 do 8 znaków")]
        public string Password { get; set; }
        [Required]
        public string Gender {get;set;}
        [Required]
        public DateTime DateOfBirth {get;set;}
        [Required]
        public string ZodiacSign {get;set;}
        [Required]
        public string City {get;set;}
        [Required]
        public string Country {get;set;}
        public DateTime Created {get;set;}
        public DateTime LastActive {get;set;}

        public UserForRegisterDto()
        {
            Created = DateTime.Now;
            LastActive = DateTime.Now;
        }
    }
}