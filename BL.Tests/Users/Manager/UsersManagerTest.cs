using System;
using AutoMapper;
using BL.Users.Entitiy;
using BL.Users.Manager;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using ScooterDataAccess.Entities;
using ScooterDataAccess.Repository;

namespace BL.Tests.Users.Manager;

[TestFixture]
[TestOf(typeof(UsersManager))]
public class UsersManagerTest
{
    private IMapper _mapper;
    private Mock<IRepository<UserEntity>> _repositoryMock;
    private UsersManager _manager;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<CreateUserModel, UserEntity>();
            cfg.CreateMap<UpdateUserModel, UserEntity>();
            cfg.CreateMap<UserEntity, UserModel>();
        });
        _mapper = mapperConfig.CreateMapper();

        _repositoryMock = new Mock<IRepository<UserEntity>>();
        _manager = new UsersManager(_repositoryMock.Object, _mapper);
    }

    [SetUp]
    public void Setup()
    {
        _repositoryMock.Reset();
    }

    [Test]
    public void CreateUser_ShouldAddUserToRepository()
    {
        var createModel = new CreateUserModel
        {
            Login = "test_user",
            Email = "test@example.com",
            Surname = "User"
        };

        var userEntity = new UserEntity
        {
            Id = 0,
            UserName = createModel.Login,
            Surname = createModel.Surname,
            Email = createModel.Email
        };

        _repositoryMock.Setup(repo => repo.Save(It.IsAny<UserEntity>()))
            .Callback<UserEntity>(user => user.Id = 1)
            .Returns((UserEntity user) => user);

        var result = _manager.CreateUser(createModel);

        _repositoryMock.Verify(repo => repo.Save(It.IsAny<UserEntity>()), Times.Once);
        result.Id.Should().Be(1);
        result.Surname.Should().Be("User");
        result.Email.Should().Be("test@example.com");
    }

    [Test]
    public void DeleteUser_ShouldRemoveUserFromRepository()
    {
        var user = new UserEntity
        {
            Id = 1,
            UserName = "delete_user",
            Surname = "User",
            Email = "delete@example.com",
            CreationTime = DateTime.UtcNow
        };

        _repositoryMock.Setup(repo => repo.GetById(1)).Returns(user);
        _repositoryMock.Setup(repo => repo.Delete(It.IsAny<UserEntity>()));

        _manager.DeleteUser(1);

        _repositoryMock.Verify(repo => repo.Delete(It.IsAny<UserEntity>()), Times.Once);
    }

    [Test]
    public void UpdateUser_ShouldModifyExistingUser()
    {
        var user = new UserEntity
        {
            Id = 2,
            Surname = "Old",
            Email = "update@example.com",
            CreationTime = DateTime.UtcNow
        };

        var updateModel = new UpdateUserModel
        {
            Surname = "Updated",
            Email = "updated@example.com"
        };

        _repositoryMock.Setup(repo => repo.GetById(2)).Returns(user);
        _repositoryMock.Setup(repo => repo.Save(It.IsAny<UserEntity>()))
            .Callback<UserEntity>(u =>
            {
                u.Surname = updateModel.Surname;
                u.Email = updateModel.Email;
            })
            .Returns((UserEntity u) => u);
        var updateUserModel = _manager.UpdateUser(updateModel);

        _repositoryMock.Verify(repo => repo.Save(It.IsAny<UserEntity>()), Times.Once);
        updateUserModel.Surname.Should().Be("Updated");
        updateUserModel.Email.Should().Be("updated@example.com");
    }
}