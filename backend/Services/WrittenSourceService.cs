using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks.Dataflow;

namespace Mappa.Services;

public class WrittenSourceService : IComplexEntityService<WrittenSource, 
    WrittenSourceGeneralDto, WrittenSourceDetailDto, WrittenSourceCreateRequest, 
    WrittenSourceUpdateRequest, WrittenSourceFilterDto, WrittenSourceFilterResponseDto,
    WrittenSourceGraphDto>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public WrittenSourceService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<WrittenSourceGeneralDto>> GetAllAsync()
    {
        return await _dbContext.Set<WrittenSource>()
            .Include(ws => ws.Genre)
            .Include(ws => ws.Language)
            .Select(e => new WrittenSourceGeneralDto
            {
                Id = e.Id,
                AlternateNames = e.AlternateNames,
                Author = e.Author,
                YearWritten = e.YearWritten,
                Genre = _mapper.Map<GenreDto>(e.Genre),
                Language = _mapper.Map<LanguageDto>(e.Language)
            })
            .OrderBy(ws => ws.Id)
            .ToListAsync();
    }

    public async Task<WrittenSourceDetailDto> GetByIdAsync(int id)
    {
        var entity = await _dbContext.Set<WrittenSource>()
            .Include(ws => ws.Genre)
            .Include(ws => ws.Language)
            .Include(ws => ws.TranslatedLanguages)
            .Include(ws => ws.CitiesMentionedByTheSource)
            .Include(ws => ws.CitiesWhereSourcesAreWritten)
            .Include(ws => ws.OrdinaryPersons)
            .Include(ws => ws.UnordinaryPersons)
            .FirstOrDefaultAsync(ws => ws.Id == id);
        
        if (entity == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        return new WrittenSourceDetailDto
        {
            Id = entity.Id,
            AlternateNames = entity.AlternateNames,
            Author = entity.Author,
            YearWritten = entity.YearWritten,
            Genre = _mapper.Map<GenreDto>(entity.Genre),
            Language = _mapper.Map<LanguageDto>(entity.Language),
            KnownCopies = entity.KnownCopies,
            SurvivedCopies = entity.SurvivedCopies,
            LibraryInformation = entity.LibraryInformation,
            OtherInformation = entity.OtherInformation,
            RemarkableWorksOnTheBook = entity.RemarkableWorksOnTheBook,
            Image = entity.Image,
            TranslatedLanguages = _mapper.Map<List<LanguageDto>>(entity.TranslatedLanguages),
            CitiesMentionedByTheSource = _mapper.Map<List<CityBaseDto>>(entity.CitiesMentionedByTheSource),
            CitiesWhereSourcesAreWritten = _mapper.Map<List<CityBaseDto>>(entity.CitiesWhereSourcesAreWritten),
            OrdinaryPersons = _mapper.Map<List<OrdinaryPersonBaseDto>>(entity.OrdinaryPersons),
            UnordinaryPersons = _mapper.Map<List<UnordinaryPersonBaseDto>>(entity.UnordinaryPersons),
        };
    }

    public async Task<WrittenSourceDetailDto> CreateAsync(WrittenSourceCreateRequest request)
    {
        if ((request.AlternateNames == null) || (request.AlternateNames.Count == 0) )
            throw new ArgumentException("Alternate names is not provided");

        bool exists = await _dbContext.Set<WrittenSource>()
            .AnyAsync(ws => ws.AlternateNames.Any(name => request.AlternateNames.Contains(name)));

        if (exists)
            throw new ArgumentException("A written source with one or more of the provided alternate names already exists.");

        var writtenSource = new WrittenSource
        {
            AlternateNames = request.AlternateNames,
            Author = request.Author,
            YearWritten = request.YearWritten,
            KnownCopies = request.KnownCopies,
            SurvivedCopies = request.SurvivedCopies,
            LibraryInformation = request.LibraryInformation,
            OtherInformation = request.OtherInformation,
            RemarkableWorksOnTheBook = request.RemarkableWorksOnTheBook,
            Image = request.Image,
        };

        // Check if Genre exists
        if(request.Genre != null)
        {
            var genre = await _dbContext.Set<Genre>().FirstOrDefaultAsync(e => e.Name == request.Genre );
            if (genre == null)
                throw new ArgumentException($"Genre with name {request.Genre} not found.");
            writtenSource.Genre = genre;
        }

        // Check if Language exists
        if(request.Language != null)
        {
            var language = await _dbContext.Set<Language>().FirstOrDefaultAsync(e => e.Name == request.Language );
            if (language == null)
                throw new ArgumentException($"Language with name {request.Language} not found.");
            writtenSource.Language = language;
        }

        // Update TranslatedLanguages (List of LanguageDto)
        if (request.TranslatedLanguages != null)
        {
            var translatedLanguages = await _dbContext.Set<Language>()
                .Where(l => request.TranslatedLanguages.Contains(l.Name))
                .ToListAsync();

            if (translatedLanguages.Count != request.TranslatedLanguages.Count)
                throw new ArgumentException("One or more provided Translated Language are invalid.");
            writtenSource.TranslatedLanguages = translatedLanguages;
        }

        // Update CitiesMentionedByTheSource (List of CityBaseDto)
        if (request.CitiesMentionedByTheSource != null)
        {
            var mentionedCities = await _dbContext.Set<City>()
                .Where(c => request.CitiesMentionedByTheSource.Contains(c.AsciiName))
                .ToListAsync();

            if (mentionedCities.Count != request.CitiesMentionedByTheSource.Count)
                throw new ArgumentException("One or more provided CitiesMentionedByTheSource are invalid.");
            writtenSource.CitiesMentionedByTheSource = mentionedCities;
        }

        // Update CitiesWhereSourcesAreWritten (List of CityBaseDto)
        if (request.CitiesWhereSourcesAreWritten != null)
        {
            var writtenCities = await _dbContext.Set<City>()
                .Where(c => request.CitiesWhereSourcesAreWritten.Contains(c.AsciiName))
                .ToListAsync();

            if (writtenCities.Count != request.CitiesWhereSourcesAreWritten.Count)
                throw new ArgumentException("One or more provided CitiesWhereSourcesAreWritten IDs are invalid.");
            writtenSource.CitiesWhereSourcesAreWritten = writtenCities;
        }
        
        _dbContext.Set<WrittenSource>().Add(writtenSource);
        await _dbContext.SaveChangesAsync();

        return new WrittenSourceDetailDto
        {
            Id = writtenSource.Id,
            AlternateNames = writtenSource.AlternateNames,
            Author = writtenSource.Author,
            YearWritten = writtenSource.YearWritten,
            Genre = _mapper.Map<GenreDto>(writtenSource.Genre),
            Language = _mapper.Map<LanguageDto>(writtenSource.Language),
            KnownCopies = writtenSource.KnownCopies,
            SurvivedCopies = writtenSource.SurvivedCopies,
            LibraryInformation = writtenSource.LibraryInformation,
            OtherInformation = writtenSource.OtherInformation,
            RemarkableWorksOnTheBook = writtenSource.RemarkableWorksOnTheBook,
            Image = writtenSource.Image,
            TranslatedLanguages = _mapper.Map<List<LanguageDto>>(writtenSource.TranslatedLanguages),
            CitiesMentionedByTheSource = _mapper.Map<List<CityBaseDto>>(writtenSource.CitiesMentionedByTheSource),
            CitiesWhereSourcesAreWritten = _mapper.Map<List<CityBaseDto>>(writtenSource.CitiesWhereSourcesAreWritten)
        };
    }

    public async Task<WrittenSourceDetailDto> UpdateAsync(int id, WrittenSourceUpdateRequest request)
    {
        var writtenSource = await _dbContext.Set<WrittenSource>()
            .Include(ws => ws.Genre)
            .Include(ws => ws.Language)
            .Include(ws => ws.TranslatedLanguages)
            .Include(ws => ws.CitiesMentionedByTheSource)
            .Include(ws => ws.CitiesWhereSourcesAreWritten)
            .FirstOrDefaultAsync(ws => ws.Id == id);

        if (writtenSource == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        // Only update fields if they are not null
        if (request.AlternateNames != null)
            writtenSource.AlternateNames = request.AlternateNames;

        if (request.Author != null)
            writtenSource.Author = request.Author;

        if ((request.YearWritten != null) && (request.YearWritten.Count != 0) )  
            writtenSource.YearWritten = request.YearWritten;

        // Genre update only if a valid genre is provided
        if (request.Genre != null)
        {
            var genre = await _dbContext.Set<Genre>().FirstOrDefaultAsync(e => e.Name == request.Genre);
            if (genre == null)
                throw new ArgumentException($"Genre with name {request.Genre} not found.");
            writtenSource.Genre = genre;
        }

        // Language update only if a valid language is provided
        if (request.Language != null)
        {
            var language = await _dbContext.Set<Language>().FirstOrDefaultAsync(e => e.Name == request.Language);
            if (language == null)
                throw new ArgumentException($"Language with name {request.Language} not found.");
            writtenSource.Language = language;
        }

        // Only update fields if they are provided in the request
        if (request.KnownCopies != null)
            writtenSource.KnownCopies = request.KnownCopies;

        if (request.SurvivedCopies != null)
            writtenSource.SurvivedCopies = request.SurvivedCopies;

        if (request.LibraryInformation != null)
            writtenSource.LibraryInformation = request.LibraryInformation;

        if (request.OtherInformation != null)
            writtenSource.OtherInformation = request.OtherInformation;

        if (request.RemarkableWorksOnTheBook != null)
            writtenSource.RemarkableWorksOnTheBook = request.RemarkableWorksOnTheBook;

        if (request.Image != null)
            writtenSource.Image = request.Image;

        // Update TranslatedLanguages (List of LanguageDto)
        if (request.TranslatedLanguages != null)
        {
            var translatedLanguages = await _dbContext.Set<Language>()
                .Where(l => request.TranslatedLanguages.Contains(l.Name))
                .ToListAsync();

            if (translatedLanguages.Count != request.TranslatedLanguages.Count)
                throw new ArgumentException("One or more provided Translated Languages are invalid.");

            writtenSource.TranslatedLanguages = translatedLanguages;
        }

        // Update CitiesMentionedByTheSource (List of CityBaseDto)
        if (request.CitiesMentionedByTheSource != null)
        {
            var mentionedCities = await _dbContext.Set<City>()
                .Where(c => request.CitiesMentionedByTheSource.Contains(c.AsciiName))
                .ToListAsync();

            if (mentionedCities.Count != request.CitiesMentionedByTheSource.Count)
                throw new ArgumentException("One or more provided CitiesMentionedByTheSource are invalid.");

            writtenSource.CitiesMentionedByTheSource = mentionedCities;
        }

        // Update CitiesWhereSourcesAreWritten (List of CityBaseDto)
        if (request.CitiesWhereSourcesAreWritten != null)
        {
            var writtenCities = await _dbContext.Set<City>()
                .Where(c => request.CitiesWhereSourcesAreWritten.Contains(c.AsciiName))
                .ToListAsync();

            if (writtenCities.Count != request.CitiesWhereSourcesAreWritten.Count)
                throw new ArgumentException("One or more provided CitiesWhereSourcesAreWritten are invalid.");

            writtenSource.CitiesWhereSourcesAreWritten = writtenCities;
        }

        await _dbContext.SaveChangesAsync();

        return new WrittenSourceDetailDto
        {
            Id = writtenSource.Id,
            AlternateNames = writtenSource.AlternateNames,
            Author = writtenSource.Author,
            YearWritten = writtenSource.YearWritten,
            Genre = _mapper.Map<GenreDto>(writtenSource.Genre),
            Language = _mapper.Map<LanguageDto>(writtenSource.Language),
            KnownCopies = writtenSource.KnownCopies,
            SurvivedCopies = writtenSource.SurvivedCopies,
            LibraryInformation = writtenSource.LibraryInformation,
            OtherInformation = writtenSource.OtherInformation,
            RemarkableWorksOnTheBook = writtenSource.RemarkableWorksOnTheBook,
            Image = writtenSource.Image,
            TranslatedLanguages = _mapper.Map<List<LanguageDto>>(writtenSource.TranslatedLanguages),
            CitiesMentionedByTheSource = _mapper.Map<List<CityBaseDto>>(writtenSource.CitiesMentionedByTheSource),
            CitiesWhereSourcesAreWritten = _mapper.Map<List<CityBaseDto>>(writtenSource.CitiesWhereSourcesAreWritten)
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var writtenSource = await _dbContext.Set<WrittenSource>().FindAsync(id);
        if (writtenSource == null)
            return false;

        _dbContext.Set<WrittenSource>().Remove(writtenSource);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<PaginationResponse<WrittenSourceFilterResponseDto>> GetPageAsync(
        int pageNumber, int pageSize, WrittenSourceFilterDto filter)
    {
        var query = _dbContext.Set<WrittenSource>()
            .Include(op => op.Genre)
            .Include(op => op.Language)
            .Include(op => op.OrdinaryPersons)
            .Include(op => op.UnordinaryPersons)
            .AsQueryable();

        if(filter != null)
        {
            if(filter.Name != null)
            {
                var checkedString = filter.Name.ToLower().Replace(" ", "").Replace("\t", "");
                query = query.Where(e =>  (e.AlternateNames != null)
                    && e.AlternateNames.Any(a => a.ToLower().Replace(" ", "").Replace("\t", "").
                    Contains(checkedString)));
            }

            if(filter.Genre != null) 
                query = query.Where(op => op.Genre != null && (op.Genre.Id == filter.Genre));

            if(filter.YearWritten != null && filter.YearWritten.Count > 0)
            {
                if(filter.YearWritten.Count == 1)
                    query = query.Where(op => op.ProbableYearWritten != null && 
                        (op.ProbableYearWritten >= filter.YearWritten[0]));
                else // For case 2, further elements are ignored
                    query = query.Where(op => op.ProbableYearWritten != null && 
                        (op.ProbableYearWritten >= filter.YearWritten[0]) && 
                        (op.ProbableYearWritten <= filter.YearWritten[1]));
            }

            if(filter.Author != null)
            {
                var checkedString = filter.Author.ToLower().Replace(" ", "").Replace(" ","");
                query = query.Where(op => op.Author != null && (op.Author.ToLower()
                    .Replace(" ", "").Replace(" ","").Contains(checkedString)));
            }
                
            
            if(filter.Language != null)
                query = query.Where(op => op.Language != null && (op.Language.Id == filter.Language));
        
            if(filter.OrdinaryPersons != null)
            {
                var innerItems = query.Include(op => op.OrdinaryPersons).AsEnumerable()
                    .Where(op => (op.OrdinaryPersons != null) 
                    && filter.OrdinaryPersons.
                        Any(fs => op.OrdinaryPersons.Select(s => s.Id).Contains(fs))
                        )
                    .OrderBy(p => p.Id)  // Sort by Id (or other field)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => new WrittenSourceFilterResponseDto
                    {
                        Id = p.Id,
                        AlternateNames = p.AlternateNames,
                        Author = p.Author,
                        YearWritten = p.YearWritten,
                        Genre = _mapper.Map<GenreDto>(p.Genre),
                        Language = _mapper.Map<LanguageDto>(p.Language),
                    })
                    .ToList();

                Console.WriteLine("items", innerItems);

                int totalCount1 = innerItems.Count;

                return new PaginationResponse<WrittenSourceFilterResponseDto>
                {
                    Data = innerItems,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount1,
                };
            }
        
            if(filter.UnordinaryPersons != null)
            {
                var innerItems = query.Include(op => op.UnordinaryPersons).AsEnumerable()
                    .Where(op => (op.UnordinaryPersons != null) 
                    && filter.UnordinaryPersons.
                        Any(fs => op.UnordinaryPersons.Select(s => s.Id).Contains(fs))
                        )
                    .OrderBy(p => p.Id)  // Sort by Id (or other field)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => new WrittenSourceFilterResponseDto
                    {
                        Id = p.Id,
                        AlternateNames = p.AlternateNames,
                        Author = p.Author,
                        YearWritten = p.YearWritten,
                        Genre = _mapper.Map<GenreDto>(p.Genre),
                        Language = _mapper.Map<LanguageDto>(p.Language),
                    })
                    .ToList();

                Console.WriteLine("items", innerItems);

                int totalCount1 = innerItems.Count;

                return new PaginationResponse<WrittenSourceFilterResponseDto>
                {
                    Data = innerItems,
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalCount = totalCount1,
                };
            }
        }
        else
        {
            throw new ArgumentException("Filter is not provided.");
        }

        int totalCount = await query.CountAsync();

        var items = await query
            .OrderBy(p => p.Id)  // Sort by Id (or other field)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new WrittenSourceFilterResponseDto
            {
                Id = p.Id,
                AlternateNames = p.AlternateNames,
                Author = p.Author,
                YearWritten = p.YearWritten,
                Genre = _mapper.Map<GenreDto>(p.Genre),
                Language = _mapper.Map<LanguageDto>(p.Language),
            })
            .ToListAsync();

        return new PaginationResponse<WrittenSourceFilterResponseDto>
        {
            Data = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
        };
    }

    public Task<IEnumerable<WrittenSourceGraphDto>> GetAllForGraphAsync()
    {
        throw new NotImplementedException();
    }
}
