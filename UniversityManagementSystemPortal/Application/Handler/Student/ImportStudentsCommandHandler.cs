using LINQtoCSV;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.Student;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.ModelDto.Student;

namespace UniversityManagementSystemPortal
{
    public class ImportStudentsCommandHandler : IRequestHandler<ImportStudentsCommand, List<string>>
    {
        private readonly IUserInterface _userInterface;
        private readonly IStudentRepository _studentRepository;
        private readonly IStudentProgramRepository _studentProgramRepository;
        private readonly IProgramRepository _programRepository;
        private readonly ILogger<ImportStudentsCommandHandler> _logger;
        private readonly IIdentityServices _identityServices;

        public ImportStudentsCommandHandler(
            IUserInterface userInterface,
            IStudentRepository studentRepository,
            IStudentProgramRepository studentProgramRepository,
            IProgramRepository programRepository,
            ILogger<ImportStudentsCommandHandler> logger,
            IIdentityServices identityServices)
        {
            _userInterface = userInterface;
            _studentRepository = studentRepository;
            _studentProgramRepository = studentProgramRepository;
            _programRepository = programRepository;
            _logger = logger;
            _identityServices = identityServices;
        }

        public async Task<List<string>> Handle(ImportStudentsCommand request, CancellationToken cancellationToken)
        {
            var _skippedEntries = await _studentRepository.Upload(request.StudentsData);
            return _skippedEntries;
        }


    }
}
