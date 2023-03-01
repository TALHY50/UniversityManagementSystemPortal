﻿using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.ModelDto.UserDto
{
    public class RegistorViewModel
    {
        public string FirstName { get; set; } = null!;

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? MobileNo { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; } = null!;

        public string Username { get; set; } = null!;

        public bool EmailConfirmed { get; set; }

        public string Password { get; set; } = null!;

    }
}