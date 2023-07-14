using MediatR;
using Vensy.Application.Interfaces.Persistence;

namespace Vensy.Application.Authentication.Commands;


public class UpdateUserRefreshTokenCommandHandler : IRequestHandler<UpdateUserRefreshTokenCommand>
{

    private readonly IUserRepository _userRepository;
    public UpdateUserRefreshTokenCommandHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task Handle(UpdateUserRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user =  _userRepository.FindByEmail(email: request.Email);
        if(user is null)
            return Task.CompletedTask;
        var oldToken = user.RefreshTokens.Where(refreshToken => refreshToken.Token == request.OldToken).First();
        if (request.OldToken is null ){
            user.RefreshTokens.Add(new(){Token= request.NewToken});
            return Task.CompletedTask;
        }
        oldToken.Token = request.NewToken;
        _userRepository.Save(user);
        return Task.CompletedTask;
    }
}