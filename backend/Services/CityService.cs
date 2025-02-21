using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace Mappa.Services;

public class CityService : ICityService
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
                Name = e.Name,
            })
            .OrderBy(e => e.Id)
            .ToListAsync();
    }

    public async Task<CityDetailDto> GetByIdAsync(int id)
    {
        var entity = await _dbContext.Set<City>()
            .Include(op => op.LocationOf)
            .Include(op => op.BackgroundCityOf)
            .Include(op => op.BirthPlaceOf)
            .Include(op => op.DeathPlaceOf)
            .Include(op => op.SourcesMentioningTheCity)
            .Include(op => op.SourcesWrittenInTheCity)
            .FirstOrDefaultAsync(ws => ws.Id == id);
        
        if (entity == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        return new CityDetailDto
        {
            Id = entity.Id,
            AsciiName = entity.AsciiName,
            Name = entity.Name,
            GeoNamesId = entity.GeoNamesId,
            AlternateNames = entity.AlternateNames,
            CountryCode = entity.CountryCode,
            Latitude = entity.Latitude,
            Longitude = entity.Longitude,
            LocationOf = _mapper.Map<List<OrdinaryPersonBaseDto>>(entity.LocationOf),
            BackgroundCityOf = _mapper.Map<List<OrdinaryPersonBaseDto>>(entity.BackgroundCityOf),
            BirthPlaceOf = _mapper.Map<List<UnordinaryPersonBaseDto>>(entity.BirthPlaceOf),
            DeathPlaceOf = _mapper.Map<List<UnordinaryPersonBaseDto>>(entity.DeathPlaceOf),
            SourcesMentioningTheCity = _mapper.Map<List<WrittenSourceBaseDto>>(entity.SourcesMentioningTheCity),
            SourcesWrittenInTheCity = _mapper.Map<List<WrittenSourceBaseDto>>(entity.SourcesWrittenInTheCity),
        };
    }

    public async Task<CityDetailDto> CreateAsync(CityCreateRequest request)
    {
        bool exists = await _dbContext.Set<City>().AnyAsync(c => 
            EF.Property<string>(c, "Name") == request.Name);

        if (exists)
        {
            throw new ArgumentException($"An entity with the name '{request.Name}' already exists.");
        }

        var city = new City
        {
            AsciiName = request.AsciiName,
            Name = request.Name,
            GeoNamesId = request.GeoNamesId,
            AlternateNames = request.AlternateNames,
            CountryCode = request.CountryCode,
            Latitude = request.Latitude,
            Longitude = request.Longitude,
        };
        
        _dbContext.Set<City>().Add(city);
        await _dbContext.SaveChangesAsync();

        return new CityDetailDto
        {
            Id = city.Id,
            AsciiName = city.AsciiName,
            Name = city.Name,
            GeoNamesId = city.GeoNamesId,
            AlternateNames = city.AlternateNames,
            CountryCode = city.CountryCode,
            Latitude = city.Latitude,
            Longitude = city.Longitude,
            LocationOf = _mapper.Map<List<OrdinaryPersonBaseDto>>(city.LocationOf),
            BackgroundCityOf = _mapper.Map<List<OrdinaryPersonBaseDto>>(city.BackgroundCityOf),
            BirthPlaceOf = _mapper.Map<List<UnordinaryPersonBaseDto>>(city.BirthPlaceOf),
            DeathPlaceOf = _mapper.Map<List<UnordinaryPersonBaseDto>>(city.DeathPlaceOf),
            SourcesMentioningTheCity = _mapper.Map<List<WrittenSourceBaseDto>>(city.SourcesMentioningTheCity),
            SourcesWrittenInTheCity = _mapper.Map<List<WrittenSourceBaseDto>>(city.SourcesWrittenInTheCity),
        };
    }

    public async Task<CityDetailDto> UpdateAsync(int id, CityUpdateRequest request)
    {
        var city = await _dbContext.Set<City>()
            .Include(op => op.LocationOf)
            .Include(op => op.BackgroundCityOf)
            .Include(op => op.BirthPlaceOf)
            .Include(op => op.DeathPlaceOf)
            .Include(op => op.SourcesMentioningTheCity)
            .Include(op => op.SourcesWrittenInTheCity)
            .FirstOrDefaultAsync(ws => ws.Id == id);

        if (city == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        if (request.AsciiName != null)
            city.AsciiName = request.AsciiName;

        if (request.Name != null)
            city.Name = request.Name;

        if (request.GeoNamesId != null)
            city.GeoNamesId = request.GeoNamesId;

        if (request.AlternateNames != null)
            city.AlternateNames = request.AlternateNames;

        if (request.CountryCode != null)
            city.CountryCode = request.CountryCode;

        if (request.Latitude != null)
            city.Latitude = request.Latitude;

        if (request.Longitude != null)
            city.Longitude = request.Longitude;

        await _dbContext.SaveChangesAsync();

        return new CityDetailDto
        {
            Id = city.Id,
            AsciiName = city.AsciiName,
            Name = city.Name,
            GeoNamesId = city.GeoNamesId,
            AlternateNames = city.AlternateNames,
            CountryCode = city.CountryCode,
            Latitude = city.Latitude,
            Longitude = city.Longitude,
            LocationOf = _mapper.Map<List<OrdinaryPersonBaseDto>>(city.LocationOf),
            BackgroundCityOf = _mapper.Map<List<OrdinaryPersonBaseDto>>(city.BackgroundCityOf),
            BirthPlaceOf = _mapper.Map<List<UnordinaryPersonBaseDto>>(city.BirthPlaceOf),
            DeathPlaceOf = _mapper.Map<List<UnordinaryPersonBaseDto>>(city.DeathPlaceOf),
            SourcesMentioningTheCity = _mapper.Map<List<WrittenSourceBaseDto>>(city.SourcesMentioningTheCity),
            SourcesWrittenInTheCity = _mapper.Map<List<WrittenSourceBaseDto>>(city.SourcesWrittenInTheCity),

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

    public async  Task<IEnumerable<CityFilterResponseDto>> GetAllFilteredAsync(CityFilterDto filter)
    {
        var query = _dbContext.Set<City>()
            .Include(op => op.LocationOf)
            .Include(op => op.BackgroundCityOf)
            .Include(op => op.BirthPlaceOf)
            .Include(op => op.DeathPlaceOf)
            .Include(op => op.SourcesMentioningTheCity)
            .Include(op => op.SourcesWrittenInTheCity)
            .AsQueryable();

        if(filter != null)
        {
            if(filter.Name != null)
            {
                var checkedString = filter.Name.ToLower().Replace(" ", "").Replace("\t", "");
                query = query.Where(e => e.Name.ToLower().Replace(" ", "").
                    Replace("\t", "").Contains(checkedString) || 
                    (e.AsciiName != null && e.AsciiName.ToLower().Replace(" ", "").
                    Replace("\t", "").Contains(checkedString)) ||
                    (e.AlternateNames != null &&
                    e.AlternateNames.Any(a => a.ToLower().Replace(" ", "").Replace("\t", "")
                    .Contains(checkedString))));
            }

            if(filter.LocationOf != null)
            {
                var innerItems = query.Include(op => op.LocationOf).AsEnumerable()
                    .Where(op => (op.LocationOf != null) 
                    && filter.LocationOf.
                        Any(fs => op.LocationOf.Select(s => s.Id).Contains(fs))
                        )
                    .OrderBy(p => p.Id)  // Sort by Id (or other field)
                    .Select(p => new CityFilterResponseDto
                    {
                        Id = p.Id,
                        AsciiName = p.AsciiName,
                        Name = p.Name,
                        AlternateNames = p.AlternateNames,
                        GeoNamesId = p.GeoNamesId,
                        CountryCode = p.CountryCode,
                        Latitude = p.Latitude,
                        Longitude = p.Longitude,
                    })
                    .ToList();

                Console.WriteLine("items", innerItems);

                int totalCount1 = innerItems.Count;

                return innerItems;
            }

            if(filter.BackgroundCityOf != null)
            {
                var innerItems = query.Include(op => op.BackgroundCityOf).AsEnumerable()
                    .Where(op => (op.BackgroundCityOf != null) 
                    && filter.BackgroundCityOf.
                        Any(fs => op.BackgroundCityOf.Select(s => s.Id).Contains(fs))
                        )
                    .OrderBy(p => p.Id)  // Sort by Id (or other field)
                    .Select(p => new CityFilterResponseDto
                    {
                        Id = p.Id,
                        AsciiName = p.AsciiName,
                        Name = p.Name,
                        AlternateNames = p.AlternateNames,
                        GeoNamesId = p.GeoNamesId,
                        CountryCode = p.CountryCode,
                        Latitude = p.Latitude,
                        Longitude = p.Longitude,
                    })
                    .ToList();

                Console.WriteLine("items", innerItems);

                int totalCount1 = innerItems.Count;

                return innerItems;
            }

            if(filter.BirthPlaceOf != null)
            {
                var innerItems = query.Include(op => op.BirthPlaceOf).AsEnumerable()
                    .Where(op => (op.BirthPlaceOf != null) 
                    && filter.BirthPlaceOf.
                        Any(fs => op.BirthPlaceOf.Select(s => s.Id).Contains(fs))
                        )
                    .OrderBy(p => p.Id)  // Sort by Id (or other field)
                    .Select(p => new CityFilterResponseDto
                    {
                        Id = p.Id,
                        AsciiName = p.AsciiName,
                        Name = p.Name,
                        AlternateNames = p.AlternateNames,
                        GeoNamesId = p.GeoNamesId,
                        CountryCode = p.CountryCode,
                        Latitude = p.Latitude,
                        Longitude = p.Longitude,
                    })
                    .ToList();

                Console.WriteLine("items", innerItems);

                int totalCount1 = innerItems.Count;

                return innerItems;
            }

            if(filter.DeathPlaceOf != null)
            {
                var innerItems = query.Include(op => op.DeathPlaceOf).AsEnumerable()
                    .Where(op => (op.DeathPlaceOf != null) 
                    && filter.DeathPlaceOf.
                        Any(fs => op.DeathPlaceOf.Select(s => s.Id).Contains(fs))
                        )
                    .OrderBy(p => p.Id)  // Sort by Id (or other field)
                    .Select(p => new CityFilterResponseDto
                    {
                        Id = p.Id,
                        AsciiName = p.AsciiName,
                        Name = p.Name,
                        AlternateNames = p.AlternateNames,
                        GeoNamesId = p.GeoNamesId,
                        CountryCode = p.CountryCode,
                        Latitude = p.Latitude,
                        Longitude = p.Longitude,
                    })
                    .ToList();

                Console.WriteLine("items", innerItems);

                int totalCount1 = innerItems.Count;

                return innerItems;
            }

            if(filter.SourcesMentioningTheCity != null)
            {
                var innerItems = query.Include(op => op.SourcesMentioningTheCity).AsEnumerable()
                    .Where(op => (op.SourcesMentioningTheCity != null) 
                    && filter.SourcesMentioningTheCity.
                        Any(fs => op.SourcesMentioningTheCity.Select(s => s.Id).Contains(fs))
                        )
                    .OrderBy(p => p.Id)  // Sort by Id (or other field)
                    .Select(p => new CityFilterResponseDto
                    {
                        Id = p.Id,
                        AsciiName = p.AsciiName,
                        Name = p.Name,
                        AlternateNames = p.AlternateNames,
                        GeoNamesId = p.GeoNamesId,
                        CountryCode = p.CountryCode,
                        Latitude = p.Latitude,
                        Longitude = p.Longitude,
                    })
                    .ToList();

                Console.WriteLine("items", innerItems);

                int totalCount1 = innerItems.Count;

                return innerItems;
            }

            if(filter.SourcesWrittenInTheCity != null)
            {
                var innerItems = query.Include(op => op.SourcesWrittenInTheCity).AsEnumerable()
                    .Where(op => (op.SourcesWrittenInTheCity != null) 
                    && filter.SourcesWrittenInTheCity.
                        Any(fs => op.SourcesWrittenInTheCity.Select(s => s.Id).Contains(fs))
                        )
                    .OrderBy(p => p.Id)  // Sort by Id (or other field)
                    .Select(p => new CityFilterResponseDto
                    {
                        Id = p.Id,
                        AsciiName = p.AsciiName,
                        Name = p.Name,
                        AlternateNames = p.AlternateNames,
                        GeoNamesId = p.GeoNamesId,
                        CountryCode = p.CountryCode,
                        Latitude = p.Latitude,
                        Longitude = p.Longitude,
                    })
                    .ToList();

                Console.WriteLine("items", innerItems);

                int totalCount1 = innerItems.Count;

                return innerItems;
            }
        }
        
        return await query
            .Select(p => new CityFilterResponseDto
                {
                    Id = p.Id,
                    AsciiName = p.AsciiName,
                    Name = p.Name,
                    AlternateNames = p.AlternateNames,
                    GeoNamesId = p.GeoNamesId,
                    CountryCode = p.CountryCode,
                    Latitude = p.Latitude,
                    Longitude = p.Longitude,
                })
            .OrderBy(p => p.Id)  // Sort by Id (or other field)
            .ToListAsync();
    }

    public async Task<IEnumerable<CityMapDto>> GetAllForMapAsync()
    {
        return await _dbContext.Set<City>()
            .Select(e => new CityMapDto
            {
                Id = e.Id,
                AsciiName = e.AsciiName,
                Name = e.Name,
                Latitude = e.Latitude,
                Longitude = e.Longitude,
                NumberOfLocationOf = (e.LocationOf != null) ? e.LocationOf.Count : 0,
                NumberOfBackgroundCityOf = (e.BackgroundCityOf != null) ? 
                    e.BackgroundCityOf.Count : 0,
                NumberOfBirthPlaceOf = (e.BirthPlaceOf != null) ? e.BirthPlaceOf.Count : 0,
                NumberOfDeathPlaceOf = (e.DeathPlaceOf != null) ? e.DeathPlaceOf.Count : 0,
                NumberOfSourcesMentioningTheCity = (e.SourcesMentioningTheCity != null) 
                    ? e.SourcesMentioningTheCity.Count : 0,
                NumberOfSourcesWrittenInTheCity = (e.SourcesWrittenInTheCity != null) 
                    ? e.SourcesWrittenInTheCity.Count : 0,

            })
            .OrderBy(e => e.Id)
            .ToListAsync();
    }

    // public int Id { get; set; }
    // public string AsciiName { get; set; }
    // public double? Latitude {get; set;}
    // public double? Longitude {get; set;}
    // // OrdinaryPerson Ids
    // public int? NumberOfLocationOf {get; set;}
    // public int? NumberOfBackgroundCityOf {get; set;}
    // // UnordinaryPerson Ids
    // public int? NumberOfBirthPlaceOf {get; set;}
    // public int? NumberOfDeathPlaceOf {get; set;}
    // // WrittenSource Ids
    // public int? NumberOfSourcesMentioningTheCity {get; set;}
    // public int? NumberOfSourcesWrittenInTheCity {get; set;}
}
