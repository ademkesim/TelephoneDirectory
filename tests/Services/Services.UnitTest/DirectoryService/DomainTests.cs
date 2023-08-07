using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DirectoryService.Api.Core.Enums;
using DirectoryService.Api.Core.Domain.Concrete.DomainObjects;
using System.Text.Json;
using DirectoryService.Api.Core.Domain.Concrete.RequestDTO;
using DirectoryService.Api.Core.Domain.Concrete.ResponseDTO;
using DirectoryService.Api.Core.Domain.Concrete;

namespace Services.UnitTest.DirectoryService
{
    [TestClass]
    public class DomainTests
    {

        [TestMethod]
        public void UserDetailObject_PropertiesAreSetCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var name = "Adem";
            var surname = "Kesim";
            var companyName = "Rise";
            var communications = new List<UserDetailCommunicationObject>();

            // Act
            var userDetailObject = new UserDetailObject
            {
                Id = id,
                Name = name,
                Surname = surname,
                CompanyName = companyName,
                Communications = communications
            };

            // Assert
            Assert.AreEqual(id, userDetailObject.Id);
            Assert.AreEqual(name, userDetailObject.Name);
            Assert.AreEqual(surname, userDetailObject.Surname);
            Assert.AreEqual(companyName, userDetailObject.CompanyName);
            Assert.AreEqual(communications, userDetailObject.Communications);
        }

        [TestMethod]
        public void UserDetailCommunicationObject_PropertiesAreSetCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();
            var communicationType = CommunicationTypeEnum.Email;
            var communicationInfo = "adem@gmail.com";

            // Act
            var communicationObject = new UserDetailCommunicationObject
            {
                Id = id,
                CommunicationType = communicationType,
                CommunicationInfo = communicationInfo
            };

            // Assert
            Assert.AreEqual(id, communicationObject.Id);
            Assert.AreEqual(communicationType, communicationObject.CommunicationType);
            Assert.AreEqual(communicationInfo, communicationObject.CommunicationInfo);
        }

        [TestMethod]
        public void UserDetailCommunicationObject_JsonSerialization()
        {
            // Arrange
            var communicationObject = new UserDetailCommunicationObject
            {
                Id = Guid.NewGuid(),
                CommunicationType = CommunicationTypeEnum.PhoneNumber,
                CommunicationInfo = "555 555 55 55"
            };

            // Act
            var jsonString = JsonSerializer.Serialize(communicationObject);
            var deserializedObject = JsonSerializer.Deserialize<UserDetailCommunicationObject>(jsonString);

            // Assert
            Assert.IsNotNull(deserializedObject);
            Assert.AreEqual(communicationObject.Id, deserializedObject.Id);
            Assert.AreEqual(communicationObject.CommunicationType, deserializedObject.CommunicationType);
            Assert.AreEqual(communicationObject.CommunicationInfo, deserializedObject.CommunicationInfo);
        }

        [TestMethod]
        public void AddUserCommunicationRequestDTO_PropertiesAreSetCorrectly()
        {
            // Arrange
            var communicationType = CommunicationTypeEnum.Email;
            var communicationInfo = "adem@example.com";
            var userInfoId = Guid.NewGuid();

            // Act
            var requestDTO = new AddUserCommunicationRequestDTO
            {
                CommunicationType = communicationType,
                CommunicationInfo = communicationInfo,
                UserInfoId = userInfoId
            };

            // Assert
            Assert.AreEqual(communicationType, requestDTO.CommunicationType);
            Assert.AreEqual(communicationInfo, requestDTO.CommunicationInfo);
            Assert.AreEqual(userInfoId, requestDTO.UserInfoId);
        }

        [TestMethod]
        public void AddUserRequestDTO_PropertiesAreSetCorrectly()
        {
            // Arrange
            var name = "Adem";
            var surname = "Kesim";
            var companyName = "Rise";

            // Act
            var requestDTO = new AddUserRequestDTO
            {
                Name = name,
                Surname = surname,
                CompanyName = companyName
            };

            // Assert
            Assert.AreEqual(name, requestDTO.Name);
            Assert.AreEqual(surname, requestDTO.Surname);
            Assert.AreEqual(companyName, requestDTO.CompanyName);
        }

        [TestMethod]
        public void AddUserResponseDTO_IdIsSetCorrectly()
        {
            // Arrange
            var id = Guid.NewGuid();

            // Act
            var responseDTO = new AddUserResponseDTO
            {
                Id = id
            };

            // Assert
            Assert.AreEqual(id, responseDTO.Id);
        }

        [TestMethod]
        public void UserDetailResponseDTO_UserDetailIsSetCorrectly()
        {
            // Arrange
            var userDetail = new UserDetailObject
            {
                Id = Guid.NewGuid(),
                Name = "Adem",
                Surname = "Kesim",
                CompanyName = "Rise",
                Communications = new List<UserDetailCommunicationObject>()
            };

            // Act
            var responseDTO = new UserDetailResponseDTO
            {
                UserDetail = userDetail
            };

            // Assert
            Assert.AreEqual(userDetail, responseDTO.UserDetail);
        }

        [TestMethod]
        public void UserCommunicationInfo_PropertiesAreSetCorrectly()
        {
            // Arrange
            var communicationType = CommunicationTypeEnum.Email;
            var communicationInfo = "john@example.com";
            var userInfoId = Guid.NewGuid();

            // Act
            var userCommunicationInfo = new UserCommunicationInfo
            {
                CommunicationType = communicationType,
                CommunicationInfo = communicationInfo,
                UserInfoId = userInfoId
            };

            // Assert
            Assert.AreEqual(communicationType, userCommunicationInfo.CommunicationType);
            Assert.AreEqual(communicationInfo, userCommunicationInfo.CommunicationInfo);
            Assert.AreEqual(userInfoId, userCommunicationInfo.UserInfoId);
        }
        [TestMethod]
        public void UserInfo_PropertiesAreSetCorrectly()
        {
            // Arrange
            var name = "Adem";
            var surname = "Kesim";
            var companyName = "Rise";

            // Act
            var userInfo = new UserInfo
            {
                Name = name,
                Surname = surname,
                CompanyName = companyName
            };

            // Assert
            Assert.AreEqual(name, userInfo.Name);
            Assert.AreEqual(surname, userInfo.Surname);
            Assert.AreEqual(companyName, userInfo.CompanyName);
        }

        [TestMethod]
        public void UserInfo_DefaultValuesAreSetCorrectly()
        {
            // Arrange & Act
            var userInfo = new UserInfo();

            // Assert
            Assert.AreEqual(string.Empty, userInfo.Name);
            Assert.AreEqual(string.Empty, userInfo.Surname);
            Assert.AreEqual(string.Empty, userInfo.CompanyName);
        }

        [TestMethod]
        public void UserInfo_InheritsFromMongoDbEntity()
        {
            // Act
            var userInfo = new UserInfo();

            // Assert
            Assert.IsInstanceOfType(userInfo, typeof(MongoDbEntity));
        }
    }

}
