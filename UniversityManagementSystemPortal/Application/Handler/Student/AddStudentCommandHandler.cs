using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.Student;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.Student;
using UniversityManagementSystemPortal.PictureManager;

namespace UniversityManagementSystemPortal
{
    public class AddStudentCommandHandler : IRequestHandler<AddStudentCommand, AddStudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMapper _mapper;
        private readonly IPictureManager _pictureManager;
        private readonly ILogger<AddStudentCommandHandler> _logger;
        private readonly IIdentityServices _identityService;

        public AddStudentCommandHandler(IStudentRepository studentRepository,
            IMapper mapper,
            IPictureManager pictureManager,
            ILogger<AddStudentCommandHandler> logger,
            IIdentityServices identityService)
        {
            _studentRepository = studentRepository;
            _mapper = mapper;
            _pictureManager = pictureManager;
            _logger = logger;
            _identityService = identityService;
        }

        public async Task<AddStudentDto> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
            {
                _logger.LogError("The student data is empty.");
                return null;
            }
            if (request.InstituteId == null)
            {
                _logger.LogError("The Institute ID is required.");
                return null;
            }
            var existingStudent = await _studentRepository.GetByAdmissionNo(request.AdmissionNo);
            if (existingStudent != null)
            {
                _logger.LogError($"A student with the same admission number ({request.AdmissionNo}) already exists.");
                return null;
            }
            var activeUserId = _identityService.GetUserId();
            var student = _mapper.Map<Student>(request);
            student.CreatedBy = activeUserId;
            student.UpdatedBy = activeUserId;
            if (!string.IsNullOrEmpty(request.AdmissionNo))
            {
                student.AdmissionNo = request.AdmissionNo.Substring(0, 2) + "-" + request.AdmissionNo.Substring(2);
            }

            if (request.Picture != null)
            {
                student.ProfilePath = await _pictureManager.Upload(request.Picture);
            }
            var addedStudent = await _studentRepository.Add(student);
            var addedStudentDto = _mapper.Map<AddStudentDto>(addedStudent);
            return addedStudentDto;
        }

    }

}
