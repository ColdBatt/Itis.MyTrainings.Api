using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Itis.MyTrainings.Api.Core.Entities;

/// <summary>
/// Пользователь
/// </summary>
public class User: IdentityUser<Guid>
{
    /// <summary>
    /// Имя пользователя
    /// </summary>
    [Required]
    public string FirstName { get; set; } = default!;
        
    /// <summary>
    /// Фамилия пользователя
    /// </summary>
    [Required]
    public string LastName { get; set; } = default!;
}