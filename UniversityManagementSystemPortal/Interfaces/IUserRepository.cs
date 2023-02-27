using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UniversityManagementsystem.Models;
using UniversityManagementSystemPortal.ModelDto;

namespace UniversityManagementSystemPortal.Interfce
{

    public interface IUserRepository
    {
       LoginView Authenticate(Login model);
        List<User> GetAll();
     
        Task<User> GetById(int id);
        Task<User> Register(User model);
        Task<User> Update(User model);
        Task<User> Delete(int id);
    }
}
