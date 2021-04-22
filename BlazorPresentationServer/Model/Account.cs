using System.ComponentModel.DataAnnotations;

namespace BlazorPresentationServer.Model
{
    public class Account
    {
        public int Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "The password must be a minimum of 6 characters")]
        public string Password { get; set; }
    }
}