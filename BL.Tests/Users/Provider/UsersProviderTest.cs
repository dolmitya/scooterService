using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using BL.Users.Entitiy;
using BL.Users.Provider;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using ScooterDataAccess.Entities;
using ScooterDataAccess.Repository;

namespace BL.Tests.Users.Provider;

[TestFixture]
[TestOf(typeof(UsersProvider))]
public class UsersProviderTest
{
    private Mock<IRepository<UserEntity>> _repositoryMock;
    private IMapper _mapper;
    private UsersProvider _userProvider;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _repositoryMock = new Mock<IRepository<UserEntity>>();

        var mapperConfig = new MapperConfiguration(cfg => { cfg.CreateMap<UserEntity, UserModel>(); });
        _mapper = mapperConfig.CreateMapper();

        _userProvider = new UsersProvider(_repositoryMock.Object, _mapper);
    }

    [SetUp]
    public void Setup()
    {
        _repositoryMock.Reset();
    }

    [Test]
    public void GetUsers_ReturnsMappedUsers_WhenFilterIsNull()
    {
        var users = new List<UserEntity>
        {
            new UserEntity { Id = 1, UserName = "user1", Email = "user1@test.com" },
            new UserEntity { Id = 2, UserName = "user2", Email = "user2@test.com" }
        };
        _repositoryMock.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
            .Returns((Expression<Func<UserEntity, bool>> predicate) =>
                users.AsQueryable().Where(predicate));

        var result = _userProvider.GetUsers();

        result.Should().NotBeNull();
        result.Count().Should().Be(2);
    }

    [Test]
    public void GetUsers_ReturnsFilteredUsers_WhenFilterIsProvided()
    {
        var users = new List<UserEntity>
        {
            new UserEntity { Id = 1, UserName = "user1", Email = "user1@test.com" },
            new UserEntity { Id = 2, UserName = "user2", Email = "user2@test.com" }
        };

        _repositoryMock.Setup(repo => repo.GetAll(It.IsAny<Expression<Func<UserEntity, bool>>>()))
            .Returns((Expression<Func<UserEntity, bool>> predicate) =>
                users.AsQueryable().Where(predicate));

        var filter = new FilterUserModel { MailPart = "user1@test.com" };
        var result = _userProvider.GetUsers(filter);

        result.Should().HaveCount(1);
        result.First().Id.Should().Be(1);
    }

    [Test]
    public void GerUserInfo_ReturnsUser_WhenUserExists()
    {
        var user = new UserEntity { Id = 1, UserName = "user1", Email = "user1@test.com" };

        _repositoryMock.Setup(repo => repo.GetById(1))
            .Returns(user);

        var result = _userProvider.GerUserInfo(1);

        result.Should().NotBeNull();
        result.Id.Should().Be(1);
        result.Email.Should().Be("user1@test.com");
    }
}