using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BlazorPresentationServer.Model
{
    public class Account
    {
        public long Id { get; set; }
        [Required]
        public string Username { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "The password must be a minimum of 6 characters")]
        public string Password { get; set; }
        
        [DefaultValue(10000)]
        public Decimal Balance { get; set; }
    }
}