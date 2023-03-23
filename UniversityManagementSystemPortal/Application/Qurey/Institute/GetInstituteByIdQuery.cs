using MediatR;
using UniversityManagementSystemPortal.ModelDto.InstituteDto;

namespace UniversityManagementSystemPortal.Application.Qurey.Institute
{
    public class GetInstituteByIdQuery : IRequest<InstituteDto>
    {
        public Guid Id { get; set; }
    }
}
