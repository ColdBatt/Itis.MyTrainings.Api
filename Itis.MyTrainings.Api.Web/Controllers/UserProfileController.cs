using System.Security.Claims;
using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.GetUserProfileById;
using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.PostUserProfile;
using Itis.MyTrainings.Api.Core.Requests.UserProfile.GetUserProfileById;
using Itis.MyTrainings.Api.Core.Requests.UserProfile.PostUserProfile;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Itis.MyTrainings.Api.Web.Controllers;

/// <summary>
/// Контроллер для профиля пользователя
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class UserProfileController: Controller
{
    /// <summary>
    /// Получить профиль пользователя по идентификатор
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="userId">Идентификатор пользователя</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [HttpGet("getProfileById/{userId}")]
    public async Task<GetUserProfileByIdResponse> GetProfileById(
        [FromServices] IMediator mediator,
        [FromRoute] Guid userId,
        CancellationToken cancellationToken) =>
        await mediator.Send(
            new GetUserProfileByIdQuery(userId), 
            cancellationToken);

    /// <summary>
    /// Получить профиль пользователя по идентификатор
    /// </summary>
    /// <param name="mediator">Медиатор CQRS</param>
    /// <param name="request">Запрос</param>
    /// <param name="cancellationToken">Токен отмены запроса</param>
    /// <returns></returns>
    [Authorize]
    [HttpPost("postUserProfile")]
    public async Task<ActionResult> CreateUserProfile(
        [FromServices] IMediator mediator,
        [FromBody] PostUserProfileRequest request,
        CancellationToken cancellationToken)
    {
        var currentUserId = User.FindFirst(ClaimTypes.NameIdentifier);
        if (currentUserId == null)
            return BadRequest("Идентификатор пользователя не найден");
        
        var currentUserGuid = Guid.Parse(currentUserId.Value);

        PostUserProfileResponse result;
        try
        {
            result = await mediator.Send(
                new PostUserProfileCommand(currentUserGuid)
                {
                    Gender = request.Gender,
                    DateOfBirth = request.DateOfBirth,
                    PhoneNumber = request.PhoneNumber,
                    Height = request.Height,
                    Weight = request.Weight,
                    StartDate = request.StartDate,
                    WeeklyTrainingFrequency = request.WeeklyTrainingFrequency,
                    MedicalSickness = request.MedicalSickness,
                    DietaryPreferences = request.DietaryPreferences,
                }, 
                cancellationToken);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        return Ok(result);
    }
        

    #region 

    /// <summary>
    /// Пасхалочка
    /// </summary>
    /// <returns></returns>
    [HttpGet("chipi")]
    public IActionResult Chipi()
    {
        return new ContentResult()
        {
            Content = $"<!DOCTYPE html>\n<html>\n<head>\n   " +
                      $"</head>\n<body>\n    <img style=\"width: 100%\" " +
                      $"src=\"https://media1.tenor.com/m/s7Tf_aL-Di0AAAAC/chipi-chipi-chapa-chapa.gif\" alt=\"Гифка\">\n</body>\n</html>",
            ContentType = "text/html",
            StatusCode = 200,
        };
    }

    #endregion
}