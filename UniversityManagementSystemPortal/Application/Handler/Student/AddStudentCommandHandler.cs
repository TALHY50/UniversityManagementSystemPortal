using AutoMapper;
using DocumentFormat.OpenXml.Spreadsheet;
using MediatR;
using NuGet.Protocol.Core.Types;
using UniversityManagementSystemPortal.Application.Command.Student;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.Student;
using UniversityManagementSystemPortal.PictureManager;
using UniversityManagementSystemPortal.Repository;

namespace UniversityManagementSystemPortal
{
    public class AddStudentCommandHandler : IRequestHandler<AddStudentCommand, AddStudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        private readonly IMapper _mapper;
        private readonly IPictureManager _pictureManager;
        private readonly ILogger<AddStudentCommandHandler> _logger;
        private readonly IIdentityServices _identityService;
        private readonly IInstituteAdminRepository _repository;

        public AddStudentCommandHandler(IStudentRepository studentRepository,
            IUserRoleRepository userRoleRepository,
            IMapper mapper,
            IPictureManager pictureManager,
            ILogger<AddStudentCommandHandler> logger,
            IIdentityServices identityService,
            IInstituteAdminRepository repository)
        {
            _studentRepository = studentRepository;
            _userRoleRepository = userRoleRepository;

            _mapper = mapper;
            _pictureManager = pictureManager;
            _logger = logger;
            _identityService = identityService;
        }
        public async Task<AddStudentDto> Handle(AddStudentCommand request, CancellationToken cancellationToken)
        {
            var activeUserId = _identityService.GetUserId();


            if (request == null)
            {
                _logger.LogError("The student data is empty.");
                return null;
            }

            var existingStudent = await _studentRepository.GetByAdmissionNo(request.addStudentDto.AdmissionNo);
            if (existingStudent != null)
            {
                _logger.LogError($"A student with the same admission number ({request.addStudentDto.AdmissionNo}) already exists.");
                return null;
            }
            var student = _mapper.Map<Student>(request);
            student.CreatedBy = activeUserId;
            student.UpdatedBy = activeUserId;
            if (!string.IsNullOrEmpty(request.addStudentDto.AdmissionNo))
            {
                student.AdmissionNo = request.addStudentDto.AdmissionNo.Substring(0, 2) + "-" + request.addStudentDto.AdmissionNo.Substring(2);
            }

            if (request.addStudentDto.Picture != null)
            {
                student.ProfilePath = await _pictureManager.Upload(request.addStudentDto.Picture);
            }
            var addedStudent = await _studentRepository.Add(student);
            var studentRole = await _userRoleRepository.GetByRoleNameAsync("Student");
            if (studentRole != null)
            {
                var userRole = new UserRole
                {
                    UserId = addedStudent.UserId,
                    RoleId = studentRole.Id
                };
                await _userRoleRepository.AddAsync(userRole);
            }
            var addedStudentDto = _mapper.Map<AddStudentDto>(addedStudent);
            return addedStudentDto;
        }

    }

}
