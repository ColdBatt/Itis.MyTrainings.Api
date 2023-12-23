using System.Security.Claims;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUser;
using Itis.MyTrainings.Api.Contracts.Requests.User.RegisterUserWithVk;
using Itis.MyTrainings.Api.Core.Abstractions;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Itis.MyTrainings.Api.Core.Requests.User.RegisterUserWithVk;

public class RegisterUserWithVkCommandHandler : IRequestHandler<RegisterUserWithVkCommand, RegisterUserWithVkResponse>
{
    private readonly IUserService _userService;
    private readonly IRoleService _roleService;
    private readonly IDbContext _dbContext;
    private readonly IVkService _vkService;
    private readonly IJwtService _jwtService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="userService">Сервис для работы с пользователем</param>
    /// <param name="roleService">Сервис для работы с ролями</param>
    /// <param name="vkService">Сервис работы с ВКонтакте</param>
    /// <param name="dbContext">Контекст бд</param>
    /// <param name="jwtService">Сервис для работы с Jwt</param>
    public RegisterUserWithVkCommandHandler(
        IUserService userService,
        IRoleService roleService,
        IVkService vkService,
        IDbContext dbContext,
        IJwtService jwtService)
    {
        _userService = userService;
        _roleService = roleService;
        _dbContext = dbContext;
        _vkService = vkService;
        _jwtService = jwtService;
    }
    
    /// <inheritdoc />
    public async Task<RegisterUserWithVkResponse> Handle(
        RegisterUserWithVkCommand request, 
        CancellationToken cancellationToken)
    {
        var user = await GetUserFromVkAsync(request.Code!, cancellationToken);

        if (await _userService.FindUserByEmailAsync(user.Email!) != null)
            return new RegisterUserWithVkResponse(IdentityResult.Success, _jwtService.GenerateJwt(user.Id, "User"));
        
        var result = await _userService.RegisterUserAsync(user);

        if (result.Succeeded)
            await _userService.AddUserRole(user, "User");

        var claims = new List<Claim>
        {
            new (ClaimTypes.Role, "User")
        };

        if (result.Succeeded)
            await _userService.AddClaimsAsync(user, claims);

        return new RegisterUserWithVkResponse(result, _jwtService.GenerateJwt(user.Id, "User"));
    }

    private async Task<Entities.User> GetUserFromVkAsync(string code, CancellationToken cancellationToken)
    {
        await _vkService.GetAccessTokenAsync(code, cancellationToken);
        var info = (await _vkService.GetVkUserInfoAsync(cancellationToken)).Response;

        var user = new Entities.User()
        {
            FirstName = info.FirstName,
            LastName = info.LastName,
            PhoneNumber = info.Phone,
            UserName = info.Id.ToString(),
            Email = info.Mail,
        };
        return user;
    }
}