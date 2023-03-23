using MediatR;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;

namespace UniversityManagementSystemPortal.Application.Qurey.Institute
{
    public class GetAllInstitutesQuery : IRequest<IEnumerable<InstituteDto>>
    {
    }
}
