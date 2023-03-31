using System.ComponentModel.DataAnnotations;

namespace dotNFT.Data.Entities
{
    public class CollectionDto
    {

        public int Id { get; set; }

        public string Logo { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}
