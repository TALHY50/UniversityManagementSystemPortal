using MediatR;
using UniversityManagementSystemPortal.Models.ModelDto.Department;

namespace UniversityManagementSystemPortal.Application.Qurey.Department
{
    public record GetLookupList(LookupDto LookupDto): IRequest<List<LookupDto>>;
  
}
