using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using UniversityManagementSystemPortal.Enum;
using UniversityManagementSystemPortal.ModelDto.Category;
using UniversityManagementSystemPortal.ModelDto.Department;
using UniversityManagementSystemPortal.ModelDto.Employee;
using UniversityManagementSystemPortal.ModelDto.Position;
using UniversityManagementSystemPortal.Models.ModelDto.Department;

namespace UniversityManagementSystemPortalWeb.ViewModels
{
    public class EmployeeUserViewModel
    {
        public RegistorUserDto userDto { get; set; } = new RegistorUserDto();

        public CreateEmployeeDto createEmployeeDto { get; set; } = new CreateEmployeeDto();


    }
}
