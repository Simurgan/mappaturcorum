using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mappa.Services;

public class ProfessionService : EntityService<Profession, ProfessionDto>
{
    public ProfessionService(AppDbContext dbContext) : base(dbContext)
    {
    }
}
