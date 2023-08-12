using ErrorOr;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Moq;
using Vensy.Application.Authentication.Commands;
using Vensy.Application.Authentication.Common;
using Vensy.Application.Test.Authentication.TestUtils;
using Vensy.Domain.Models;

namespace Vensy.Application.Test.Authentication.Commands;

public class RegisterCommandHandlerTest
{

    [Fact]
    public async void Handle_UserCreationPass_ReturnsARegistrationResult()
    {

        // Arrange
        RegisterCommand registerCommand = RegisterUserCommandUtils.CreateRegisterUserCommand();
        var newUser = new ApplicationUser() { Email = registerCommand.Email, Firstname = registerCommand.Firstname, Lastname = registerCommand.Lastname, UserName = registerCommand.Username };
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        var userManagerMoq = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        userManagerMoq.Setup(mock => mock.CreateAsync(It.IsAny<ApplicationUser>(), registerCommand.Password)).ReturnsAsync(IdentityResult.Success);
        RegisterCommandHandler _sut = new(userManager: userManagerMoq.Object);

        // Act
        ErrorOr<RegisterResult> result = await _sut.Handle(registerCommand, CancellationToken.None);

        // Assert
        Assert.NotNull(result.Value);
        Assert.IsType(typeof(RegisterResult), result.Value);
        Assert.NotEmpty(result.Value.Message);
        userManagerMoq.Verify(mock => mock.CreateAsync(It.IsAny<ApplicationUser>(), registerCommand.Password), Times.Once);
    }


    [Fact]
    public async void Handle_UserCreationFailed_ReturnsListOfErrors()
    {
        // Arrange
        RegisterCommand registerCommand = RegisterUserCommandUtils.CreateRegisterUserCommand();
        var newUser = new ApplicationUser() { Email = registerCommand.Email, Firstname = registerCommand.Firstname, Lastname = registerCommand.Lastname, UserName = registerCommand.Username };
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        var userManagerMoq = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        userManagerMoq.Setup(mock => mock.CreateAsync(It.IsAny<ApplicationUser>(), registerCommand.Password))
                      .ReturnsAsync(IdentityResult.Failed(new IdentityError[]
                      {
                          new() { Code = "Password Failed", Description = "Something Wrong with Password" },
                          new() { Code = "Username", Description = "Something Wrong with Username" }
                      }));
        RegisterCommandHandler _sut = new(userManager: userManagerMoq.Object);

        // Act
        ErrorOr<RegisterResult> result = await _sut.Handle(registerCommand, CancellationToken.None);

        // Assert
        Assert.NotNull(result.Errors);
        Assert.Equal(2, result.Errors.Count);
        userManagerMoq.Verify(mock => mock.CreateAsync(It.IsAny<ApplicationUser>(), registerCommand.Password), Times.Once);
    }

    [Fact]
    public async void Handle_UserAlreadyExists_ReturnsConflictError()
    {
        // Arrange
        RegisterCommand registerCommand = RegisterUserCommandUtils.CreateRegisterUserCommand();
        var newUser = new ApplicationUser() { Email = registerCommand.Email, Firstname = registerCommand.Firstname, Lastname = registerCommand.Lastname, UserName = registerCommand.Username };
        var userStoreMock = new Mock<IUserStore<ApplicationUser>>();
        var userManagerMoq = new Mock<UserManager<ApplicationUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        userManagerMoq.Setup(mock => mock.FindByEmailAsync(registerCommand.Email))
                      .ReturnsAsync(newUser);
        RegisterCommandHandler _sut = new(userManager: userManagerMoq.Object);

        // Act
        ErrorOr<RegisterResult> result = await _sut.Handle(registerCommand, CancellationToken.None);

        // Assert
        Assert.NotNull(result.Errors);
        Assert.Equal(1, result.Errors.Count);
        Assert.Equal(StatusCodes.Status409Conflict, (int) result.Errors[0].Type);
        userManagerMoq.Verify(mock => mock.FindByEmailAsync(registerCommand.Email), Times.Once);
    }
}