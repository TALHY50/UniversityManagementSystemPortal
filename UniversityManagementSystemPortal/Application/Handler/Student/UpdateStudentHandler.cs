using AutoMapper;
using MediatR;
using Serilog;
using UniversityManagementSystemPortal.Application.Command.Student;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.Student;
using UniversityManagementSystemPortal.PictureManager;

namespace UniversityManagementSystemPortal.Application.Handler.Student
{
    public class UpdateStudentHandler : IRequestHandler<UpdateStudentCommand, UpdateStudentDto>
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IPictureManager _pictureManager;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateStudentHandler> _logger;
        private readonly IIdentityServices _identityServices;

        public UpdateStudentHandler(IStudentRepository studentRepository, IPictureManager pictureManager, IMapper mapper, ILogger<UpdateStudentHandler> logger, IIdentityServices identityServices)
        {
            _studentRepository = studentRepository;
            _pictureManager = pictureManager;
            _mapper = mapper;
            _logger = logger;
            _identityServices = identityServices;
        }
        public async Task<UpdateStudentDto> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
        {
            var existingStudent = await _studentRepository.GetById(request.Id);
            if (existingStudent == null)
            {
                // Log the error
                Log.Error("Student with Id {Id} not found.", request.Id);
                return null;
            }
            _mapper.Map(request, existingStudent);
            if (request.Picture != null)
            {
                existingStudent.ProfilePath = await _pictureManager.Update(existingStudent.Id, request.Picture, existingStudent.ProfilePath);
            }
            existingStudent.CreatedBy = _identityServices.GetUserId();
            await _studentRepository.Update(existingStudent);
            return _mapper.Map<UpdateStudentDto>(existingStudent);
        }
    }

}
