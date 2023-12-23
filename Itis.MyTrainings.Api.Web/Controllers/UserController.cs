﻿using Itis.MyTrainings.Api.Contracts.Requests.User.GetResetPasswordCode;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUser;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithVk;
using Itis.MyTrainings.Api.Contracts.Requests.User.ResetPassword;
using Itis.MyTrainings.Api.Contracts.Requests.User.SignIn;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities;
using Itis.MyTrainings.Api.Core.Requests.User.GetResetPasswordCode;
using Itis.MyTrainings.Api.Core.Requests.User.RegisterUser;
using Itis.MyTrainings.Api.Core.Requests.User.RegisterUserWithVk;
using Itis.MyTrainings.Api.Core.Requests.User.ResetPassword;
using Itis.MyTrainings.Api.Core.Requests.User.SignIn;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Контроллер сущности "Пользователь"
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserController: Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IVkService _vkService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userManager">Менеджер пользователя</param>
    /// <param name="vkService">Сервис для работы с ВКонтакте</param>
    public UserController(
        UserManager<User> userManager,
        IVkService vkService)
    {
        _userManager = userManager;
        _vkService = vkService;
    }
    
    /// <summary>
    /// Зарегестрировать пользователя
    /// </summary>
    /// <returns></returns>
    [HttpPost("register")]
    public async Task<RegisterUserResponse> RegisterUser(
        [FromServices] IMediator mediator,
        [FromBody] RegisterUserRequest request,
        CancellationToken cancellationToken) =>
        await mediator.Send(new RegisterUserCommand()
        {
            UserName = request.UserName,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Role = request.Role,
            Email = request.Email,
            Password = request.Password,
        },
        cancellationToken);
    
    /// <summary>
    /// Авторизоваться
    /// </summary>
    /// <returns></returns>
    [HttpPost("signIn")]
    public async Task<SignInResponse> SignIn(
        [FromServices] IMediator mediator,
        [FromBody] SignInRequest request,
        CancellationToken cancellationToken) =>
        await mediator.Send(new SignInQuery
        {
            Email = request.Email,
            Password = request.Password,
        },
        cancellationToken);

    /// <summary>
    /// Получить код восстановления пароля
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("getResetPasswordCode")]
    public async Task<GetResetPasswordCodeResponse> GetResetPasswordCode(
        [FromServices] IMediator mediator,
        [FromBody] GetResetPasswordCodeRequest request,
        CancellationToken cancellationToken) =>
        await mediator.Send(new GetResetPasswordQuery
        {
            Email = request.Email
        },
        cancellationToken);
    
    /// <summary>
    /// Сбросить пароль
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    [HttpPost("resetPassword")]
    public async Task<ResetPasswordResponse> ResetPassword(
        [FromServices] IMediator mediator,
        [FromBody] ResetPasswordRequest request,
        CancellationToken cancellationToken) =>
        await mediator.Send(new ResetPasswordCommand
        {
            Email = request.Email,
            Password = request.Password,
            Code = request.Code,
        },
        cancellationToken);

    /// <summary>
    /// Авторизировать пользователя через вконтакте
    /// </summary>
    /// <returns>Редирект на форму авторизации</returns>
    [HttpGet("authorizeVk")]
    public async Task<RedirectResult> VkAuthorize() 
        => Redirect(
            "https://oauth.vk.com/authorize?client_id=51816062&display=page&response_type=code&v=5.131&redirect_uri=https://localhost/after-auth");
    
    
    /// <summary>
    /// Авторизировать пользователя с помощью Вконтакте
    /// </summary>
    /// <returns></returns>
    [HttpPost("registerWithVk/{code}")]
    public async Task<RegisterUserWithVkResponse> RegisterUserWithVk(
        [FromServices] IMediator mediator,
        [FromRoute] string code,
        CancellationToken cancellationToken) =>
        await mediator.Send(new RegisterUserWithVkCommand(code),
            cancellationToken);
}