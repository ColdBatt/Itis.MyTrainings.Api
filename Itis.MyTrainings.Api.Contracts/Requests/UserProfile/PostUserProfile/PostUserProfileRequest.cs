namespace Itis.MyTrainings.Api.Contracts.Requests.UserProfile.PostUserProfile;

/// <summary>
/// Запрос на добавление профиля пользователя
/// </summary>
public class PostUserProfileRequest
{
    /// <summary>
    /// Пол
    /// </summary>
    public string? Gender { get; set; }
    
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime? DateOfBirth { get; set; }
    
    /// <summary>
    /// Номер телефона
    /// </summary>
    public string? PhoneNumber { get; set; }

    /// <summary>
    /// Рос
    /// </summary>
    public int? Height { get; set; }
    
    /// <summary>
    /// Вес
    /// </summary>
    public int? Weight { get; set; }

    /// <summary>
    /// Дата начала тренировок
    /// </summary>
    public DateTime? StartDate { get; set; }
    
    /// <summary>
    /// Количество тренировок в неделю
    /// </summary>
    public int? WeeklyTrainingFrequency { get; set; }
    
    /// <summary>
    /// Заболевания
    /// </summary>
    public string? MedicalSickness { get; set; }
    
    /// <summary>
    /// Предпочтения по питанию
    /// </summary>
    public string? DietaryPreferences { get; set; }
}