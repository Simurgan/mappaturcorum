using Mappa.Dtos;

namespace Mappa.Services;

public interface IEntityService<TEntity, TDto>
{
    Task<IEnumerable<TDto>> GetAllAsync();
    Task<TDto> GetByIdAsync(int id);
    Task<TDto> CreateAsync(CreateRequest request);
    Task<TDto> UpdateAsync(int id, UpdateRequest request);
    Task DeleteAsync(int id);
}
