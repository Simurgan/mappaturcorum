using Mappa.Dtos;

namespace Mappa.Services;

public interface IComplexEntityService<TEntity, TGeneralDto, TDetailDto, 
    TCreateRequest, TUpdateRequest, TFilterDto, TFilterSearchResponseDto, TGraphDto,
    TSearchDto>
{
    Task<IEnumerable<TGeneralDto>> GetAllAsync();
    Task<TDetailDto> GetByIdAsync(int id);
    Task<TDetailDto> CreateAsync(TCreateRequest request);
    Task<TDetailDto> UpdateAsync(int id, TUpdateRequest request);
    Task<bool> DeleteAsync(int id);
    Task<PaginationResponse<TFilterSearchResponseDto>> GetPageAsync(int pageNumber, 
        int pageSize, TFilterDto filter);
    Task<PaginationResponse<TFilterSearchResponseDto>> GetPageAsync(int pageNumber, 
        int pageSize, string sortingField, bool isDescendingOrder,TFilterDto filter, 
        TSearchDto search);
    Task<IEnumerable<TGraphDto>> GetAllForGraphAsync();
}
