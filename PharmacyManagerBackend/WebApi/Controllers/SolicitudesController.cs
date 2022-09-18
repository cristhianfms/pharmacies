using Domain;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Models;
using WebApi.Utils;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitudesController : ControllerBase
    {
        private ISolicitudeLogic _solicitudeLogic;
        public SolicitudesController(ISolicitudeLogic solicitudeLogic)
        {
            this._solicitudeLogic = solicitudeLogic;
        }

        [HttpPost]
        public IActionResult Create([FromBody] SolicitudeRequestModel solicitudeRequestModel)
        {
            Solicitude solicitude = ModelsMapper.ToEntity(solicitudeRequestModel);
            Solicitude solicitudeCreated = _solicitudeLogic.Create(solicitude);
            SolicitudeResponseModel solicitudeResponseModel = ModelsMapper.ToModel(solicitudeCreated);

            return Ok(solicitudeResponseModel);
        }
    }
}
