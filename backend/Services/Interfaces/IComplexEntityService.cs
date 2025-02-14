using Mappa.Dtos;

namespace Mappa.Services;

public interface IComplexEntityService<TEntity, TGeneralDto, TDetailDto, 
    TCreateRequest, TUpdateRequest, TFilterDto>
{
    Task<IEnumerable<TGeneralDto>> GetAllAsync();
    Task<TDetailDto> GetByIdAsync(int id);
    Task<TDetailDto> CreateAsync(TCreateRequest request);
    Task<TDetailDto> UpdateAsync(int id, TUpdateRequest request);
    Task<bool> DeleteAsync(int id);
    Task<PaginationResponse<TGeneralDto>> GetPageAsync(int pageNumber, int pageSize,
        TFilterDto filter);
}
