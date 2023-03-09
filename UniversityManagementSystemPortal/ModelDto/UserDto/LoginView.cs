﻿using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.NewFolder
{
    public class LoginView
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }

        public string Token { get; set; }

        public LoginView(User user, string token)
        {
            Id = user.Id;
            Username = user.Username;
            Email = user.Email;
            Token = token;
        }
    }


}
