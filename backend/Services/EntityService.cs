using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;

namespace Mappa.Services;

public class EntityService<TEntity, TDto> : IEntityService<TEntity, TDto>
    where TEntity : class, IEntity, new()
    where TDto : BaseDto, new()
{
    private readonly AppDbContext _dbContext;

    public EntityService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<TDto>> GetAllAsync()
    {
        return await _dbContext.Set<TEntity>()
            .Select(e => new TDto
            {
                Id = e.Id,
                Name = e.Name
            })
            .ToListAsync();
    }

    public async Task<TDto> GetByIdAsync(int id)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        if (entity == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        return new TDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public async Task<TDto> CreateAsync(CreateRequest request)
    {
        var entity = new TEntity();
        typeof(TEntity).GetProperty("Name")?.SetValue(entity, request.Name);

        _dbContext.Set<TEntity>().Add(entity);
        await _dbContext.SaveChangesAsync();

        return new TDto
        {
            Id = entity.Id,
            Name = entity.Name
        };
    }

    public async Task<TDto> UpdateAsync(int id, UpdateRequest request)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        if (entity == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        typeof(TEntity).GetProperty("Name")?.SetValue(entity, request.Name);

        await _dbContext.SaveChangesAsync();

        return new TDto
        {
            Id = id,
            Name = request.Name
        };
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await _dbContext.Set<TEntity>().FindAsync(id);
        if (entity == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        _dbContext.Set<TEntity>().Remove(entity);
        await _dbContext.SaveChangesAsync();
    }
}
