using AutoMapper;
using MediatR;
using System.Text;
using UniversityManagementSystemPortal.Application.Qurey.Student;
using UniversityManagementSystemPortal.Interfaces;

namespace UniversityManagementSystemPortal.Application.Handler.Student
{
    public class ExportStudentListQueryHandler : IRequestHandler<ExportStudentListQuery, byte[]>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;

        public ExportStudentListQueryHandler(IStudentRepository studentRepository, IMapper mapper)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
        }

        public async Task<byte[]> Handle(ExportStudentListQuery request, CancellationToken cancellationToken)
        {
            var students = await _studentRepository.Get();
            var studentDtos = _mapper.Map<List<StudentDto>>(students);

            var csvContent = new StringBuilder();
            csvContent.AppendLine("Admission No,RoleNo,SectionName,ProgramName, First Name, Middle Name, Last Name,MobileNo, Gender, Date of Birth,BloodGroup, Category,Email,EmailConfirm,UserName,IsActive,Address");

            foreach (var studentDto in studentDtos)
            {
                csvContent.AppendLine(
                    $"{studentDto.AdmissionNo}," +
                    $"{studentDto.RoleNo}," +
                    $"{studentDto.SectionName}," +
                    $"{studentDto.ProgramName}," +
                    $" {studentDto.FirstName}," +
                    $" {studentDto.MiddleName}, " +
                    $"{studentDto.LastName}, " +
                    $"{studentDto.MobileNo}, " +
                    $"{studentDto.Gender}," +
                    $" {studentDto.DateOfBirth}, " +
                    $" {studentDto.BloodGroup}, " +
                    $"{studentDto.Category}, " +
                    $"{studentDto.Email}, " +
                    $"{studentDto.EmailConfirm}, " +
                    $"{studentDto.UserName}, " +
                    $"{studentDto.IsActive}, " +
                    $"{studentDto.Address}");
            }

            return Encoding.UTF8.GetBytes(csvContent.ToString());
        }
    }
}
