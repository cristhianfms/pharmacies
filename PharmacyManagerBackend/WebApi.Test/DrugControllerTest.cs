using IBusinessLogic;
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
    private Mock<IPharmacyLogic> _pharmacyLogicMock;
    private DrugController _drugApiController;
    private Drug _drug;

    [TestInitialize]
    public void InitTest()
    {
        _drugLogicMock = new Mock<IDrugLogic>(MockBehavior.Strict);
        _pharmacyLogicMock = new Mock <IPharmacyLogic>(MockBehavior.Strict);
        _drugApiController = new DrugController(_drugLogicMock.Object, _pharmacyLogicMock.Object);
        _drug = new Drug()
        {
            Id = 1,
            DrugCode = "2A5",
            Price = 150,
            NeedsPrescription = false,
            Stock = 20,
            DrugInfo = new DrugInfo()
        };
    }


    [TestMethod]
    public void CreateDrugOk()
    {
        const int pharmacyId = 3;
      //  _pharmacyLogicMock.Setup(p => p.ExistsDrug(_drug.DrugCode, pharmacyId)).Returns(false);
        _drugLogicMock.Setup(m => m.Create(It.IsAny<Drug>())).Returns(_drug);
        
        var drugModel = new DrugModel()
        {
            Id = 1,
            DrugCode = "2A5",
            Price = 150,
            NeedsPrescription = false,
            Stock = 20,
            PharmacyId = pharmacyId
           
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
        _drugLogicMock.Setup(m => m.Delete(_drug.Id)).Verifiable();

        var result = _drugApiController.Delete(_drug.Id);

        Assert.IsTrue(result is OkObjectResult);

    }

}