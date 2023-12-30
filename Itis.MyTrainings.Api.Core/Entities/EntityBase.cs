using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Itis.MyTrainings.Api.Core.Abstractions;

namespace Itis.MyTrainings.Api.Core.Entities;

public class EntityBase : IEntity
{
    /// <summary>
    /// ИД сущности
    /// </summary>
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }
}