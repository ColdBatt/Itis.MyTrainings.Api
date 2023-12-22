using System.ComponentModel.DataAnnotations;

namespace Itis.MyTrainings.Api.Contracts.Requests.User.GetResetPasswordCode;

/// <summary>
/// Запрос на получение кода для сброса пароля
/// </summary>
public class GetResetPasswordCodeRequest
{
    /// <summary>
    /// Email
    /// </summary>
    [Required]
    [DataType(DataType.EmailAddress)]
    public string Email { get; set; } = default!;
}