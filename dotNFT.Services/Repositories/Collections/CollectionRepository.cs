using dotNFT.Data.Entities;
using dotNFT.Services.Repositories.Collections;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using dotNFT.Data;

namespace dotNFT.Services.Repositories.Collections
{
    public class CollectionRepository : ICollectionRepository
    {
        private readonly AppDbContext _context;

        public CollectionRepository(AppDbContext context)
        {
            _context = context;
        }

        public void CreateCollection(CollectionDto CollectionDto)
        {
            if (CollectionDto == null) throw new ArgumentNullException(nameof(CollectionDto));
            if (string.IsNullOrEmpty(CollectionDto.Name)) throw new ArgumentException($"{nameof(CollectionDto.Name)} cannot be null or empty.");
            //Add Collection Photo

            if (_context.Collections.Any(u => u.Name == CollectionDto.Name))
            {
                throw new Exception("Can't create collection with existing name.");
            }

            var CollectionEntity = new Collection
            {
                Name = CollectionDto.Name,
                Id = CollectionDto.Id,
                Description = CollectionDto.Description,
                Logo = CollectionDto.Logo,
            };

            _context.Collections.Add(CollectionEntity);
            _context.SaveChanges();
        }

        public void DeleteCollection(int CollectionId)
        {
            if (CollectionId <= 0) throw new ArgumentOutOfRangeException(nameof(CollectionId));

            var CollectionToDelete = _context.Collections.Find(CollectionId);

            if (CollectionToDelete != null)
            {
                _context.Collections.Remove(CollectionToDelete);
            }
        }

        public List<CollectionDto> GetAll()
        {
            return _context.Collections
                .Select(u => new CollectionDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Description = u.Description,
                    Logo = u.Logo,

                   
                })
                .ToList();
        }

        public CollectionDto? GetCollection(int CollectionId)
        {
            if (CollectionId <= 0) throw new ArgumentOutOfRangeException(nameof(CollectionId));

            var Collection = _context.Collections.SingleOrDefault(u => u.Id == CollectionId);

            if (Collection == null) return null;

            var CollectionDto = new CollectionDto
            {
                Name = Collection.Name,
                Id = Collection.Id,
                Description = Collection.Description,
                Logo = Collection.Logo,
            };

            return CollectionDto;
        }


        public IEnumerable<CollectionDto> SearchByName(string searchTerm)
        {
            return _context.Collections
               .Where(u => u.Name.Contains(searchTerm))
               .Select(u => new CollectionDto
               {})
               .ToList();
        }

        public void UpdateCollection(CollectionDto CollectionDto)
        {
            if (CollectionDto == null) throw new ArgumentNullException(nameof(CollectionDto));
            if (string.IsNullOrEmpty(CollectionDto.Name)) throw new ArgumentException($"{nameof(CollectionDto.Name)} cannot be null or empty.");
          //Add Collection Photo
            var CollectionToUpdate = _context.Collections.Find(CollectionDto.Id);
            if (CollectionToUpdate == null)
            {
                throw new Exception("The Collection has not been found");
            }

            CollectionToUpdate.Name = CollectionDto.Name;
            CollectionToUpdate.Logo = CollectionDto.Logo;
        }
    }
}