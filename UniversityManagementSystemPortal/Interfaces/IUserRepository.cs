using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto.NewFolder;

namespace UniversityManagementSystemPortal.Interfce
{

    public interface IUserRepository
    {
       LoginView Authenticate(Login model);
        Task<User> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task<User> RegisterAsUser(User model);
 
    }
}
