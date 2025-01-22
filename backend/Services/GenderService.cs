using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mappa.Services;

public class GenderService : EntityService<Gender, GenderDto>
{
    public GenderService(AppDbContext dbContext) : base(dbContext)
    {
    }
}
