using Itis.MyTrainings.Api.Contracts.Requests.User.GetResetPasswordCode;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUser;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithVk;
using Itis.MyTrainings.Api.Contracts.Requests.User.ResetPassword;
using Itis.MyTrainings.Api.Contracts.Requests.User.SignIn;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Requests.User.GetResetPasswordCode;
using Itis.MyTrainings.Api.Core.Requests.User.RegisterUser;
using Itis.MyTrainings.Api.Core.Requests.User.RegisterUserWithVk;
using Itis.MyTrainings.Api.Core.Requests.User.ResetPassword;
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
    private readonly IVkService _vkService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="vkService">Сервис для работы с ВКонтакте</param>
    public UserController(
        IVkService vkService)
    {
        _vkService = vkService;
    }

    /// <summary>
    /// Зарегестрировать пользователя
    /// </summary>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser(
        [FromServices] IMediator mediator,
        [FromBody] RegisterUserRequest request)
    {
        RegisterUserResponse result;
        try
        {
            result = await mediator.Send(new RegisterUserCommand()
            {
                UserName = request.UserName,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Role = request.Role,
                Email = request.Email,
                Password = request.Password,
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(result.Result);
    }
        
    
    /// <summary>
    /// Авторизоваться
    /// </summary>
    /// <returns></returns>
    [HttpPost("signIn")]
    public async Task<ActionResult> SignIn(
        [FromServices] IMediator mediator,
        [FromBody] SignInRequest request)
    {
        SignInResponse result;
        try
        {
            result = await mediator.Send(new SignInQuery
            {
                Email = request.Email,
                Password = request.Password,
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(result.Result);
    }

    /// <summary>
    /// Отправить код восстановления пароля
    /// </summary>
    /// <param name="mediator">IMediator</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">CancellationToken</param>
    /// <returns></returns>
    [HttpPost("sendResetPasswordCode")]
    public async Task<ActionResult> SendResetPasswordCode(
        [FromServices] IMediator mediator,
        [FromBody] SendResetPasswordCodeRequest request,
        CancellationToken cancellationToken)
    {
        SendResetPasswordCodeResponse result;
        try
        {
            result = await mediator.Send(new SendResetPasswordQuery
            {
                Email = request.Email
            }, cancellationToken);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(result.Result);
    }


    /// <summary>
    /// Сбросить пароль
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("resetPassword")]
    public async Task<ActionResult> ResetPassword(
        [FromServices] IMediator mediator,
        [FromBody] ResetPasswordRequest request)
    {
        ResetPasswordResponse result;
        try
        {
            result = await mediator.Send(new ResetPasswordCommand
            {
                Email = request.Email,
                Password = request.Password,
                Code = request.Code,
            });
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(result.Result);
    }
        

    /// <summary>
    /// Авторизировать пользователя через вконтакте
    /// </summary>
    /// <returns>Редирект на форму авторизации</returns>
    [HttpGet("authorizeVk")]
    public async Task<RedirectResult> VkAuthorize() 
        => Redirect(_vkService.GetRedirectToAuthorizationUrl());
    
    /// <summary>
    /// Авторизировать пользователя с помощью Вконтакте
    /// </summary>
    /// <returns></returns>
    [HttpGet("registerWithVk")]
    public async Task<RegisterUserWithVkResponse> RegisterUserWithVk(
        [FromServices] IMediator mediator,
        [FromQuery] string code) =>
        await mediator.Send(new RegisterUserWithVkCommand(code));
}