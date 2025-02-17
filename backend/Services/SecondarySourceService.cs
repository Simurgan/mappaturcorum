using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;

namespace Mappa.Services;

public class SecondarySourceService : IComplexEntityService<SecondarySource, 
    SecondarySourceGeneralDto, SecondarySourceDetailDto, SecondarySourceCreateRequest, 
    SecondarySourceUpdateRequest, SecondarySourceFilterDto, SecondarySourceFilterResponseDto,
    SecondarySourceGraphDto>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public SecondarySourceService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<SecondarySourceGeneralDto>> GetAllAsync()
    {
        return await _dbContext.Set<SecondarySource>()
            .Include(ss => ss.Type )
            .Select(e => new SecondarySourceGeneralDto
            {
                Id = e.Id,
                AlternateNames = e.AlternateNames,
                Author = e.Author,
                YearWritten = e.YearWritten,
                University = e.University,
                Type = _mapper.Map<TypeDto>(e.Type)
            })
            .OrderBy(ss => ss.Id)
            .ToListAsync();
    }

    public async Task<SecondarySourceDetailDto> GetByIdAsync(int id)
    {
        var entity = await _dbContext.Set<SecondarySource>()
            .Include(ss => ss.Type)
            .Include(ss => ss.Language)
            .Include(ss => ss.TranslatedLanguages)
            .FirstOrDefaultAsync(ss => ss.Id == id);
        
        if (entity == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        return new SecondarySourceDetailDto
        {
            Id = entity.Id,
            AlternateNames = entity.AlternateNames,
            Author = entity.Author,
            YearWritten = entity.YearWritten,
            University = entity.University,
            Type = _mapper.Map<TypeDto>(entity.Type),
            Topic = entity.Topic,
            BibliographyInformation = entity.BibliographyInformation,
            OtherInformation = entity.OtherInformation,
            Language = _mapper.Map<LanguageDto>(entity.Language),
            TranslatedLanguages = _mapper.Map<List<LanguageDto>>(entity.TranslatedLanguages),
        };
    }

    public async Task<SecondarySourceDetailDto> CreateAsync(SecondarySourceCreateRequest request)
    {
        if ((request.AlternateNames == null) || (request.AlternateNames.Count == 0) )
            throw new ArgumentException("Alternate names is not provided");

        bool exists = await _dbContext.Set<SecondarySource>()
            .AnyAsync(ws => ws.AlternateNames.Any(name => request.AlternateNames.Contains(name)));

        if (exists)
            throw new ArgumentException("A secondary source with one or more of the provided alternate names already exists.");

        var entity = new SecondarySource
        {
            AlternateNames = request.AlternateNames,
            Author = request.Author,
            YearWritten = request.YearWritten,
            University = request.University,
            Topic = request.Topic,
            BibliographyInformation = request.BibliographyInformation,
            OtherInformation = request.OtherInformation
        };

        // Check if Type exists
        if(request.Type != null)
        {
            Entities.Type? type = await _dbContext.Set<Entities.Type>().FirstOrDefaultAsync(e => e.Name == request.Type);
            if (type == null)
                throw new ArgumentException($"Type with name {request.Type} not found.");
            entity.Type = type;
        }

        // Check if Language exists
        if(request.Language != null)
        {
            Language? language = await _dbContext.Set<Language>().FirstOrDefaultAsync(e => e.Name == request.Language);
            if (language == null)
                throw new ArgumentException($"Language with name {request.Language} not found.");
            entity.Language = language;
        }

        if (request.TranslatedLanguages != null)
        {
                List<Language>? translatedLanguages = await _dbContext.Set<Language>()
                .Where(l => request.TranslatedLanguages.Contains(l.Name))
                .ToListAsync();

            if (translatedLanguages.Count != request.TranslatedLanguages.Count)
                throw new ArgumentException("One or more provided Translated Languages are invalid.");
            entity.TranslatedLanguages = translatedLanguages;
        }

        
        _dbContext.Set<SecondarySource>().Add(entity);
        await _dbContext.SaveChangesAsync();

        return new SecondarySourceDetailDto
        {
            Id = entity.Id,
            AlternateNames = entity.AlternateNames,
            Author = entity.Author,
            YearWritten = entity.YearWritten,
            University = entity.University,
            Type = _mapper.Map<TypeDto>(entity.Type),
            Topic = entity.Topic,
            BibliographyInformation = entity.BibliographyInformation,
            OtherInformation = entity.OtherInformation,
            Language = _mapper.Map<LanguageDto>(entity.Language),
            TranslatedLanguages = _mapper.Map<List<LanguageDto>>(entity.TranslatedLanguages),    
        };
    }

    public async Task<SecondarySourceDetailDto> UpdateAsync(int id, SecondarySourceUpdateRequest request)
    {
        var secondarySource = await _dbContext.Set<SecondarySource>()
            .Include(ws => ws.Type)
            .Include(ws => ws.Language)
            .Include(ws => ws.TranslatedLanguages)
            .FirstOrDefaultAsync(ws => ws.Id == id);

        if (secondarySource == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        // Only update fields if they are not null
        if (request.AlternateNames != null)
            secondarySource.AlternateNames = request.AlternateNames;

        if (request.Author != null)
            secondarySource.Author = request.Author;

        if (request.YearWritten != null)  // DateOnly
            secondarySource.YearWritten = request.YearWritten;

        if (request.University != null)
            secondarySource.University = request.University;

        // Type update only if a valid type is provided
        if (request.Type != null)
        {
            var type = await _dbContext.Set<Entities.Type>().FirstOrDefaultAsync(
                e => e.Name == request.Type);
            if (type == null)
                throw new ArgumentException($"Type with name {request.Type} not found.");
            secondarySource.Type = type;
        }

        if (request.Topic != null)
            secondarySource.Topic = request.Topic;

        if (request.BibliographyInformation != null)
            secondarySource.BibliographyInformation = request.BibliographyInformation;

        if (request.OtherInformation != null)
            secondarySource.OtherInformation = request.OtherInformation;

        // Language update only if a valid language is provided
        if (request.Language != null)
        {
            var language = await _dbContext.Set<Language>().FirstOrDefaultAsync(
                e => e.Name == request.Language);
            if (language == null)
                throw new ArgumentException($"Language with name {request.Language} not found.");
            secondarySource.Language = language;
        }

        // Update TranslatedLanguages (List of LanguageDto)
        if (request.TranslatedLanguages != null)
        {
            var translatedLanguages = await _dbContext.Set<Language>()
                .Where(l => request.TranslatedLanguages.Contains(l.Name))
                .ToListAsync();

            if (translatedLanguages.Count != request.TranslatedLanguages.Count)
                throw new ArgumentException("One or more provided Translated Language IDs are invalid.");

            secondarySource.TranslatedLanguages = translatedLanguages;
        }

        await _dbContext.SaveChangesAsync();

        return new SecondarySourceDetailDto
        {
            Id = secondarySource.Id,
            AlternateNames = secondarySource.AlternateNames,
            Author = secondarySource.Author,
            YearWritten = secondarySource.YearWritten,
            University = secondarySource.University,
            Type = _mapper.Map<TypeDto>(secondarySource.Type),
            Topic = secondarySource.Topic,
            BibliographyInformation = secondarySource.BibliographyInformation,
            OtherInformation = secondarySource.OtherInformation,
            Language = _mapper.Map<LanguageDto>(secondarySource.Language),
            TranslatedLanguages = _mapper.Map<List<LanguageDto>>(secondarySource.TranslatedLanguages),    
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var secondarySource = await _dbContext.Set<SecondarySource>().FindAsync(id);
        if (secondarySource == null)
            return false;

        _dbContext.Set<SecondarySource>().Remove(secondarySource);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public Task<PaginationResponse<SecondarySourceFilterResponseDto>> GetPageAsync(int pageNumber, int pageSize, SecondarySourceFilterDto filter)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SecondarySourceFilterResponseDto>> GetAllFilteredAsync(SecondarySourceFilterDto filter)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<SecondarySourceGraphDto>> GetAllForGraphAsync()
    {
        throw new NotImplementedException();
    }
}
