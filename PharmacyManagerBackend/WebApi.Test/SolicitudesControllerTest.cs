using Domain;
using Domain.Dtos;
using IBusinessLogic;
using Microsoft.AspNetCore.Mvc;
using Moq;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Test;

[TestClass]
public class SolicitudesControllerTest
{
    private Mock<ISolicitudeLogic> _solicitudeLogicMock;
    private SolicitudesController _solicitudeApiController;
    private User _userForTest;
    private Solicitude _solicitudeForTest;


    [TestInitialize]
    public void InitTest()
    {
        _solicitudeLogicMock = new Mock<ISolicitudeLogic>(MockBehavior.Strict);
        _solicitudeApiController = new SolicitudesController(_solicitudeLogicMock.Object);

        _userForTest = new User()
        {
            Id = 1,
            UserName = "Usuario1",
            Email = "ususario@user.com",
            Address = "Cuareim 123",
            Password = "Usuario+1",
            Role = new Role()
            {
                Name = Role.EMPLOYEE
            },
            Pharmacy = new Pharmacy()
            {
            Name = "Pharmashop"
            }
        };
        SolicitudeItem solicitudeItem3 = new SolicitudeItem()
        {
            DrugQuantity = 20,
            DrugCode = "XF324",
        };
        SolicitudeItem solicitudeItem4 = new SolicitudeItem()
        {
            DrugQuantity = 3,
            DrugCode = "AG123",
        };

        List<SolicitudeItem> solicitudeItems3and4 = new List<SolicitudeItem>()
            {
                solicitudeItem3, solicitudeItem4,
            };

        _solicitudeForTest = new Solicitude()
        {
            Id = 2,
            State = State.PENDING,
            Date = DateTime.Now,
            Employee = _userForTest,
            PharmacyId = _userForTest.Pharmacy.Id,
            Items = solicitudeItems3and4
        };
    }


    [TestMethod]
    public void CreateSolicitudeOk()
    {

        SolicitudeItem solicitudeItem = new SolicitudeItem()
        {
            DrugQuantity = 20,
            DrugCode = "XF324",
        };

        List<SolicitudeItem> solicitudeItems = new List<SolicitudeItem>()
            {
                solicitudeItem,
            };

        Solicitude solicitude = new Solicitude()
        {
            Id = 1,
            State = State.PENDING,
            Date = DateTime.Now,
            Employee = _userForTest,
            PharmacyId = _userForTest.Pharmacy.Id,
            Items = solicitudeItems
        };

        SolicitudeItemModel solicitudeItemModel = new SolicitudeItemModel()
        {
            DrugCode = solicitudeItem.DrugCode,
            DrugQuantity = solicitudeItem.DrugQuantity
        };

        List<SolicitudeItemModel> solicitudeItemModels = new List<SolicitudeItemModel>()
            {
               solicitudeItemModel,
            };

        SolicitudeRequestModel solicitudeRequestModel = new SolicitudeRequestModel()
        {
            SolicitudeItems = solicitudeItemModels
        };

        _solicitudeLogicMock.Setup(m => m.Create(It.IsAny<Solicitude>())).Returns(solicitude);

 
        var result = _solicitudeApiController.Create(solicitudeRequestModel);
        var okResult = result as OkObjectResult;
        var createdSolicitude = okResult.Value as SolicitudeResponseModel;
        var solicitudeToModel = ModelsMapper.ToModel(solicitude);

        Assert.AreEqual(solicitude.Id, createdSolicitude.Id);
        Assert.AreEqual(solicitude.State, createdSolicitude.State);
        Assert.AreEqual(solicitude.Date, createdSolicitude.Date);
        Assert.AreEqual(solicitude.Employee.UserName, createdSolicitude.EmployeeUserName);
        CollectionAssert.AreEqual(solicitudeToModel.SolicitudeItems, createdSolicitude.SolicitudeItems);
        _solicitudeLogicMock.VerifyAll();
    }

    [TestMethod]
    public void GetAllSolicitudesOk()
    {
        SolicitudeItem solicitudeItem1 = new SolicitudeItem()
        {
            DrugQuantity = 20,
            DrugCode = "XF324",
        };
        SolicitudeItem solicitudeItem2 = new SolicitudeItem()
        {
            DrugQuantity = 3,
            DrugCode = "AG123",
        };

        List<SolicitudeItem> solicitudeItems1and2 = new List<SolicitudeItem>()
            {
                solicitudeItem1, solicitudeItem2,
            };

        Solicitude solicitude1ToReturn = new Solicitude()
        {
            Id = 1,
            State = State.PENDING,
            Date = DateTime.Now,
            Employee = _userForTest,
            PharmacyId = _userForTest.Pharmacy.Id,
            Items = solicitudeItems1and2

        };

        List<Solicitude> solicitudesToReturn = new List<Solicitude>()
            {
                solicitude1ToReturn, _solicitudeForTest
            };

        QuerySolicitudeDto querySolicitudeDto = new QuerySolicitudeDto()
        {

        };
        _solicitudeLogicMock.Setup(s => s.GetSolicitudes(querySolicitudeDto)).Returns(solicitudesToReturn);


        var result = _solicitudeApiController.GetSolicitudes(querySolicitudeDto);
        var okResult = result as OkObjectResult;
        var solicitudes = okResult.Value as List<SolicitudeResponseModel>;
        var solicitudesToReturnModels = ModelsMapper.ToModelList(solicitudesToReturn);


        Assert.AreEqual(solicitudesToReturnModels[0], solicitudes[0]);
        Assert.AreEqual(solicitudesToReturnModels[1], solicitudes[1]);
        Assert.AreEqual(solicitudesToReturnModels.Count, solicitudes.Count);
        CollectionAssert.AreEqual(solicitudesToReturnModels, solicitudes);
        _solicitudeLogicMock.VerifyAll();
    }

    [TestMethod]
    public void UpdateSolicitudeStateOk()
    {
        int solicitudeId = _solicitudeForTest.Id;
        SolicitudePutModel solicitudeModelToUpdate = new SolicitudePutModel()
        {
            State = "ACCEPTED"
        };
        Solicitude solicitudeToReturn = new Solicitude()
        {
            Id = solicitudeId,
            State = State.ACCEPTED,
            Date = _solicitudeForTest.Date,
            Items = _solicitudeForTest.Items,
            Employee = _solicitudeForTest.Employee,
        };
        SolicitudeResponseModel solicitudeModelUpdated = ModelsMapper.ToModel(solicitudeToReturn);

        _solicitudeLogicMock.Setup(s => s.Update(solicitudeId, It.IsAny<Solicitude>())).Returns(solicitudeToReturn);

        var result = _solicitudeApiController.Update(solicitudeId, solicitudeModelToUpdate);
        var okResult = result as OkObjectResult;
        var solicitudeUpdated = okResult.Value as SolicitudeResponseModel;


        Assert.AreEqual(solicitudeModelUpdated.Id, solicitudeUpdated.Id);
        Assert.AreEqual(solicitudeModelUpdated.State, solicitudeUpdated.State);
        Assert.AreEqual(solicitudeModelUpdated.Date, solicitudeUpdated.Date);
        Assert.AreEqual(solicitudeModelUpdated.EmployeeUserName, solicitudeUpdated.EmployeeUserName);
        Assert.AreEqual(solicitudeModelUpdated.SolicitudeItems[0].DrugCode, solicitudeUpdated.SolicitudeItems[0].DrugCode);
        Assert.AreEqual(solicitudeModelUpdated.SolicitudeItems[0].DrugQuantity, solicitudeUpdated.SolicitudeItems[0].DrugQuantity);
        CollectionAssert.AreEqual(solicitudeModelUpdated.SolicitudeItems, solicitudeUpdated.SolicitudeItems);

        _solicitudeLogicMock.VerifyAll();
    }
}

