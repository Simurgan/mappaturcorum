using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace Mappa.Services;

public class CityService : IComplexEntityService<City, 
    CityGeneralDto, CityDetailDto, 
    CityCreateRequest, CityUpdateRequest>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public CityService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<CityGeneralDto>> GetAllAsync()
    {
        return await _dbContext.Set<City>()
            .Select(e => new CityGeneralDto
            {
                Id = e.Id,
                AsciiName = e.AsciiName,
            })
            .ToListAsync();
    }

    public async Task<CityDetailDto> GetByIdAsync(int id)
    {
        var entity = await _dbContext.Set<City>()
            .FirstOrDefaultAsync(ws => ws.Id == id);
        
        if (entity == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        return new CityDetailDto
        {
            Id = entity.Id,
            AsciiName = entity.AsciiName,
            GeoNamesId = entity.GeoNamesId,
            AlternateNames = entity.AlternateNames,
            CountryCode = entity.CountryCode,
        };
    }

    public async Task<CityDetailDto> CreateAsync(CityCreateRequest request)
    {
        bool exists = await _dbContext.Set<City>().AnyAsync(c => 
            EF.Property<string>(c, "AsciiName") == request.AsciiName);

        if (exists)
        {
            throw new ArgumentException($"An entity with the name '{request.AsciiName}' already exists.");
        }

        var city = new City
        {
            AsciiName = request.AsciiName,
            GeoNamesId = request.GeoNamesId,
            AlternateNames = request.AlternateNames,
            CountryCode = request.CountryCode,
        };
        
        _dbContext.Set<City>().Add(city);
        await _dbContext.SaveChangesAsync();

        return new CityDetailDto
        {
            Id = city.Id,
            AsciiName = city.AsciiName,
            GeoNamesId = city.GeoNamesId,
            AlternateNames = city.AlternateNames,
            CountryCode = city.CountryCode,
        };
    }

    public async Task<CityDetailDto> UpdateAsync(int id, CityUpdateRequest request)
    {
        var city = await _dbContext.Set<City>()
            .FirstOrDefaultAsync(ws => ws.Id == id);

        if (city == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        if (request.AsciiName != null)
            city.AsciiName = request.AsciiName;

        if (request.GeoNamesId != null)
            city.GeoNamesId = request.GeoNamesId;

        if (request.AlternateNames != null)
            city.AlternateNames = request.AlternateNames;

        if (request.CountryCode != null)
            city.CountryCode = request.CountryCode;

        await _dbContext.SaveChangesAsync();

        return new CityDetailDto
        {
            Id = city.Id,
            AsciiName = city.AsciiName,
            GeoNamesId = city.GeoNamesId,
            AlternateNames = city.AlternateNames,
            CountryCode = city.CountryCode,
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var city = await _dbContext.Set<City>().FindAsync(id);
        if (city == null)
            return false;

        _dbContext.Set<City>().Remove(city);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
