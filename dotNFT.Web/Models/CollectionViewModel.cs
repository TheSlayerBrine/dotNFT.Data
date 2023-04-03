using dotNFT.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace dotNFT.Models
{
    public class CollectionViewModel 
    {
        public int Id { get; set; }

        [Display(Name = "Collection Logo")]
        [Required(ErrorMessage = "Collection logo is required")]
        public string Logo { get; set; }

        [Display(Name = "Collection Name")]
        [Required(ErrorMessage = "Collection name is required")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "Collection description is required")]
        public string Description { get; set; }

        //Relationships
        public List<NFT> NFTs { get; set; }
    }
}
