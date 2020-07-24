using System;
using System.Collections.Generic;

namespace PortalRandkowy.API.Models
{
    public class User
    {
        public  int Id { get; set; }
        public string Username { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt {get;set;}

        //basic information about user

        public string Gender { get; set; }
        public DateTime DateodBirth { get; set; }
        public string ZodiacSing { get; set; }
        public DateTime Created { get; set; }

        public DateTime LastActive { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        //additional information about user
        //overlap info
        public string Growth { get; set; }
        public string EyeColor { get; set; }
        public string HairColor { get; set; }
        public string MartialStatus { get; set; }
        public string Education { get; set; }
        public string Profession { get; set; }

        public string Children { get; set; }
        public string Languages { get; set; }

        //overlap About me
        public int Motto { get; set; }
        public string Description { get; set; }
        public string Presonality { get; set; }
        public string LookingFor { get; set; }

        //overlap passions, interests
        public string Interests { get; set; }
        public string FreeTime { get; set; }
        public string Sport { get; set; }
        public string Movies { get; set; }
        public string Music { get; set; }

        //preferences
        public string ILike { get; set; }
        public string IDoNotLike { get; set; }
        public string MakesMyLaught { get; set; }
        public string ItFeelsBestIn { get; set; }
        public string FriendsWoulldDescribeMe { get; set; }

        //pictures
        public ICollection<Photo> Photos { get; set; }

    }


}