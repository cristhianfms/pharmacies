using Domain;
using Domain.Dtos;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Controllers
{
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
            InvitationDto invitationToCreate = ModelsMapper.ToEntity(invitationModel);
            Invitation invitationCreated = _invitationLogic.Create(invitationToCreate);
            InvitationModel invitationCreatedModel = ModelsMapper.ToModel(invitationCreated);

            return Ok(invitationCreatedModel);
        }
    }
}