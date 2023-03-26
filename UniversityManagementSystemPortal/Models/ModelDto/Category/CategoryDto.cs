
using UniversityManagementSystemPortal.ModelDto.InstituteDto;

namespace UniversityManagementSystemPortal
{
    public class CategoryDto
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public string? Prefix { get; set; }
        public bool IsActive { get; set; }
        public bool IsStaff { get; set; }
        public bool IsFaculty { get; set; }
        public InstituteDto? Institute { get; set; }
        //public ICollection<PositionDto> Positions { get; set; }
    }
}
