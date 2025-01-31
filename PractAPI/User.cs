﻿namespace PractAPI
{
    public class User
    {
        public string Login { get; set; }

        public string Password { get; set; }

        public string? Role { get; set; }

        public User(string login, string password, string role)
        {
            Login = login;
            Password = password;
            Role = role;
        }
    }
}
