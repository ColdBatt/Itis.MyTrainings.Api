using System.ComponentModel.DataAnnotations.Schema;
using Itis.MyTrainings.Api.Core.Abstractions;

namespace Itis.MyTrainings.Api.Core.Entities;

public class EntityBase : IEntity
{
    /// <summary>
    /// ИД сущности
    /// </summary>
    [ForeignKey(nameof(Id))]
    public Guid Id { get; set; }
}