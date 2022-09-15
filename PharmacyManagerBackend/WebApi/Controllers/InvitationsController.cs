using BusinessLogic;
using Domain;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvitationController : ControllerBase
    {
        private InvitationLogic _invitationLogic;

        public InvitationController([FromBody] InvitationLogic invitationLogic)
        {
            this._invitationLogic = invitationLogic;
        }

        [HttpPost]
        public IActionResult Create(InvitationModel invitationModel)
        {
            Invitation invitation = ModelsMapper.ToEntity(invitationModel);
            Invitation invitationCreated = _invitationLogic.Create(invitation);
            InvitationModel invitationCreatedModel = ModelsMapper.ToModel(invitationCreated);

            return Ok(invitationCreatedModel);
        }
    }
}