using Domain;
using Domain.Dto;
using IAuthLogic;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Filter;
using WebApi.Models;
using WebApi.Models.Utils;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class InvitationsController : ControllerBase
{
    private IInvitationLogic _invitationLogic;

    public InvitationsController(IInvitationLogic invitationLogic)
    {
        this._invitationLogic = invitationLogic;
    }

    [HttpPost]
    [ServiceFilter(typeof(AuthorizationAttributeFilter))]
    public IActionResult Create([FromBody] InvitationRequestModel invitationModel)
    {
        InvitationDto invitationToCreate = InvitationModelsMapper.ToEntity(invitationModel);
        Invitation invitationCreated = _invitationLogic.Create(invitationToCreate);
        InvitationResponseModel invitationCreatedModel = InvitationModelsMapper.ToModel(invitationCreated);

        return Ok(invitationCreatedModel);
    }

    [HttpPut("{invitationCode}")]
    [ServiceFilter(typeof(AuthorizationAttributePublicFilter))]
    public IActionResult Update(string invitationCode, [FromBody] InvitationPutModel invitationPutModel)
    {
        InvitationDto invitationToUpdate = InvitationModelsMapper.ToEntity(invitationPutModel);
        InvitationDto invitationCreated = _invitationLogic.Update(invitationCode, invitationToUpdate);
        InvitationConfirmedModel invitationCreatedModel = InvitationModelsMapper.ToModel(invitationCreated);

        return Ok(invitationCreatedModel);
    }
    
    [HttpGet]
    [ServiceFilter(typeof(AuthorizationAttributeFilter))]
    public IActionResult GetInvitations([FromQuery] QueryInvitationDto queryInvitationDto)
    {
        List<Invitation> invitations = _invitationLogic.GetInvitations(queryInvitationDto).ToList();
        IEnumerable<InvitationResponseModel> invitationModels = InvitationModelsMapper.ToModelList(invitations);

        return Ok(invitationModels);
    }
}
