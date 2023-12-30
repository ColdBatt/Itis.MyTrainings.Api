using Itis.MyTrainings.Api.Contracts.Requests.UserProfile.PostUserProfile;
using Itis.MyTrainings.Api.Core.Abstractions;
using Itis.MyTrainings.Api.Core.Exceptions;
using MediatR;

namespace Itis.MyTrainings.Api.Core.Requests.UserProfile.PostUserProfile;

/// <summary>
/// Обработчик запроса <see cref="PostUserProfileCommand"/>
/// </summary>
public class PostUserProfileCommandHandler: IRequestHandler<PostUserProfileCommand, PostUserProfileResponse>
{
    private readonly IDbContext _dbContext;
    private readonly IUserService _userService;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="dbContext">Контекст БД</param>
    /// <param name="userService">Сервис для работы с пользователями</param>
    public PostUserProfileCommandHandler(
        IDbContext dbContext, 
        IUserService userService)
    {
        _dbContext = dbContext;
        _userService = userService;
    }

    /// <inheritdoc />
    public async Task<PostUserProfileResponse> Handle(PostUserProfileCommand request, CancellationToken cancellationToken)
    {
        var user = await _userService.FindUserByIdAsync(request.UserId)
            ?? throw new EntityNotFoundException<Entities.User>(request.UserId);

        var userProfile = new Entities.UserProfile
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
        };

        _dbContext.UserProfiles.Add(userProfile);

        user.Profile = userProfile;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new PostUserProfileResponse(userProfile.Id);
    }
}