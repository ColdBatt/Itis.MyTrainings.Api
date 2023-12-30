namespace Itis.MyTrainings.Api.Contracts.Requests.UserProfile.PostUserProfile;

/// <summary>
/// Ответ на запрос <see cref="PostUserProfileRequest"/>
/// </summary>
public class PostUserProfileResponse
{
    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="id">Идентификатор созданного профиля</param>
    public PostUserProfileResponse(Guid id)
        => UserProfileId = id;

    /// <summary>
    /// Идентификатор созданного профиля
    /// </summary>
    public Guid UserProfileId { get; }
}