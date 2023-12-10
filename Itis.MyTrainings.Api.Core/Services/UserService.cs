using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Entities;
using Microsoft.AspNetCore.Identity;

namespace Itis.MyTrainings.Api.Core.Services;

/// <inheritdoc />
public class UserService: IUserService
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signInManager;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userManager">API для управления пользователем</param>
    /// <param name="signInManager">API для входа пользователей</param>
    public UserService(
        UserManager<User> userManager,
        SignInManager<User> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }
    
    /// <inheritdoc />
    public async Task<IdentityResult> RegisterUser(User user, string password)
        => await _userManager.CreateAsync(user, password);

    /// <inheritdoc />
    public async Task<User?> FindUserByEmailAsync(string email)
        => await _userManager.FindByEmailAsync(email);

    /// <inheritdoc />
    public async Task<SignInResult> SignInWithPasswordAsync(User user, string password)
        => await _signInManager.PasswordSignInAsync(user, password, false, false);
}