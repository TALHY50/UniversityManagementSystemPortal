using MediatR;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;
using UniversityManagementSystemPortal.Models.ModelDto.Institute;

namespace UniversityManagementSystemPortal.Application.Qurey.Institute
{
    public class GetAllInstitutesQuery : IRequest<List<InstituteDto>>
    {
    }
}
