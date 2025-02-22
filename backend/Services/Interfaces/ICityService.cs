using Mappa.Dtos;

namespace Mappa.Services;

public interface ICityService
{
    Task<IEnumerable<CityGeneralDto>> GetAllAsync();
    Task<CityDetailDto> GetByIdAsync(int id);
    Task<CityDetailDto> CreateAsync(CityCreateRequest request);
    Task<CityDetailDto> UpdateAsync(int id, CityUpdateRequest request);
    Task<bool> DeleteAsync(int id);
    Task<IEnumerable<CityFilterResponseDto>> GetAllFilteredAsync(CityFilterDto filter);
    Task<IEnumerable<CityMapDto>> GetAllForMapAsync();
}
