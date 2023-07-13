using Vensy.Application.Authentication.Commands;
using Vensy.Application.Test.TestUils;
namespace Vensy.Application.Test.Authentication.TestUtils;



public static class RegisterUserCommandUtils
{
    public static RegisterCommand CreateRegisterUserCommand() =>
        new RegisterCommand(Constants.User.Email, Constants.User.Password, Constants.User.Firstname, Constants.User.Lastname, Constants.User.UserName);
    
}