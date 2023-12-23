using System.Security.Claims;
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
    public async Task<User?> FindUserByIdAsync(Guid guid)
        => await _userManager.FindByIdAsync(guid.ToString());

    /// <inheritdoc />
    public async Task<IdentityResult> RegisterUserAsync(User user, string password)
        => await _userManager.CreateAsync(user, password);

    public async Task<IdentityResult> RegisterUserAsync(User user)
        => await _userManager.CreateAsync(user);

    /// <inheritdoc />
    public async Task<IdentityResult> AddUserRole(User user, string roleName)
        => await _userManager.AddToRoleAsync(user, roleName);

    /// <inheritdoc />
    public async Task<IdentityResult> AddClaimsAsync(User user, IEnumerable<Claim> claims)
        => await _userManager.AddClaimsAsync(user, claims);
    
    /// <inheritdoc />
    public async Task<IList<Claim>> GetClaimsAsync(User user)
        => await _userManager.GetClaimsAsync(user);

    /// <inheritdoc />
    public async Task<string?> GetRoleAsync(User user)
    {
        var claims = await _userManager.GetClaimsAsync(user);
        return claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;
    }

    /// <inheritdoc />
    public async Task<User?> FindUserByEmailAsync(string email)
        => await _userManager.FindByEmailAsync(email);

    /// <inheritdoc />
    public async Task<SignInResult> SignInWithPasswordAsync(User user, string password)
        => await _signInManager.PasswordSignInAsync(user, password, false, false);

    /// <inheritdoc />
    public async Task<IdentityResult> ResetPasswordAsync(User user, string code, string newPassword)
        => await _userManager.ResetPasswordAsync(user, code, newPassword);

    /// <inheritdoc />
    public async Task<string> GetPasswordResetTokenAsync(User user)
        => await _userManager.GeneratePasswordResetTokenAsync(user);
}