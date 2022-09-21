using Domain;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Controllers;
using WebApi.Models;
using WebApi.Test.Utils;
using System.Collections.Generic;
using IBusinessLogic;

namespace WebApi.Test;

[TestClass]
public class DrugControllerTest
{
    private Mock<IDrugLogic> _drugLogicMock;
    private DrugController _drugApiController;
    private Drug _drug;

    [TestInitialize]
    public void InitTest()
    {
        _drugLogicMock = new Mock<IDrugLogic>(MockBehavior.Strict);
        _drugApiController = new DrugController(_drugLogicMock.Object);
        _drug = new Drug()
        {
            Id = 1,
            DrugCode = "2A5",
            Price = 150,
            NeedsPrescription = false,
            Stock = 20
        };
    }


    [TestMethod]
    public void CreateDrugOk()
    {
        _drugLogicMock.Setup(m => m.Create(It.IsAny<Drug>())).Returns(_drug);
        var drugModel = new DrugModel()
        {
            Id = 1,
            DrugCode = "2A5",
            Price = 150,
            NeedsPrescription = false,
            Stock = 20
        };

        var result = _drugApiController.Create(drugModel);
        var okResult = result as OkObjectResult;
        var createdDrug = okResult.Value as DrugModel;

        Assert.IsTrue(ModelsComparer.DrugCompare(drugModel, createdDrug));
        _drugLogicMock.VerifyAll();
    }

    [TestMethod]
    public void DeleteDrugOk()
    {
        _drugLogicMock.Setup(m => m.Delete(_drug)).Verifiable();

        var result = _drugApiController.Delete(_drug);

        Assert.IsTrue(result is OkObjectResult);

    }

    [TestMethod]
    public void GetAllDrugsOk()
    {
        _drugLogicMock.Setup(m => m.GetAllDrugs()).Returns(It.IsAny<IEnumerable<Drug>>);

        var result = _drugApiController.GetAll();

        Assert.IsTrue(result is OkObjectResult);

    }

    [TestMethod]
    public void GetDrugOk()
    {
        _drugLogicMock.Setup(m => m.GetDrug(_drug)).Returns(It.IsAny<Drug>);

        var result = _drugApiController.GetDrug(_drug);

        Assert.IsTrue(result is OkObjectResult);
    }

}

