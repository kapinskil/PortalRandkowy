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
    }
}