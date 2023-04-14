﻿using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Enum;

namespace UniversityManagementSystemPortal.Models.ModelDto.UserDto
{
    public class UserViewModel
    {

        public string FirstName { get; set; }

        public string? MiddleName { get; set; }

        public string? LastName { get; set; }

        public string? MobileNo { get; set; }

        public DateTime? DateOfBirth { get; set; }

        public BloodGroup BloodGroup { get; set; }

        public Gender Gender { get; set; }

        public string Email { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? EmailConfirmed
        {
            get; set;
        }
        public DateTime? CreatedAt { get; set; }

        public Guid? CreatedBy { get; set; }

        public DateTime? UpdatedAt { get; set; }

        public Guid? UpdatedBy { get; set; }
    }
}