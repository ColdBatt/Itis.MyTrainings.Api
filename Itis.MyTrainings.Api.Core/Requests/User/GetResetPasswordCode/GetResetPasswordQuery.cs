using Itis.MyTrainings.Api.Contracts.Requests.User.GetResetPasswordCode;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.User.GetResetPasswordCode;

/// <summary>
/// Команда запроса <see cref="GetResetPasswordCodeRequest"/>
/// </summary>
public class GetResetPasswordQuery: GetResetPasswordCodeRequest, IRequest<GetResetPasswordCodeResponse>
{
}