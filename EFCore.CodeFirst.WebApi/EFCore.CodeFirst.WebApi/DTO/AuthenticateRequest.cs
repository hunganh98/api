using System.ComponentModel.DataAnnotations;

namespace EFCore.CodeFirst.WebApi.DTO
{
    public class AuthenticateRequest
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}