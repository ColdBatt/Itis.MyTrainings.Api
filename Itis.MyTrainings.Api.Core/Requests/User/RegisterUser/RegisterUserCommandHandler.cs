using System.Security.Claims;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUser;
using Itis.MyTrainings.Api.Core.Abstractions;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.RegisterUser;

/// <summary>
/// Обработчик запроса <see cref="RegisterUserCommand"/>
/// </summary>
public class RegisterUserCommandHandler
    : IRequestHandler<RegisterUserCommand, RegisterUserResponse>
{
    private readonly IUserService _userService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователем</param>
    public RegisterUserCommandHandler(IUserService userService)
    {
        _userService = userService;
    }

    /// <inheritdoc />
    public async Task<RegisterUserResponse> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var user = new Entities.User
        {
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Email = request.Email
        };

        var result = await _userService.RegisterUserAsync(user, request.Password);

        var claims = new List<Claim>
        {
            new (ClaimTypes.Role, request.Role)
        };

        if (result.Succeeded)
            await _userService.AddClaimsAsync(user, claims);

        return new RegisterUserResponse(result);
    }
}