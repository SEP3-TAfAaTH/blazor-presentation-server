using System.ComponentModel.DataAnnotations;

namespace BlazorPresentationServer.Models
{
    public class Account
    {
        [Required]
        public string Username { get; set; }
        
        [Required]
        [MinLength(6, ErrorMessage = "The password must be a minimum of 6 characters")]
        public string Password { get; set; }
        
    }
}