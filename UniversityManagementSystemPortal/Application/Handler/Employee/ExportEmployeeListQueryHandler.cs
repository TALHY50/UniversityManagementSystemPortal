using AutoMapper;
using MediatR;
using System.Text;
using UniversityManagementSystemPortal.Application.Qurey.Employee;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Employee;

namespace UniversityManagementSystemPortal.Application.Handler.Employee
{
    public class ExportEmployeeListQueryHandler : IRequestHandler<ExportEmployeeListQuery, byte[]>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public ExportEmployeeListQueryHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<byte[]> Handle(ExportEmployeeListQuery request, CancellationToken cancellationToken)
        {
            var employees =  _employeeRepository.GetAllAsync();
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);

            var csvContent = new StringBuilder();
            csvContent.AppendLine("Employee No,First Name,Middle Name,Last Name,Mobile No,Date of Birth,Blood Group,Gender,Employee Type,Employee Address,Joining Date,Institute Name,Department Name,Department Code,Position Name,Is Active,Created At,Created By,Updated At,Updated By");

            foreach (var employeeDto in employeeDtos)
            {
                csvContent.AppendLine(
                    $"{employeeDto.EmployeeNo}," +
                    $"{employeeDto.FirstName}," +
                    $"{employeeDto.MiddleName ?? ""}," +
                    $"{employeeDto.LastName ?? ""}," +
                    $"{employeeDto.MobileNo ?? ""}," +
                    $"{employeeDto.DateOfBirth?.ToString("yyyy-MM-dd") ?? ""}," +
                    $"{employeeDto.BloodGroup}," +
                    $"{employeeDto.Gender}," +
                    $"{employeeDto.EmployeeType}," +
                    $"{employeeDto.EmployeAddress ?? ""}," +
                    $"{employeeDto.JoiningDate?.ToString("yyyy-MM-dd") ?? ""}," +
                    $"{employeeDto.InstituteName ?? ""}," +
                    $"{employeeDto.DepartmentName ?? ""}," +
                    $"{employeeDto.DepartmentCode ?? ""}," +
                    $"{employeeDto.PositionName}," +
                    $"{employeeDto.IsActive}," +
                    $"{employeeDto.CreatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""}," +
                    $"{employeeDto.CreatedBy}," +
                    $"{employeeDto.UpdatedAt?.ToString("yyyy-MM-dd HH:mm:ss") ?? ""}," +
                    $"{employeeDto.UpdatedBy}");
            }

            return Encoding.UTF8.GetBytes(csvContent.ToString());
        }
    }
}
