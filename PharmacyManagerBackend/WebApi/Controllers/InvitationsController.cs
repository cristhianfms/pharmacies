using Domain;
using Domain.Dtos;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Controllers;
[ApiController]
[Route("api/[controller]")]
public class InvitationController : ControllerBase
{
    private IInvitationLogic _invitationLogic;

    public InvitationController(IInvitationLogic invitationLogic)
    {
        this._invitationLogic = invitationLogic;
    }

    [HttpPost]
    public IActionResult Create([FromBody] InvitationModel invitationModel)
    {
        InvitationDto invitationToCreate = InvitationModelsMapper.ToEntity(invitationModel);
        Invitation invitationCreated = _invitationLogic.Create(invitationToCreate);
        InvitationModel invitationCreatedModel = InvitationModelsMapper.ToModel(invitationCreated);

        return Ok(invitationCreatedModel);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int invitationId, [FromBody] InvitationPutModel invitationPutModel)
    {
        InvitationDto invitationToUpdate = InvitationModelsMapper.ToEntity(invitationPutModel);
        InvitationDto invitationCreated = _invitationLogic.Update(invitationId, invitationToUpdate);
        InvitationConfirmedModel invitationCreatedModel = InvitationModelsMapper.ToModel(invitationCreated);

        return Ok(invitationCreatedModel);
    }
}
