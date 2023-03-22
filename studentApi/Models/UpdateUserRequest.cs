using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace studentApi.Models
{
    public class UpdateUserRequest
    {

        //[Required]
        //[Column(TypeName = "nvarchar(100)")]
        //public string UserName { get; set; } = "";
        [Required]
        public string Password { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string UserFirstName { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string UserLastName { get; set; } = "";
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; } = "";


        [Required]
        [MaxLength(11, ErrorMessage = "max 11 character is required")]
        [MinLength(11, ErrorMessage = "Min 11 Character only")]
        public string Phone { get; set; } = "";

        [Column(TypeName = "nvarchar(500)")]
        [Required]
        public string Address { get; set; } = "";

        [Required]
        [Column(TypeName = "nvarchar(200)")]
        public string Role { get; set; }


        public string UserName
        {
            get
            {
                return UserFirstName + " " + UserLastName;
            }
        }

    }
}
