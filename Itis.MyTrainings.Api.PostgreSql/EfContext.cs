using Itis.MyTrainings.Api.Core.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Itis.MyTrainings.Api.PostgreSql;

/// <summary>
/// Контекст EF Core для приложения
/// </summary>
public class EfContext: DbContext, IDbContext
{
    
}