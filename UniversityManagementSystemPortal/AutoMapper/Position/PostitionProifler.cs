using AutoMapper;
using UniversityManagementSystemPortal.ModelDto.Employee;
using UniversityManagementSystemPortal.ModelDto.Position;
using UniversityManagementSystemPortal.ModelDto.Program;


namespace UniversityManagementSystemPortal
{
    public class PostitionProifler : Profile
    {
        public PostitionProifler()
        {
            CreateMap<Position, PositionDto>();
            CreateMap<List<PositionDto>, IEnumerable<PositionDto>>();
            CreateMap<PositionAddorUpdateDto, Position>();
        }
    }
}
