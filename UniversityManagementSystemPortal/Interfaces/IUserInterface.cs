using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementSystemPortal.ModelDto.NewFolder;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Interfaces
{

    public interface IUserInterface
    {
        Task<User> Authenticate(Login model);
        Task<User> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<User> RegisterAsUser(User model);
        Task<User> GetByEmailAsync(string email);
        Task<int> GetUniqueUsernameNumberAsync(string username);
        Task<User> GetByUsernameAsync(string username);
        Task<bool> SaveChangesAsync();
    }
}
