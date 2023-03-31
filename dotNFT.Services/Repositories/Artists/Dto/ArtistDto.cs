using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using dotNFT.Data;
using dotNFT.Services.Repositories.Users;
using dotNFT.Data.Entities;
namespace dotNFT.Services.Repositories.Artists
{
    public class ArtistDto
    {
        public int Id { get; set; }
        public string ProfilePictureURL { get; set; }
        public string ArtistName { get; set; }
        public string Bio { get; set; }
        public User User { get; set; }

        
    }
}
