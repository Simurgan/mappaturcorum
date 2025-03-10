using Mappa.Dtos;

namespace Mappa.Services;

public interface IComplexEntityService<TEntity, TGeneralDto, TDetailDto, 
    TCreateRequest, TUpdateRequest, TFilterDto, TFilterResponseDto, TGraphDto>
{
    Task<IEnumerable<TGeneralDto>> GetAllAsync();
    Task<TDetailDto> GetByIdAsync(int id);
    Task<TDetailDto> CreateAsync(TCreateRequest request);
    Task<TDetailDto> UpdateAsync(int id, TUpdateRequest request);
    Task<bool> DeleteAsync(int id);
    Task<PaginationResponse<TFilterResponseDto>> GetPageAsync(int pageNumber, int pageSize,
        TFilterDto filter);
    Task<IEnumerable<TGraphDto>> GetAllForGraphAsync();
}
