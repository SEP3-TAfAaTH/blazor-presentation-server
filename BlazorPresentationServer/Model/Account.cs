﻿using System.ComponentModel.DataAnnotations;

namespace BlazorPresentationServer.Model
{
    public class Account
    {
        public long Id { get; set; }
        public string Username { get; set; }
        [Required]
        [MinLength(6, ErrorMessage = "The password must be a minimum of 6 characters")]
        public string Password { get; set; }
    }
}