using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mappa.Services;

public class LanguageService : EntityService<Language, LanguageDto>
{
    public LanguageService(AppDbContext dbContext) : base(dbContext)
    {
    }
}
