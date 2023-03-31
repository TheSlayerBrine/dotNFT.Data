using System.ComponentModel.DataAnnotations;

namespace dotNFT.Web.Models.Users
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [MaxLength(50, ErrorMessage = "The first name is too long!")]
        public string FirstName { get; set; }

        [MaxLength(50, ErrorMessage = "The last name is too long!")]
        public string LastName { get; set; }

        [MaxLength(50, ErrorMessage = "The Username is too long!")]
        public string UserName { get; set; }

        [EmailAddress(ErrorMessage = "The email is invalid")]
        public string Email { get; set; }
    }
}
