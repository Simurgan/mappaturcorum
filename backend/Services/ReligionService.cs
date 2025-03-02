using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mappa.Services;

public class ReligionService : EntityService<Religion, ReligionDto>
{
    public ReligionService(AppDbContext dbContext) : base(dbContext)
    {
    }
}
