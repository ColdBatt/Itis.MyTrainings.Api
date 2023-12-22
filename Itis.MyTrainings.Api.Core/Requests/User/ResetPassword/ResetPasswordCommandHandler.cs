using Itis.MyTrainings.Api.Contracts.Requests.User.ResetPassword;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.ResetPassword;

/// <summary>
/// Обработчик запроса <see cref="ResetPasswordCommand"/>
/// </summary>
public class ResetPasswordCommandHandler
    : IRequestHandler<ResetPasswordCommand, ResetPasswordResponse>
{
    private readonly IUserService _userService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService"></param>
    public ResetPasswordCommandHandler(IUserService userService)
        => _userService = userService;

    /// <inheritdoc />
    public async Task<ResetPasswordResponse> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.FindUserByEmailAsync(request.Email)
            ?? throw new EntityNotFoundException<Entities.User>($"Не найдены пользователи со следующим email: {request.Email}");

        var result = await _userService.ResetPasswordAsync(user, request.Code, request.Password);

        return new ResetPasswordResponse(result);
    }
}