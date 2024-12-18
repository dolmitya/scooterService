using System;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using BL.Authorization;
using BL.Mapper;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using NUnit.Framework;
using ScooterDataAccess.Entities;

namespace BL.Tests.Authorization;

[TestFixture]
[TestOf(typeof(AuthProvider))]
public class AuthProviderTest
{
    private Mock<SignInManager<UserEntity>> _signInManagerMock;
    private Mock<UserManager<UserEntity>> _userManagerMock;
    private Mock<IHttpClientFactory> _httpClientFactoryMock;
    private IMapper _mapper;
    private AuthProvider _authProvider;

    [SetUp]
    public void SetUp()
    {
        var userStoreMock = new Mock<IUserStore<UserEntity>>();
        _userManagerMock = new Mock<UserManager<UserEntity>>(userStoreMock.Object,
            null, null, null, null, null, null, null, null);

        var contextAccessorMock = new Mock<IHttpContextAccessor>();
        var userClaimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<UserEntity>>();
        _signInManagerMock = new Mock<SignInManager<UserEntity>>(
            _userManagerMock.Object, contextAccessorMock.Object, userClaimsPrincipalFactoryMock.Object, null, null,
            null, null);

        _httpClientFactoryMock = new Mock<IHttpClientFactory>();

        var mapperConfig = new MapperConfiguration(cfg => { cfg.AddProfile(new UsersBLProfile()); });
        _mapper = mapperConfig.CreateMapper();

        _authProvider = new AuthProvider(
            _signInManagerMock.Object,
            _userManagerMock.Object,
            _httpClientFactoryMock.Object,
            "http://identityserver",
            "test-client",
            "test-secret",
            _mapper);
    }

    [Test]
    public async Task AuthorizeUser_ThrowsException_WhenUserNotFound()
    {
        _userManagerMock.Setup(um => um.FindByEmailAsync("invalid@example.com"))
            .ReturnsAsync((UserEntity)null);

        Func<Task> act = async () =>
            await _authProvider.AuthorizeUser("invalid@example.com", "password123");

        act.Should().ThrowAsync<Exception>().WithMessage("User not found.");
    }

    [Test]
    public async Task RegisterUser_CreatesUser_WhenDataIsValid()
    {
        var email = "test@example.com";
        var password = "password123@";

        _userManagerMock.SetupSequence(um => um.FindByEmailAsync(email))
            .ReturnsAsync((UserEntity)null)
            .ReturnsAsync(new UserEntity());

        _userManagerMock.Setup(um => um.CreateAsync(It.IsAny<UserEntity>(), password))
            .ReturnsAsync(IdentityResult.Success);
        var result = await _authProvider.RegisterUser(email, password);

        result.Should().NotBeNull();
        _userManagerMock.Verify(um => um.CreateAsync(It.IsAny<UserEntity>(), password), Times.Once);
    }
}