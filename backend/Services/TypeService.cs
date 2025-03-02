using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mappa.Services;

public class TypeService : EntityService<Entities.Type, TypeDto>
{
    public TypeService(AppDbContext dbContext) : base(dbContext)
    {
    }
}
