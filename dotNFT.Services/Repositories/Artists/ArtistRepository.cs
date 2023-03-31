
using dotNFT.Data;
using dotNFT.Data.Entities;
using dotNFT.Services.Repositories.Users;
using Microsoft.EntityFrameworkCore;
using System.Net.NetworkInformation;

namespace dotNFT.Services.Repositories.Artists
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly AppDbContext _context;

        public ArtistRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateArtist(ArtistDto artistDto)
        {

            if (_context.Users.Any(a => a.UserName == artistDto.ArtistName))
            {
                throw new Exception("Cannot insert a new User with the same Username.");
            }



            var ArtistEntity = new Artist
            {
                Id = artistDto.Id,
                ArtistName = artistDto.ArtistName,
                ProfilePictureURL = artistDto.ProfilePictureURL,
                Bio = artistDto.Bio,
                User = artistDto.User,
            };

            _context.Artists.Add(ArtistEntity);
            _context.SaveChanges();
        }

        public List<ArtistDto> GetAll()
        {
            return _context.Artists
                .Select(a => new ArtistDto
                {
                    Id = a.Id,
                    ProfilePictureURL = a.ProfilePictureURL,
                    ArtistName = a.ArtistName,
                    Bio = a.Bio,
                    User = a.User,
                })
                .ToList();
        }

        public ArtistDto? GetArtist(int artistId)
        {
            if (artistId <= 0) throw new ArgumentOutOfRangeException(nameof(artistId));

            var artist = _context.Artists.SingleOrDefault(a => a.Id == artistId);

            if (artist == null) return null;

            var artistDto = new ArtistDto
            {
                Id = artistId,
                ArtistName = artist.ArtistName,
                Bio = artist.Bio,
                User = artist.User,
            };

            return artistDto;
        }

        public ArtistDto? GetArtistByEmail(string email)
        {
            if (string.IsNullOrEmpty(email)) throw new ArgumentNullException(nameof(email));

            var artist = _context.Artists.SingleOrDefault(a => a.User.Email == email);

            if (artist == null) return null;

            var artistDto = new ArtistDto
            {
                Id = artist.Id,
                ArtistName = artist.ArtistName,
                Bio = artist.Bio,
                User = artist.User,
            };

            return artistDto;
        }

        public IEnumerable<ArtistDto> SearchByName(string searchTerm)
        {

            return _context.Artists
               .Where(a => a.ArtistName.Contains(searchTerm))
               .Select(a => new ArtistDto
               {
                   Id = a.Id,
                   ProfilePictureURL = a.ProfilePictureURL,
                   ArtistName = a.ArtistName,
                   Bio = a.Bio,
                   User = a.User,

               })
               .ToList();
        }
        public void DeleteArtist(int artistId)
        {
            if (artistId <= 0) throw new ArgumentOutOfRangeException(nameof(artistId));

            var artistToDelete = _context.Users.Find(artistId);

            if (artistToDelete != null)
            {
                _context.Users.Remove(artistToDelete);
            }
        }
        public void UpdateArtist(ArtistDto artistDto)
        {
            if (artistDto == null) throw new ArgumentNullException(nameof(artistDto));
            if (string.IsNullOrEmpty(artistDto.ArtistName)) throw new ArgumentException($"{nameof(artistDto.ArtistName)} cannot be null or empty.");


            var artistToUpdate = _context.Artists.Find(artistDto.Id);
            if (artistToUpdate == null)
            {
                throw new Exception("The Artist has not been found");
            }
            artistToUpdate.ArtistName = artistDto.ArtistName;
            artistToUpdate.Bio = artistDto.Bio;
            artistToUpdate.User = artistDto.User;
            artistToUpdate.ProfilePictureURL = artistDto.ProfilePictureURL;
            artistToUpdate.Id = artistDto.Id;


        }
    }
}