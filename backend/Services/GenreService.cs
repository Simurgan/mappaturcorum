using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mappa.Services;

public class GenreService : EntityService<Genre, GenreDto>
{
    public GenreService(AppDbContext dbContext) : base(dbContext)
    {
    }
}
