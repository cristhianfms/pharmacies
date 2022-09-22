using Domain;
using Domain.Dtos;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Controllers;

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


    [HttpGet]
    public IActionResult GetSolicitudes([FromQuery] QuerySolicitudeDto querySolicitudeDto)
    {
        List<Solicitude> solicitudes = _solicitudeLogic.GetSolicitudes(querySolicitudeDto).ToList();
        List<SolicitudeResponseModel> solicitudeModels = ModelsMapper.ToModelList(solicitudes);

        return Ok(solicitudeModels);
    }

    [HttpPut]
    public IActionResult Update(int id, SolicitudeResponseModel solicitudeResponseModel)
    {
        Solicitude solicitudeToUpdate = ModelsMapper.ToEntity(solicitudeResponseModel);
        Solicitude solicitudeUpdated = _solicitudeLogic.Update(id, solicitudeToUpdate);
        SolicitudeResponseModel solicitudeUpdatedModel = ModelsMapper.ToModel(solicitudeUpdated);

        return Ok(solicitudeUpdatedModel);
    }
}

