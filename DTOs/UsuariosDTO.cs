using System.ComponentModel.DataAnnotations;

namespace DTOs
{
    public class UsuariosDTO
    {
        [Required]
        public string NameUser {get; set;}
        [Required]
        public string PhoneNumber {get; set;}
        [Required]
        [EmailAddress]
        public string Email {get; set;}
        [Required]
        public string Password {get; set;}
    }

    public class LoginDTO
    {
        [Required]
        public string NameUser {get; set;}
        [Required]
        public string Password {get; set;}
    }

    public class HacerAdminDTO
    {
        [Required]
        public string UserName { get; set;}
    }
}