using AutoMapper;
using MediatR;
using UniversityManagementSystemPortal.Application.Command.Account;
using UniversityManagementSystemPortal.Authorization;
using UniversityManagementSystemPortal.IdentityServices;
using UniversityManagementSystemPortal.Interfaces;
using UniversityManagementSystemPortal.Models.ModelDto.UserDto;

namespace UniversityManagementSystemPortal.Application.Handler.Account
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserViewModel>
    {
        private readonly IIdentityServices _identityServices;
        private readonly IUserInterface _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<UpdateUserHandler> _logger;

        public UpdateUserHandler(IIdentityServices identityServices, IUserInterface userRepository, IMapper mapper, ILogger<UpdateUserHandler> logger)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _identityServices = identityServices;
        }

        public async Task<UserViewModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                // Check if the email or username already exists in the database
                var existingUserByEmail = await _userRepository.GetByEmailAsync(request.UpdateUserDto.Email);
                if (existingUserByEmail != null && existingUserByEmail.Id != request.Id)
                {
                    throw new AppException($"Email '{request.UpdateUserDto.Email}' is already taken.");
                }

                var existingUserByUsername = await _userRepository.GetByUsernameAsync(request.UpdateUserDto.Username);
                if (existingUserByUsername != null && existingUserByUsername.Id != request.Id)
                {
                    throw new AppException($"Username '{request.UpdateUserDto.Username}' is already taken.");
                }

                // Get the user from the database
                var user = await _userRepository.GetByIdAsync(request.Id);

                if (user == null)
                {
                    throw new AppException($"User with ID '{request.Id}' not found.");
                }

                // Update the user's properties
                _mapper.Map(request.UpdateUserDto, user);


                // Save the changes to the database
                await _userRepository.UpdateAsync(user);

                _logger.LogInformation("User {UserId} updated successfully.", user.Id);

                return _mapper.Map<UserViewModel>(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating user {UserId}.", request.Id);
                throw;
            }
        }

    }
}

