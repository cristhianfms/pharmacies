using System;
using System.Collections.Generic;
using IBusinessLogic;
using Domain;
using IDataAccess;
using Exceptions;
using Domain.Dtos;

namespace BusinessLogic;

public class InvitationLogic : IInvitationLogic
{
    private IInvitationRepository _invitationRepository;
    private UserLogic _userLogic;
    private RoleLogic _roleLogic;
    private PharmacyLogic _pharmacyLogic;

    public InvitationLogic() { }

    public InvitationLogic(IInvitationRepository invitationRepository, UserLogic userLogic, RoleLogic roleLogic, PharmacyLogic pharmacyLogic)
    {
        this._invitationRepository = invitationRepository;
        this._userLogic = userLogic;
        this._pharmacyLogic = pharmacyLogic;
        this._roleLogic = roleLogic;
    }

    public virtual Invitation Create(InvitationDto invitationDto)
    {
        checkIfUserNameIsRepeated(invitationDto.UserName);
        Pharmacy pharmacy = getExistantPharmacy(invitationDto.PharmacyName);
        Role role = getExistantRole(invitationDto.RoleName);

        Invitation invitationToCreate = new Invitation()
        {
            UserName = invitationDto.UserName,
            Role = role,
            Pharmacy = pharmacy
        };

        string codeGenerated = generateNewInvitationCode();
        invitationToCreate.Code = codeGenerated;

        Invitation createdInvitation = _invitationRepository.Create(invitationToCreate);

        return createdInvitation;
    }

    public InvitationDto Update(int invitationId, InvitationDto invitationDto)
    {
        // TODO: implement!
        throw new NotImplementedException();
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

    public virtual void Delete(int id)
    {
        throw new NotImplementedException();
    }

    private void checkIfUserNameIsRepeated(string userName)
    {
        bool userExist = true;
        try
        {
            User user = _userLogic.GetUserByUserName(userName);
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
            _invitationRepository.GetInvitationByCode(code);
        }
        catch (ResourceNotFoundException e)
        {
            invitationExists = false;
        }

        return invitationExists;
    }
}

