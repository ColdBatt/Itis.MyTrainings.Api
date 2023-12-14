using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUser;
using Itis.MyTrainings.Api.Contracts.Requests.User.SignIn;
using Itis.MyTrainings.Api.Core.Requests.User.RegisterUser;
using Itis.MyTrainings.Api.Core.Requests.User.SignIn;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Контроллер сущности "Пользователь"
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController: Controller
{
    /// <summary>
    /// Зарегестрировать пользователя
    /// </summary>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<RegisterUserResponse> RegisterUser(
        [FromServices] IMediator mediator,
        [FromQuery] RegisterUserRequest request,
        CancellationToken cancellationToken) =>
        await mediator.Send(new RegisterUserCommand()
        {
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Role = request.Role,
            Email = request.Email,
            Password = request.Password
        },
        cancellationToken);
    
    /// <summary>
    /// Авторизоваться
    /// </summary>
    /// <returns></returns>
    [HttpPost("signIn")]
    public async Task<SignInResponse> SignIn(
        [FromServices] IMediator mediator,
        [FromQuery] SignInRequest request,
        CancellationToken cancellationToken) =>
        await mediator.Send(new SignInQuery
        {
            Email = request.Email,
            Password = request.Password
        },
        cancellationToken);
}