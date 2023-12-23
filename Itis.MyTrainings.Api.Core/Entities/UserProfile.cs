using Itis.MyTrainings.Api.Core.Exceptions;

namespace Itis.MyTrainings.Api.Core.Entities;

/// <summary>
/// Профиль пользователя
/// </summary>
public class UserProfile : EntityBase
{
    //private User _user;
    
    /// <summary>
    /// Конструктор
    /// </summary>
    public UserProfile()
    {
    }
    
    // /// <summary>
    // /// Идентификатор пользователя
    // /// </summary>
    // public Guid UserId { get; set; }
    
    /// <summary>
    /// Пол
    /// </summary>
    public string? Gender { get; set; }
    
    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime? DateOfBirth { get; set; }
    
    /// <summary>
    /// Почта
    /// </summary>
    public string? Email { get; set; }
    
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
    /// Список целей  TODO подумать над вынесением в отдельный класс и переделке в one2many
    /// </summary>
    public List<string>? Goals { get; set; }
    
    /// <summary>
    /// Личные тренировки  TODO подумать над вынесением в отдельный класс и переделке в one2many
    /// </summary>
    public List<string>? TrainingPreference { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public List<string>? PreferredWorkoutTypes { get; set; } // List of preferred workout types

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