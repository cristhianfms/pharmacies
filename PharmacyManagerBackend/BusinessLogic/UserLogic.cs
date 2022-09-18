using System;
using Domain;
using Exceptions;
using IBusinessLogic;
using IDataAccess;

namespace BusinessLogic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly InvitationLogic _invitationLogic;

        public UserLogic() { }

        public UserLogic(IUserRepository userRepository, InvitationLogic invitationLogic)
        {
            this._userRepository = userRepository;
            this._invitationLogic = invitationLogic;
        }

        public virtual User Create(UserDto userDto)
        {
            Invitation invitation = getCreatedInvitation(userDto.InvitationCode);

            checkInvitationUserName(invitation, userDto.UserName);

            User userToCreate = new User()
            {
                UserName = invitation.UserName,
                Role = invitation.Role,
                Email = userDto.Email,
                Address = userDto.Password,
                Password = userDto.Password,
                RegistrationDate = DateTime.Now
            };

            User createdUser = _userRepository.Create(userToCreate);
            _invitationLogic.Delete(invitation.Id);
            return createdUser;
        }

        private void checkInvitationUserName(Invitation invitation, string userName)
        {
            if (invitation.UserName != userName)
            {
                throw new ValidationException("invalid invitation code");
            }
        }

        public virtual User GetUserByUserName(string userName)
        {
            return null;
        }

        private Invitation getCreatedInvitation(string invitationCode)
        {
            Invitation invitation;
            try
            {
                invitation = _invitationLogic.GetInvitationByCode(invitationCode);
            }
            catch (ResourceNotFoundException e)
            {
                throw new ValidationException("invalid invitation code");
            }

            return invitation;
        }
    }
}
