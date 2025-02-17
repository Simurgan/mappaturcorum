using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace Mappa.Services;

public class UnordinaryPersonService : IComplexEntityService<UnordinaryPerson, 
    UnordinaryPersonGeneralDto, UnordinaryPersonDetailDto, UnordinaryPersonCreateRequest, 
    UnordinaryPersonUpdateRequest, UnordinaryPersonFilterDto, UnordinaryPersonFilterResponseDto,
    UnordinaryPersonGraphDto>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public UnordinaryPersonService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<UnordinaryPersonGeneralDto>> GetAllAsync()
    {
        return await _dbContext.Set<UnordinaryPerson>()
            .Include(up => up.Religion)
            .Include(up => up.Ethnicity)
            .Include(up => up.DeathPlace)
            .Include(up => up.InteractionsWithOrdinary)
            .Select(e => new UnordinaryPersonGeneralDto
            {
                Id = e.Id,
                Name = e.Name,
                Religion = _mapper.Map<ReligionDto>(e.Religion),
                Ethnicity = _mapper.Map<EthnicityDto>(e.Ethnicity),
                DeathYear = e.DeathYear,
                DeathPlace = _mapper.Map<CityBaseDto>(e.DeathPlace),
                InteractionsWithOrdinary = _mapper.Map<List<OrdinaryPersonBaseDto>>(e.InteractionsWithOrdinary),
            })
            .OrderBy(up => up.Id)
            .ToListAsync();
    }

    public async Task<UnordinaryPersonDetailDto> GetByIdAsync(int id)
    {
        var entity = await _dbContext.Set<UnordinaryPerson>()
            .Include(ws => ws.Religion)
            .Include(ws => ws.Ethnicity)
            .Include(ws => ws.DeathPlace)
            .Include(ws => ws.InteractionsWithOrdinary)
            .Include(ws => ws.FormerReligion)
            .Include(ws => ws.Profession)
            .Include(ws => ws.Gender)
            .Include(ws => ws.InteractionsWithUnordinaryA)
            .Include(ws => ws.Sources)
            .FirstOrDefaultAsync(ws => ws.Id == id);
        
        if (entity == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        return new UnordinaryPersonDetailDto
        {
            Id = entity.Id,
            Name = entity.Name,
            Religion = _mapper.Map<ReligionDto>(entity.Religion),
            Ethnicity = _mapper.Map<EthnicityDto>(entity.Ethnicity),
            DeathYear = entity.DeathYear,
            DeathPlace = _mapper.Map<CityBaseDto>(entity.DeathPlace),
            InteractionsWithOrdinary = _mapper.Map<List<OrdinaryPersonBaseDto>>(entity.InteractionsWithOrdinary),
            AlternateName = entity.AlternateName,
            BirthYear = entity.BirthYear,
            ProbableBirthYear = entity.ProbableBirthYear,
            ProbableDeathYear = entity.ProbableDeathYear,
            Description = entity.Description,
            FormerReligion = _mapper.Map<List<ReligionDto>>(entity.FormerReligion),
            Profession = _mapper.Map<ProfessionDto>(entity.Profession),
            Gender = _mapper.Map<GenderDto>(entity.Gender),
            BirthPlace = _mapper.Map<CityBaseDto>(entity.BirthPlace),
            InteractionsWithUnordinaryA = _mapper.Map<List<UnordinaryPersonBaseDto>>(entity.InteractionsWithUnordinaryA),
            InteractionsWithUnordinaryB = _mapper.Map<List<UnordinaryPersonBaseDto>>(entity.InteractionsWithUnordinaryB),
            Sources = _mapper.Map<List<WrittenSourceBaseDto>>(entity.Sources),
        };
    }

    public async Task<UnordinaryPersonDetailDto> CreateAsync(UnordinaryPersonCreateRequest request)
    {
        var unordinaryPerson = new UnordinaryPerson
        {
            Name = request.Name,
            AlternateName = request.AlternateName,
            BirthYear = request.BirthYear,
            DeathYear = request.DeathYear,
            ProbableBirthYear = request.ProbableBirthYear,
            ProbableDeathYear = request.ProbableDeathYear,
            Description = request.Description,
            // Religion = _mapper.Map<Religion>(request.Religion),
            // Ethnicity = _mapper.Map<Ethnicity>(request.Ethnicity),
            // FormerReligion = _mapper.Map<List<Religion>>(request.FormerReligion),
            // Profession = _mapper.Map<Profession>(request.Profession),
            // Gender = _mapper.Map<Gender>(request.Gender),
            // Sources = _mapper.Map<List<WrittenSource>>(request.Sources),
            // InteractionsWithOrdinary = _mapper.Map<List<OrdinaryPerson>>(request.InteractionsWithOrdinary),
            // InteractionsWithUnordinaryA = _mapper.Map<List<UnordinaryPerson>>(request.InteractionsWithUnordinaryA),
            // BirthPlace = _mapper.Map<City>(request.BirthPlace),
            // DeathPlace = _mapper.Map<City>(request.DeathPlace),
        };

        // Check if Religion exists
        if(request.Religion != null)
        {
            var religion = await _dbContext.Set<Religion>().FirstOrDefaultAsync(e => e.Name == request.Religion);
            if (religion == null)
                throw new ArgumentException($"Religion with name {request.Religion} not found.");
            unordinaryPerson.Religion = religion;
        }

        // Check if Ethnicity exists
        if(request.Ethnicity != null)
        {
            var ethnicity = await _dbContext.Set<Ethnicity>().FirstOrDefaultAsync(e => e.Name == request.Ethnicity);
            if (ethnicity == null)
                throw new ArgumentException($"Ethnicity with name {request.Ethnicity} not found.");
            unordinaryPerson.Ethnicity = ethnicity;
        }

        // Check if DeathPlace exists
        if(request.DeathPlace != null)
        {
            var deathPlace = await _dbContext.Set<City>().FirstOrDefaultAsync(e => e.AsciiName == request.DeathPlace);
            if (deathPlace == null)
                throw new ArgumentException($"DeathPlace with name {request.DeathPlace} not found.");
            unordinaryPerson.DeathPlace = deathPlace;
        }

        // Check if InteractionsWithOrdinary elements exists
        if (request.InteractionsWithOrdinary != null)
        {
            var interactionsWithOrdinary = await _dbContext.Set<OrdinaryPerson>()
                .Where(c => request.InteractionsWithOrdinary.Contains(c.Id))
                .ToListAsync();

            if (interactionsWithOrdinary.Count != request.InteractionsWithOrdinary.Count)
                throw new ArgumentException("One or more provided InteractionsWithOrdinary are invalid.");
            unordinaryPerson.InteractionsWithOrdinary = interactionsWithOrdinary;
        }

        // Check if FormerReligion elements exists
        if (request.FormerReligion != null)
        {
            var formerReligion = await _dbContext.Set<Religion>()
                .Where(c => request.FormerReligion.Contains(c.Name))
                .ToListAsync();

            if (formerReligion.Count != request.FormerReligion.Count)
                throw new ArgumentException("One or more provided Formerreligion are invalid.");
            unordinaryPerson.FormerReligion = formerReligion;
        }

        // Check if Profession exists
        if(request.Profession != null)
        {
            var profession = await _dbContext.Set<Profession>().FirstOrDefaultAsync(e => e.Name == request.Profession);
            if (profession == null)
                throw new ArgumentException($"Profession with name {request.Profession} not found.");
            unordinaryPerson.Profession = profession;
        }

        // Check if Gender exists
        if(request.Gender != null)
        {
            var gender = await _dbContext.Set<Gender>().FirstOrDefaultAsync(e => e.Name == request.Gender);
            if (gender == null)
                throw new ArgumentException($"Gender with name {request.Gender} not found.");
            unordinaryPerson.Gender = gender;
        }

        // Check if BirthPlace exists
        if(request.BirthPlace != null)
        {
            var birthPlace = await _dbContext.Set<City>().FirstOrDefaultAsync(e => e.AsciiName == request.BirthPlace);
            if (birthPlace == null)
                throw new ArgumentException($"BirthPlace with name {request.BirthPlace} not found.");
            unordinaryPerson.BirthPlace = birthPlace;
        }

        // Check if InteractionsWithUnordinaryA elements exists
        if (request.InteractionsWithUnordinaryA != null)
        {
            var interactionsWithUnordinaryA = await _dbContext.Set<UnordinaryPerson>()
                .Where(c => request.InteractionsWithUnordinaryA.Contains(c.Id))
                .ToListAsync();

            if (interactionsWithUnordinaryA.Count != request.InteractionsWithUnordinaryA.Count)
                throw new ArgumentException("One or more provided InteractionsWithUnordinaryA are invalid.");
            unordinaryPerson.InteractionsWithUnordinaryA = interactionsWithUnordinaryA;
        }

        // Check if Sources elements exists
        if (request.Sources != null)
        {
            var sources = await _dbContext.Set<WrittenSource>()
                .Where(c => request.Sources.Contains(c.Id))
                .ToListAsync();

            if (sources.Count != request.Sources.Count)
                throw new ArgumentException("One or more provided Sources are invalid.");
            unordinaryPerson.Sources = sources;
        }
        
        _dbContext.Set<UnordinaryPerson>().Add(unordinaryPerson);
        await _dbContext.SaveChangesAsync();

        return new UnordinaryPersonDetailDto
        {
            Id = unordinaryPerson.Id,
            Name = unordinaryPerson.Name,
            AlternateName = unordinaryPerson.AlternateName,
            BirthYear = unordinaryPerson.BirthYear,
            DeathYear = unordinaryPerson.DeathYear,
            ProbableBirthYear = unordinaryPerson.ProbableBirthYear,
            ProbableDeathYear = unordinaryPerson.ProbableDeathYear,
            Description = unordinaryPerson.Description,
            Religion = _mapper.Map<ReligionDto>(unordinaryPerson.Religion),
            Ethnicity = _mapper.Map<EthnicityDto>(unordinaryPerson.Ethnicity),
            FormerReligion = _mapper.Map<List<ReligionDto>>(unordinaryPerson.FormerReligion),
            Profession = _mapper.Map<ProfessionDto>(unordinaryPerson.Profession),
            Gender = _mapper.Map<GenderDto>(unordinaryPerson.Gender),
            Sources = _mapper.Map<List<WrittenSourceBaseDto>>(unordinaryPerson.Sources),
            InteractionsWithOrdinary = _mapper.Map<List<OrdinaryPersonBaseDto>>(unordinaryPerson.InteractionsWithOrdinary),
            InteractionsWithUnordinaryA = _mapper.Map<List<UnordinaryPersonBaseDto>>(unordinaryPerson.InteractionsWithUnordinaryA),
            InteractionsWithUnordinaryB = _mapper.Map<List<UnordinaryPersonBaseDto>>(unordinaryPerson.InteractionsWithUnordinaryB),
            BirthPlace = _mapper.Map<CityBaseDto>(unordinaryPerson.BirthPlace),
            DeathPlace = _mapper.Map<CityBaseDto>(unordinaryPerson.DeathPlace),
        };
    }

    public async Task<UnordinaryPersonDetailDto> UpdateAsync(int id, UnordinaryPersonUpdateRequest request)
    {
        var unordinaryPerson = await _dbContext.Set<UnordinaryPerson>()
            .Include(ws => ws.Religion)
            .Include(ws => ws.Ethnicity)
            .Include(ws => ws.DeathPlace)
            .Include(ws => ws.InteractionsWithOrdinary)
            .Include(ws => ws.FormerReligion)
            .Include(ws => ws.Profession)
            .Include(ws => ws.Gender)
            .Include(ws => ws.InteractionsWithUnordinaryA)
            .Include(ws => ws.Sources)
            .FirstOrDefaultAsync(ws => ws.Id == id);

        if (unordinaryPerson == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        // Religion update only if a valid religion is provided
        if (request.Religion != null)
        {
            var religion = await _dbContext.Set<Religion>().FirstOrDefaultAsync(e => e.Name == request.Religion);
            if (religion == null)
                throw new ArgumentException($"Religion with name {request.Religion} not found.");
            unordinaryPerson.Religion = religion;
        }

        // Ethnicity update only if a valid ethnicity is provided
        if (request.Ethnicity != null)
        {
            var ethnicity = await _dbContext.Set<Ethnicity>().FirstOrDefaultAsync(e => e.Name == request.Ethnicity);
            if (ethnicity == null)
                throw new ArgumentException($"Ethnicity with name {request.Ethnicity} not found.");
            unordinaryPerson.Ethnicity = ethnicity;
        }

        // Only update fields if they are not null
        if ((request.DeathYear != null) && (request.DeathYear.Count > 0))
            unordinaryPerson.DeathYear = request.DeathYear;

        // DeathPlace update only if a valid city name is provided
        if (request.DeathPlace != null)
        {
            var deathPlace = await _dbContext.Set<City>().FirstOrDefaultAsync(e => e.AsciiName == request.DeathPlace);
            if (deathPlace == null)
                throw new ArgumentException($"DeathPlace with name {request.DeathPlace} not found.");
            unordinaryPerson.DeathPlace = deathPlace;
        }

        // Update InteractionsWithOrdinary 
        if (request.InteractionsWithOrdinary != null)
        {
            var interactionsWithOrdinary = await _dbContext.Set<OrdinaryPerson>()
                .Where(l => request.InteractionsWithOrdinary.Contains(l.Id))
                .ToListAsync();

            if (interactionsWithOrdinary.Count != request.InteractionsWithOrdinary.Count)
                throw new ArgumentException("One or more provided Ordinary Person are invalid.");

            unordinaryPerson.InteractionsWithOrdinary = interactionsWithOrdinary;
        }

        if (request.AlternateName != null)
            unordinaryPerson.AlternateName = request.AlternateName;

        if (request.BirthYear != null && request.BirthYear.Count > 0)  
            unordinaryPerson.BirthYear = request.BirthYear;

        // Only update fields if they are provided in the request
        if (request.ProbableBirthYear != null)
            unordinaryPerson.ProbableBirthYear = request.ProbableBirthYear;

        if (request.ProbableDeathYear != null)
            unordinaryPerson.ProbableDeathYear = request.ProbableDeathYear;

        if (request.Description != null)
            unordinaryPerson.Description = request.Description;

        // Update FormerReligion 
        if (request.FormerReligion != null)
        {
            var formerReligion = await _dbContext.Set<Religion>()
                .Where(l => request.FormerReligion.Contains(l.Name))
                .ToListAsync();

            if (formerReligion.Count != request.FormerReligion.Count)
                throw new ArgumentException("One or more provided Former Religion are invalid.");

            unordinaryPerson.FormerReligion = formerReligion;
        }

        // Profession update only if a valid profession is provided
        if (request.Profession != null)
        {
            var profession = await _dbContext.Set<Profession>().FirstOrDefaultAsync(e => e.Name == request.Profession);
            if (profession == null)
                throw new ArgumentException($"Profession with name {request.Profession} not found.");
            unordinaryPerson.Profession = profession;
        }

        // Gender update only if a valid gender is provided
        if (request.Gender != null)
        {
            var gender = await _dbContext.Set<Gender>().FirstOrDefaultAsync(e => e.Name == request.Gender);
            if (gender == null)
                throw new ArgumentException($"Gender with name {request.Gender} not found.");
            unordinaryPerson.Gender = gender;
        }

        // BirthPlace update only if a valid city is provided
        if (request.BirthPlace != null)
        {
            var birthPlace = await _dbContext.Set<City>().FirstOrDefaultAsync(e => e.AsciiName == request.BirthPlace);
            if (birthPlace == null)
                throw new ArgumentException($"BirthPlace with name {request.BirthPlace} not found.");
            unordinaryPerson.BirthPlace = birthPlace;
        }

        // Update InteractionsWithUnordinaryA 
        if (request.InteractionsWithUnordinaryA != null)
        {
            var interactionsWithUnordinaryA = await _dbContext.Set<UnordinaryPerson>()
                .Where(l => request.InteractionsWithUnordinaryA.Contains(l.Id))
                .ToListAsync();

            if (interactionsWithUnordinaryA.Count != request.InteractionsWithUnordinaryA.Count)
                throw new ArgumentException("One or more provided Unordinary Person are invalid.");

            unordinaryPerson.InteractionsWithUnordinaryA = interactionsWithUnordinaryA;
        }

        // Update Sources 
        if (request.Sources != null)
        {
            var sources = await _dbContext.Set<WrittenSource>()
                .Where(l => request.Sources.Contains(l.Id))
                .ToListAsync();

            if (sources.Count != request.Sources.Count)
                throw new ArgumentException("One or more provided Written Source are invalid.");

            unordinaryPerson.Sources = sources;
        }

        await _dbContext.SaveChangesAsync();

        return new UnordinaryPersonDetailDto
        {
            Id = unordinaryPerson.Id,
            Name = unordinaryPerson.Name,
            AlternateName = unordinaryPerson.AlternateName,
            BirthYear = unordinaryPerson.BirthYear,
            DeathYear = unordinaryPerson.DeathYear,
            ProbableBirthYear = unordinaryPerson.ProbableBirthYear,
            ProbableDeathYear = unordinaryPerson.ProbableDeathYear,
            Description = unordinaryPerson.Description,
            Religion = _mapper.Map<ReligionDto>(unordinaryPerson.Religion),
            Ethnicity = _mapper.Map<EthnicityDto>(unordinaryPerson.Ethnicity),
            FormerReligion = _mapper.Map<List<ReligionDto>>(unordinaryPerson.FormerReligion),
            Profession = _mapper.Map<ProfessionDto>(unordinaryPerson.Profession),
            Gender = _mapper.Map<GenderDto>(unordinaryPerson.Gender),
            Sources = _mapper.Map<List<WrittenSourceBaseDto>>(unordinaryPerson.Sources),
            InteractionsWithOrdinary = _mapper.Map<List<OrdinaryPersonBaseDto>>(unordinaryPerson.InteractionsWithOrdinary),
            InteractionsWithUnordinaryA = _mapper.Map<List<UnordinaryPersonBaseDto>>(unordinaryPerson.InteractionsWithUnordinaryA),
            InteractionsWithUnordinaryB = _mapper.Map<List<UnordinaryPersonBaseDto>>(unordinaryPerson.InteractionsWithUnordinaryB),
            BirthPlace = _mapper.Map<CityBaseDto>(unordinaryPerson.BirthPlace),
            DeathPlace = _mapper.Map<CityBaseDto>(unordinaryPerson.DeathPlace),
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var unordinaryPerson = await _dbContext.Set<UnordinaryPerson>().FindAsync(id);
        if (unordinaryPerson == null)
            return false;

        _dbContext.Set<UnordinaryPerson>().Remove(unordinaryPerson);
        await _dbContext.SaveChangesAsync();
        return true;
    }
    
    public async Task<PaginationResponse<UnordinaryPersonFilterResponseDto>> GetPageAsync(
        int pageNumber, int pageSize, UnordinaryPersonFilterDto? filter)
    {
        var query = _dbContext.Set<UnordinaryPerson>()
            .Include(op => op.Religion).Include(op => op.Ethnicity)
            .Include(op => op.DeathPlace)
            .Include(op => op.InteractionsWithOrdinary)
            .AsQueryable();

        if(filter != null)
        {
            if(filter.Name != null)
            {
                var checkedString = filter.Name.ToLower().Replace(" ", "").Replace("\t", "");
                query = query.Where(e => e.Name.ToLower().Replace(" ", "").
                    Replace("\t", "").Contains(checkedString) || (e.AlternateName != null)
                    && e.AlternateName.ToLower().Replace(" ", "").Replace("\t", "").
                    Contains(checkedString));
            }
            if(filter.Religion != null) 
                query = query.Where(op => op.Religion != null && (op.Religion.Id == filter.Religion));

            if(filter.Ethnicity != null)
                query = query.Where(op => op.Ethnicity != null && (op.Ethnicity.Id == filter.Ethnicity));

            if(filter.DeathYear != null && filter.DeathYear.Count > 0)
            {
                if(filter.DeathYear.Count == 1)
                    query = query.Where(op => op.ProbableDeathYear != null && 
                        (op.ProbableDeathYear >= filter.DeathYear[0]));
                else // For case 2, further elements are ignored
                    query = query.Where(op => op.ProbableDeathYear != null && 
                        (op.ProbableDeathYear >= filter.DeathYear[0]) && 
                        (op.ProbableDeathYear <= filter.DeathYear[1]));
            }
            
            if(filter.DeathPlace != null)
                query = query.Where(op => op.DeathPlace != null && (op.DeathPlace.Id == filter.DeathPlace));
        
            if(filter.InteractionsWithOrdinary != null)
            {
                var innerItems = query.Include(op => op.InteractionsWithOrdinary).AsEnumerable()
                    .Where(op => (op.InteractionsWithOrdinary != null) 
                    && filter.InteractionsWithOrdinary.
                        Any(fs => op.InteractionsWithOrdinary.Select(s => s.Id).Contains(fs))
                        )
                    .OrderBy(p => p.Id)  // Sort by Id (or other field)
                    .Skip((pageNumber - 1) * pageSize)
                    .Take(pageSize)
                    .Select(p => new UnordinaryPersonFilterResponseDto
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Religion = _mapper.Map<ReligionDto>(p.Religion),
                        Ethnicity = _mapper.Map<EthnicityDto>(p.Ethnicity),
                        DeathYear = p.DeathYear,
                        DeathPlace = _mapper.Map<CityBaseDto>(p.DeathPlace),
                        InteractionsWithOrdinary = _mapper.Map<List<OrdinaryPersonBaseDto>>(p.InteractionsWithOrdinary),
                        AlternateName = p.AlternateName,
                    })
                    .ToList();

                Console.WriteLine("items", innerItems);

                int totalCount1 = innerItems.Count;

                return new PaginationResponse<UnordinaryPersonFilterResponseDto>
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
            .Select(p => new UnordinaryPersonFilterResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Religion = _mapper.Map<ReligionDto>(p.Religion),
                Ethnicity = _mapper.Map<EthnicityDto>(p.Ethnicity),
                DeathYear = p.DeathYear,
                DeathPlace = _mapper.Map<CityBaseDto>(p.DeathPlace),
                InteractionsWithOrdinary = _mapper.Map<List<OrdinaryPersonBaseDto>>(p.InteractionsWithOrdinary),
                AlternateName = p.AlternateName,
            })
            .ToListAsync();

        return new PaginationResponse<UnordinaryPersonFilterResponseDto>
        {
            Data = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
        };
    }

    public async Task<IEnumerable<UnordinaryPersonGraphDto>> GetAllForGraphAsync()
    {
        return await _dbContext.Set<UnordinaryPerson>()
            .Select(e => new UnordinaryPersonGraphDto
            {
                Id = e.Id,
                Name = e.Name,
                Religion = e.Religion == null ? null : e.Religion.Id,
                Ethnicity = e.Ethnicity == null ? null : e.Ethnicity.Id,
                Profession = e.Profession == null ? null : e.Profession.Id,
                BirthPlace = e.BirthPlace == null ? null : e.BirthPlace.Id,
                DeathPlace = e.DeathPlace == null ? null : e.DeathPlace.Id,
                Sources = e.Sources == null ? null : e.Sources.Select(s => s.Id).
                    ToList(),
                Gender = e.Gender == null ? null : e.Gender.Id,
                FormerReligion = e.FormerReligion == null ? null : e.FormerReligion.
                    Select(fr => fr.Id).ToList(),
                InteractionsWithUnordinaryA = e.InteractionsWithUnordinaryA == null ? null : 
                    e.InteractionsWithUnordinaryA.Select(fr => fr.Id).ToList(),
                InteractionsWithOrdinary = e.InteractionsWithOrdinary == null ? null : 
                    e.InteractionsWithOrdinary.Select(fr => fr.Id).ToList(),
            })
            .OrderBy(up => up.Id)
            .ToListAsync();
    }
}
