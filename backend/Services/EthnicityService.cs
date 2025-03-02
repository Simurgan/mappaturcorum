using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mappa.Services;

public class EthnicityService : EntityService<Ethnicity, EthnicityDto>
{
    public EthnicityService(AppDbContext dbContext) : base(dbContext)
    {
    }
}
