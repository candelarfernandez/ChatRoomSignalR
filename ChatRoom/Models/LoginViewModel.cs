﻿namespace ChatRoom.Models
{
    public class LoginViewModel
    {
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool RememberMe { get; set; }
    }
}