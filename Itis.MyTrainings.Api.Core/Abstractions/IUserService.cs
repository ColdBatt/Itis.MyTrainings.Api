using Itis.MyTrainings.Api.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Itis.MyTrainings.Api.Core.Abstractions;

/// <summary>
/// Сервис для работы с пользователем
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Зарегестрировать пользователя
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="password">Пароль</param>
    /// <returns></returns>
    public Task<IdentityResult> RegisterUser(User user, string password);

    /// <summary>
    /// Получить пользователя по Email
    /// </summary>
    /// <param name="email">Email</param>
    /// <returns></returns>
    public Task<User?> FindUserByEmailAsync(string email);

    /// <summary>
    /// Войти с помощью пароля
    /// </summary>
    /// <param name="user">Пользователь</param>
    /// <param name="password">Пароль</param>
    /// <returns></returns>
    public Task<SignInResult> SignInWithPasswordAsync(User user, string password);
}