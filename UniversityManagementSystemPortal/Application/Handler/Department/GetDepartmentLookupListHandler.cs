using MediatR;
using UniversityManagementSystemPortal.Application.Qurey.Department;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.Department;

namespace UniversityManagementSystemPortal.Application.Handler.Department
{
    public class GetDepartmentLookupListHandler : IRequestHandler<GetLookupList, List<LookupDto>>
    {
        private readonly IDepartmentRepository _departmentRepository;

        public GetDepartmentLookupListHandler(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<List<LookupDto>> Handle(GetLookupList request, CancellationToken cancellationToken)
        {
            var departments =  _departmentRepository.GetAllDepartmentsAsync();

            if (departments == null || !departments.Any())
            {
                return null;
            }

            var departmentLookupList = departments
                .Select(d => new LookupDto
                {
                    Id = d.Id,
                    Name = d.Name
                })
                .ToList();

            return departmentLookupList;
        }
    }
}
