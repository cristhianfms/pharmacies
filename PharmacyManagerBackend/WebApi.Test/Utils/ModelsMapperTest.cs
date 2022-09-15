using System;
using System.Collections.Generic;
using Domain;
using Domain.Dtos;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WebApi.Models;
using WebApi.Utils;

namespace WebApi.Test.Utils
{
    [TestClass]
    public class ModelsMapperTest
    {

        [TestMethod]
        public void CredentialsDtoToCredentialsModelOk()
        {
            TokenDto token = new TokenDto
            {
                Token = Guid.NewGuid()
            };

            TokenModel tokenModel = ModelsMapper.ToModel(token);

            Assert.AreEqual(token.Token, tokenModel.Token);
        }

        [TestMethod]
        public void SessionRequestModelToSessionOK()
        {
            CredentialsModel credentialsModel = new CredentialsModel
            {
                UserName = "Cris",
                Password = "Cris.2022"
            };

            CredentialsDto credentialsDto = ModelsMapper.ToEntity(credentialsModel);

            Assert.AreEqual(credentialsModel.UserName, credentialsDto.UserName);
            Assert.AreEqual(credentialsModel.Password, credentialsDto.Password);
        }

        [TestMethod]
        public void PurchaseRequestModelToEntityOK()
        {
            List<PurchaseItemModel> purchaseItemModels = new List<PurchaseItemModel>(){
                new PurchaseItemModel(){
                    DrugCode = "A01",
                    Quantity = 1,
                    PharmacyName = "Pharamacy Name"
                }
            };
            PurchaseRequestModel purchaseRequestModel = new PurchaseRequestModel()
            {
                UserEmail = "email@email.com",
                Items = purchaseItemModels
            };

            PurchaseDto purchaseDto = ModelsMapper.ToEntity(purchaseRequestModel);

            Assert.AreEqual(purchaseRequestModel.UserEmail, purchaseDto.UserEmail);
            Assert.AreEqual(purchaseRequestModel.Items[0].DrugCode, purchaseDto.Items[0].DrugCode);
            Assert.AreEqual(purchaseRequestModel.Items[0].Quantity, purchaseDto.Items[0].Quantity);
            Assert.AreEqual(purchaseRequestModel.Items[0].PharmacyName, purchaseDto.Items[0].PharmacyName);
        }


        [TestMethod]
        public void PurchaseDtoToModelOK()
        {
            PurchaseItemDto purchaseItemDto = new PurchaseItemDto
            {
                DrugCode = "A01",
                Quantity = 1,
                PharmacyName = "Pharamacy Name"
            };
            List<PurchaseItemDto> purchaseItems = new List<PurchaseItemDto>() { purchaseItemDto };
            PurchaseDto purchaseDto = new PurchaseDto()
            {
                Id = 1,
                UserEmail = "email@email.com",
                CreatedDate = DateTime.Now,
                Items = purchaseItems
            };

            PurchaseResponseModel purchaseResponse = ModelsMapper.ToModel(purchaseDto);

            Assert.AreEqual(purchaseDto.Id, purchaseResponse.Id);
            Assert.AreEqual(purchaseDto.UserEmail, purchaseResponse.UserEmail);
            Assert.AreEqual(purchaseDto.CreatedDate, purchaseResponse.CreatedDate);
            Assert.AreEqual(purchaseDto.Items[0].DrugCode, purchaseResponse.Items[0].DrugCode);
            Assert.AreEqual(purchaseDto.Items[0].Quantity, purchaseResponse.Items[0].Quantity);
            Assert.AreEqual(purchaseDto.Items[0].PharmacyName, purchaseResponse.Items[0].PharmacyName);
        }
    }
}