﻿using System.ComponentModel.DataAnnotations;

namespace ChatRoom.Models
{
    public class RegisterViewModel
    {

        public  string? UserName { get; set; }

        public string? Email { get; set; }

        public string? Password { get; set; }
    }
}