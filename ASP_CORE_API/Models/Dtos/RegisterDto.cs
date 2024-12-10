using System.ComponentModel.DataAnnotations;

namespace ASP_CORE_API.Models.Dtos
{
    public class RegisterDto
    {
        [Required]
        [DataType(DataType.EmailAddress)]
        public string Username { get; set; }  
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }  
        public string[] Roles { get; set; }
    }
}
