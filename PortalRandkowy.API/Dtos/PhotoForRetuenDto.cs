using System;

namespace PortalRandkowy.API.Dtos
{
    public class PhotoForRetuenDto
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }     // Opis
        public DateTime DateAdded { get; set; }     // Data dodania
        public bool IsMain { get; set; }  // czy główne

        public string PublicId {get;set;}
    }
}