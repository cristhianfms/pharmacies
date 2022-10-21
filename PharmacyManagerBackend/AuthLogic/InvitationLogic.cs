using IBusinessLogic;
using Domain;
using IDataAccess;
using Exceptions;
using Domain.Dtos;
using IAuthLogic;
using Domain.Dto;

namespace AuthLogic
{

    public class InvitationLogic : IInvitationLogic
    {
        private IInvitationRepository _invitationRepository;
        private IUserLogic _userLogic;
        private IRoleLogic _roleLogic;
        private IPharmacyLogic _pharmacyLogic;
        private Context _currentContext;
        
        public InvitationLogic(
            IInvitationRepository invitationRepository, 
            IUserLogic userLogic, 
            IRoleLogic roleLogic, 
            IPharmacyLogic pharmacyLogic,
            Context currentContext)
        {
            this._invitationRepository = invitationRepository;
            this._userLogic = userLogic;
            this._pharmacyLogic = pharmacyLogic;
            this._roleLogic = roleLogic;
            this._currentContext = currentContext;
        }

        public virtual Invitation Create(InvitationDto invitationDto)
        {
            Invitation createdInvitation;
            Invitation? existentInvitation = getInvitationForUser(invitationDto.UserName);
            if (existentInvitation == null)
            {
                checkIfUserNameIsRepeated(invitationDto.UserName);
                Role role = getRoleForInvitation(invitationDto);
                Invitation invitationToCreate = new Invitation()
                {
                    UserName = invitationDto.UserName,
                    Role = role,
                    Used = false
                };
                Pharmacy? invitationPharmacy = getFarmacyForInvitation(invitationDto);
                invitationToCreate.Pharmacy = invitationPharmacy;

                string codeGenerated = generateNewInvitationCode();
                invitationToCreate.Code = codeGenerated;

                createdInvitation = _invitationRepository.Create(invitationToCreate);
            }
            else
            {
                createdInvitation = existentInvitation;
            }

            return createdInvitation;
        }

        public InvitationDto Update(string invitationCode, InvitationDto invitationDto)
        {
            Invitation invitation = getCreatedInvitation(invitationCode);
            CheckUsedInvitation(invitation);
                
            InvitationDto invitationDtoToReturn = new InvitationDto(){};
            
            if (_currentContext.CurrentUser == null)
            {
                checkInvitationUserNameMatches(invitation, invitationDto.UserName);
                checkIfUserEmailIsRepeated(invitationDto.Email);
                User userToCreate = new User()
                {
                    UserName = invitation.UserName,
                    Role = invitation.Role,
                    Email = invitationDto.Email,
                    Address = invitationDto.Address,
                    Password = invitationDto.Password,
                    RegistrationDate = DateTime.Now,
                    Pharmacy = invitation.Pharmacy
                };
                User createdUser = _userLogic.Create(userToCreate);
                invitation.Used = true;
                _invitationRepository.Update(invitation);

                invitationDtoToReturn.UserName = userToCreate.UserName;
                invitationDtoToReturn.RoleName = invitation.Role.Name;
                invitationDtoToReturn.PharmacyName = invitation.Pharmacy?.Name;
                invitationDtoToReturn.Email = createdUser.Email;
                invitationDtoToReturn.Address = createdUser.Address;
                invitationDtoToReturn.Used = true;
            } 
            else if (_currentContext.CurrentUser.Role.Name.Equals(Role.ADMIN))
            {
                checkIfUserNameIsRepeated(invitationDto.UserName, invitation.Id);
                Role invitationRole = getRoleForInvitation(invitationDto);
                Pharmacy? invitationPharmacy = getFarmacyForInvitation(invitationDto);
                invitation.Code = generateNewInvitationCode();
                invitation.UserName = invitationDto.UserName;
                invitation.Role = invitationRole;
                invitation.Pharmacy = invitationPharmacy;

                _invitationRepository.Update(invitation);
                
                invitationDtoToReturn.UserName = invitation.UserName;
                invitationDtoToReturn.RoleName = invitation.Role.Name;
                invitationDtoToReturn.PharmacyName = invitation.Pharmacy?.Name;
                invitationDtoToReturn.Code = invitation.Code;
                invitationDtoToReturn.Used = invitation.Used;
            }

            return invitationDtoToReturn;
        }

        public IEnumerable<Invitation> GetInvitations(QueryInvitationDto queryInvitationDto)
        {
            IEnumerable<Invitation> invitationsToReturn = new List<Invitation>();
            invitationsToReturn = _invitationRepository.GetAll();

            if (queryInvitationDto.PharmacyName != null)
            {
                Pharmacy? pharmacy = null;
                pharmacy = getExistantPharmacy(queryInvitationDto.PharmacyName);

                invitationsToReturn = invitationsToReturn.Where(i => i.Pharmacy.Name.ToLower() == queryInvitationDto.PharmacyName.ToLower());
            }
            if (queryInvitationDto.UserName != null)
            {
                Invitation invitationByUser = getInvitationForUser(queryInvitationDto.UserName);
                if (invitationByUser == null)
                {
                    throw new ValidationException("user doesn't exist");
                } 
                invitationsToReturn = invitationsToReturn.Where(i => i.UserName == queryInvitationDto.UserName);
            }
            if (queryInvitationDto.Role != null)
            {
                Role? role = null;
                role = getExistantRole(queryInvitationDto.Role);

                invitationsToReturn = invitationsToReturn.Where(i => i.Role.Name == queryInvitationDto.Role);
            }
            return invitationsToReturn;
        }

        private Pharmacy getExistantPharmacy(string pharmacyName)
        {
            Pharmacy pharmacy;
            try
            {
                pharmacy = _pharmacyLogic.GetPharmacyByName(pharmacyName);
            }
            catch (ResourceNotFoundException e)
            {
                throw new ValidationException("pharmacy doesn't exist");
            }

            return pharmacy;
        }

        private Role getExistantRole(string roleName)
        {
            Role role;
            try
            {
                role = _roleLogic.GetRoleByName(roleName);
            }
            catch (ResourceNotFoundException e)
            {
                throw new ValidationException("role doesn't exist");
            }
            return role;
        }

        public virtual Invitation GetInvitationByCode(string invitationCode)
        {
            return _invitationRepository.GetFirst(i => i.Code == invitationCode);
        }

        private void checkIfUserNameIsRepeated(string userName)
        {
            bool userExist = true;
            try
            {
                _userLogic.GetFirst(u => u.UserName == userName);
               _invitationRepository.GetFirst(i => i.UserName == userName);
            }
            catch (ResourceNotFoundException e)
            {
                userExist = false;
            }

            if (userExist)
            {
                throw new ValidationException("username already exists");
            }
        }
        
        private void checkIfUserNameIsRepeated(string userName, int invitationIdExcluded)
        {
            bool userExist = true;
            try
            {
                _userLogic.GetFirst(u => u.UserName == userName);
                _invitationRepository.GetFirst(i => i.UserName == userName && i.Id != invitationIdExcluded);
            }
            catch (ResourceNotFoundException e)
            {
                userExist = false;
            }

            if (userExist)
            {
                throw new ValidationException("username already exists");
            }
        }

        private string generateNewInvitationCode()
        {
            Random generator = new Random();
            String code;
            do
            {
                code = generator.Next(0, 1000000).ToString("D6");
            } while (isExistantCode(code));

            return code;
        }

        private bool isExistantCode(string code)
        {
            bool invitationExists = true;
            try
            {
                _invitationRepository.GetFirst(i => i.Code == code);
            }
            catch (ResourceNotFoundException e)
            {
                invitationExists = false;
            }

            return invitationExists;
        }

        private Invitation getCreatedInvitation(string invitationCode)
        {
            Invitation invitation;
            try
            {
                invitation = _invitationRepository.GetFirst(i => i.Code == invitationCode);
            }
            catch (ResourceNotFoundException e)
            {
                throw new ValidationException("invalid invitation code");
            }

            return invitation;
        }

        private void checkInvitationUserNameMatches(Invitation invitation, string userName)
        {
            if (invitation.UserName != userName)
            {
                throw new ValidationException("invalid invitation code");
            }
        }
        
        private Invitation getInvitationForUser(string userName)
        {
            Invitation invitation;
            try
            {
                invitation = _invitationRepository.GetFirst(i => i.UserName == userName);
            }
            catch (ResourceNotFoundException e)
            {
                invitation = null;
            }

            return invitation;
        }
        
        private void checkIfUserEmailIsRepeated(string email)
        {
            bool userExist = true;
            try
            {
                User user = _userLogic.GetFirst(u => u.Email == email);
            }
            catch (ResourceNotFoundException e)
            {
                userExist = false;
            }

            if (userExist)
            {
                throw new ValidationException("email already registered");
            }
        }
        
        private Pharmacy? getFarmacyForInvitation(InvitationDto invitationDto)
        {
            Pharmacy? pharmacy = null;
            User currentUser = _currentContext.CurrentUser;
            if (currentUser.Role.Name.Equals(Role.OWNER))
            {
                pharmacy = currentUser.Pharmacy;
            }
            else
            {
                if (!invitationDto.RoleName.Equals(Role.ADMIN))
                {
                    pharmacy = getExistantPharmacy(invitationDto.PharmacyName);
                }
            }
            
            return pharmacy;
        }
        
        private Role getRoleForInvitation(InvitationDto invitationDto)
        {
            Role role;
            User currentUser = _currentContext.CurrentUser;
            if (currentUser.Role.Name.Equals(Role.OWNER))
            {
                role = getExistantRole(Role.EMPLOYEE);
            }
            else
            {
                role = getExistantRole(invitationDto.RoleName);
            }

            return role;
        }
        
        private void CheckUsedInvitation(Invitation invitation)
        {
            if (invitation.Used)
            {
                throw new ValidationException("Invitation already used");
            }
        }
    }
}