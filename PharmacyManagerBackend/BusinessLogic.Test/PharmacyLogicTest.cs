using System;
using System.Collections.Generic;
using Domain;
using Domain.Dtos;
using Exceptions;
using IDataAccess;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace BusinessLogic.Test;

[TestClass]
public class PharmacyLogicTest
{
    private PharmacyLogic _pharmacyLogic;
    private Mock<IPharmacyRepository> _pharmacyRepository;

    [TestInitialize]
    public void Initialize()
    {
        this._pharmacyRepository = new Mock<IPharmacyRepository>(MockBehavior.Strict);
        this._pharmacyLogic = new PharmacyLogic(this._pharmacyRepository.Object);
    }

    [TestMethod]
    public void CreateNewPharmacyOk()
    {
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Id = 1,
            Name = "Pharmashop",
            Address = "Calle A 1234"
        };
        Pharmacy pharmacyToCreate = new Pharmacy()
        {
            Name = "Pharmashop",
            Address = "Calle A 1234"
        };
        _pharmacyRepository.Setup(m => m.Create(It.IsAny<Pharmacy>())).Returns(pharmacyRepository);

        Pharmacy pharmacyCreated = _pharmacyLogic.Create(pharmacyToCreate);

        Assert.AreEqual(pharmacyRepository, pharmacyCreated);
        _pharmacyRepository.VerifyAll();
    }

    [TestMethod]
    public void GetPharmacyByrNameOk()
    {
        DateTime registrationDate = DateTime.Now;
        Pharmacy pharmacyRepository = new Pharmacy()
        {
            Id = 1,
            Name = "Pharmacy",
            Address = "Address"
        };
        _pharmacyRepository.Setup(m => m.GetFirst(It.IsAny<Func<Pharmacy, bool>>())).Returns(pharmacyRepository);

        Pharmacy pharmacyReturned = _pharmacyLogic.GetPharmacyByName("Pharmacy");

        Assert.AreEqual(pharmacyRepository, pharmacyReturned);
        _pharmacyRepository.VerifyAll();
    }
}

