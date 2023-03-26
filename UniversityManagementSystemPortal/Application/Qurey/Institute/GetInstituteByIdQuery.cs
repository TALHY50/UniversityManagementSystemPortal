using MediatR;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;
using UniversityManagementSystemPortal.Models.ModelDto.Institute;

namespace UniversityManagementSystemPortal.Application.Qurey.Institute
{
    public class GetInstituteByIdQuery : IRequest<InstituteDto>
    {
        public Guid Id { get; set; }
    }
}
