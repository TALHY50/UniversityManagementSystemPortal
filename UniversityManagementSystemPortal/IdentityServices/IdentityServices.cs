﻿using UniversityManagementsystem.Models;

namespace UniversityManagementSystemPortal.IdentityServices
{
    public interface IIdentityServices
    {
        public Guid? GetUserId();
    }
    public class IdentityServices : IIdentityServices
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public IdentityServices(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
        }
        public Guid? GetUserId()
        {
            return GetUser()?.Id;
        }

        private User? GetUser()
        {
            return (User?)_httpContextAccessor.HttpContext?.Items.FirstOrDefault(item => item.Key.Equals("User")).Value;
        }
    }
}
