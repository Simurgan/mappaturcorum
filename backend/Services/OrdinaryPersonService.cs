using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Logging.Abstractions;
using System.Diagnostics;

namespace Mappa.Services;

public class OrdinaryPersonService : IComplexEntityService<OrdinaryPerson, 
    OrdinaryPersonGeneralDto, OrdinaryPersonDetailDto, 
    OrdinaryPersonCreateRequest, OrdinaryPersonUpdateRequest, OrdinaryPersonFilterDto,
    OrdinaryPersonFilterResponseDto, OrdinaryPersonGraphDto>
{
    private readonly AppDbContext _dbContext;
    private readonly IMapper _mapper;

    public OrdinaryPersonService(AppDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<IEnumerable<OrdinaryPersonGeneralDto>> GetAllAsync()
    {
        return await _dbContext.Set<OrdinaryPerson>()
            .Include(op => op.Religion)
            .Include(op => op.Ethnicity)
            .Include(op => op.Profession)
            .Include(op => op.Location)
            .Include(op => op.Sources)
            .Include(op => op.Gender)
            .Select(e => new OrdinaryPersonGeneralDto
            {
                Id = e.Id,
                Name = e.Name,
                Religion = _mapper.Map<ReligionDto>(e.Religion),
                Ethnicity = _mapper.Map<EthnicityDto>(e.Ethnicity),
                Profession = _mapper.Map<ProfessionDto>(e.Profession),
                Location = _mapper.Map<CityBaseDto>(e.Location),
                Sources = _mapper.Map<List<WrittenSourceBaseDto>>(e.Sources),
                Gender = _mapper.Map<GenderDto>(e.Gender),
            })
            .OrderBy(op => op.Id)
            .ToListAsync();
    }

    public async Task<OrdinaryPersonDetailDto> GetByIdAsync(int id)
    {
        var entity = await _dbContext.Set<OrdinaryPerson>()
            .Include(ws => ws.Religion)
            .Include(ws => ws.Ethnicity)
            .Include(ws => ws.Profession)
            .Include(ws => ws.Location)
            .Include(ws => ws.Sources)
            .Include(ws => ws.Gender)
            .Include(ws => ws.FormerReligion)
            .Include(ws => ws.InteractionsWithOrdinaryA)
            .Include(ws => ws.InteractionsWithOrdinaryB)
            .Include(ws => ws.InteractionsWithUnordinary)
            .Include(ws => ws.Sources)
            .FirstOrDefaultAsync(ws => ws.Id == id);
        
        if (entity == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        return new OrdinaryPersonDetailDto
        {
            
            Id = entity.Id,
            Name = entity.Name,
            Religion = _mapper.Map<ReligionDto>(entity.Religion),
            Ethnicity = _mapper.Map<EthnicityDto>(entity.Ethnicity),
            Profession = _mapper.Map<ProfessionDto>(entity.Profession),
            Location = _mapper.Map<CityBaseDto>(entity.Location),
            Sources = _mapper.Map<List<WrittenSourceBaseDto>>(entity.Sources),
            Gender = _mapper.Map<GenderDto>(entity.Gender),
            AlternateName = entity.AlternateName,
            BirthYear = entity.BirthYear,
            DeathYear = entity.DeathYear,
            ProbableBirthYear = entity.ProbableBirthYear,
            ProbableDeathYear = entity.ProbableDeathYear,
            Description = entity.Description,
            FormerReligion = _mapper.Map<ReligionDto>(entity.FormerReligion),
            ReligionExplanation = entity.ReligionExplanation,
            ProfessionExplanation = entity.ProfessionExplanation,
            InterestingFeature = entity.InterestingFeature,
            InteractionWithOrdinaryExplanation = entity.InteractionWithOrdinaryExplanation,
            InteractionWithUnordinaryExplanation = entity.InteractionWithUnordinaryExplanation,
            Biography = entity.Biography,
            DescriptionInTheSource = entity.DescriptionInTheSource,
            ExplanationOfEthnicity = entity.ExplanationOfEthnicity,
            InteractionsWithOrdinaryA = _mapper.Map<List<OrdinaryPersonBaseDto>>(entity.InteractionsWithOrdinaryA),
            InteractionsWithOrdinaryB = _mapper.Map<List<OrdinaryPersonBaseDto>>(entity.InteractionsWithOrdinaryB),
            InteractionsWithUnordinary = _mapper.Map<List<UnordinaryPersonBaseDto>>(entity.InteractionsWithUnordinary),
            BackgroundCity = _mapper.Map<CityBaseDto>(entity.BackgroundCity),
        };
    }

    public async Task<OrdinaryPersonDetailDto> CreateAsync(OrdinaryPersonCreateRequest request)
    {
        var ordinaryPerson = new OrdinaryPerson
        {
            Name = request.Name,
            AlternateName = request.AlternateName,
            BirthYear = request.BirthYear,
            DeathYear = request.DeathYear,
            ProbableBirthYear = request.ProbableBirthYear,
            ProbableDeathYear = request.ProbableDeathYear,
            Description = request.Description,
            ReligionExplanation = request.ReligionExplanation,
            ProfessionExplanation = request.ProfessionExplanation,
            InterestingFeature = request.InterestingFeature,
            InteractionWithOrdinaryExplanation = request.InteractionWithOrdinaryExplanation,
            InteractionWithUnordinaryExplanation = request.InteractionWithUnordinaryExplanation,
            Biography = request.Biography,
            DescriptionInTheSource = request.DescriptionInTheSource,
            ExplanationOfEthnicity = request.ExplanationOfEthnicity,
        };

        // Check if Religion exists
        if(request.Religion != null)
        {
            var religion = await _dbContext.Set<Religion>().FirstOrDefaultAsync(e => e.Name == request.Religion);
            if (religion == null)
                throw new ArgumentException($"Religion with name {request.Religion} not found.");
            ordinaryPerson.Religion = religion;
        }

        // Check if Ethnicity exists
        if(request.Ethnicity != null)
        {
            var ethnicity = await _dbContext.Set<Ethnicity>().FirstOrDefaultAsync(e => e.Name == request.Ethnicity);
            if (ethnicity == null)
                throw new ArgumentException($"Ethnicity with name {request.Ethnicity} not found.");
            ordinaryPerson.Ethnicity = ethnicity;
        }

        // Check if Profession exists
        if(request.Profession != null)
        {
            var profession = await _dbContext.Set<Profession>().FirstOrDefaultAsync(e => e.Name == request.Profession);
            if (profession == null)
                throw new ArgumentException($"Profession with name {request.Profession} not found.");
            ordinaryPerson.Profession = profession;
        }

        // Check if Location exists
        if(request.Location != null)
        {
            var location = await _dbContext.Set<City>().FirstOrDefaultAsync(e => 
                e.Name == request.Location || (e.AsciiName != null && 
                e.AsciiName == request.Location));
            if (location == null)
                throw new ArgumentException($"Location with name {request.Location} not found.");
            ordinaryPerson.Location = location;
            location.NumberOfLocationOf ++;
        }

        // Check if Sources elements exists
        if (request.Sources != null)
        {
            var sources = await _dbContext.Set<WrittenSource>()
                .Where(c => request.Sources.Contains(c.Id))
                .ToListAsync();

            if (sources.Count != request.Sources.Count)
                throw new ArgumentException("One or more provided Sources are invalid.");
            ordinaryPerson.Sources = sources;
        }

        // Check if Gender exists
        if(request.Gender != null)
        {
            var gender = await _dbContext.Set<Gender>().FirstOrDefaultAsync(e => e.Name == request.Gender);
            if (gender == null)
                throw new ArgumentException($"Gender with name {request.Gender} not found.");
            ordinaryPerson.Gender = gender;
        }

        // Check if FormerReligion elements exists
        if (request.FormerReligion != null)
        {
            var formerReligion = await _dbContext.Set<Religion>().FirstOrDefaultAsync(e => e.Name == request.FormerReligion);
            if (formerReligion == null)
                throw new ArgumentException($"Religion with name {request.FormerReligion} not found.");
            ordinaryPerson.FormerReligion = formerReligion;
        }

        // Check if InteractionsWithOrdinaryA elements exists
        if (request.InteractionsWithOrdinaryA != null)
        {
            var interactionsWithOrdinaryA = await _dbContext.Set<OrdinaryPerson>()
                .Where(c => request.InteractionsWithOrdinaryA.Contains(c.Id))
                .ToListAsync();

            if (interactionsWithOrdinaryA.Count != request.InteractionsWithOrdinaryA.Count)
                throw new ArgumentException("One or more provided InteractionsWithOrdinaryA are invalid.");
            ordinaryPerson.InteractionsWithOrdinaryA = interactionsWithOrdinaryA;
        }

        // Check if InteractionsWithUnordinary elements exists
        if (request.InteractionsWithUnordinary != null)
        {
            var interactionsWithUnordinary = await _dbContext.Set<UnordinaryPerson>()
                .Where(c => request.InteractionsWithUnordinary.Contains(c.Id))
                .ToListAsync();

            if (interactionsWithUnordinary.Count != request.InteractionsWithUnordinary.Count)
                throw new ArgumentException("One or more provided InteractionsWithUnordinary are invalid.");
            ordinaryPerson.InteractionsWithUnordinary = interactionsWithUnordinary;
        }

        // Check if BackgroundCity exists
        if(request.BackgroundCity != null)
        {
            var backgroundCity = await _dbContext.Set<City>().FirstOrDefaultAsync(e => 
                e.Name == request.BackgroundCity || (e.AsciiName != null && 
                e.AsciiName == request.BackgroundCity));
            if (backgroundCity == null)
                throw new ArgumentException($"BackgroundCity with name {request.BackgroundCity} not found.");
            ordinaryPerson.BackgroundCity = backgroundCity;
            backgroundCity.NumberOfBackgroundCityOf++;
        }
        
        _dbContext.Set<OrdinaryPerson>().Add(ordinaryPerson);
        await _dbContext.SaveChangesAsync();

        return new OrdinaryPersonDetailDto
        {
            Id = ordinaryPerson.Id,
            Name = ordinaryPerson.Name,
            Religion = _mapper.Map<ReligionDto>(ordinaryPerson.Religion),
            Ethnicity = _mapper.Map<EthnicityDto>(ordinaryPerson.Ethnicity),
            Profession = _mapper.Map<ProfessionDto>(ordinaryPerson.Profession),
            Location = _mapper.Map<CityBaseDto>(ordinaryPerson.Location),
            Sources = _mapper.Map<List<WrittenSourceBaseDto>>(ordinaryPerson.Sources),
            Gender = _mapper.Map<GenderDto>(ordinaryPerson.Gender),
            AlternateName = ordinaryPerson.AlternateName,
            BirthYear = ordinaryPerson.BirthYear,
            DeathYear = ordinaryPerson.DeathYear,
            ProbableBirthYear = ordinaryPerson.ProbableBirthYear,
            ProbableDeathYear = ordinaryPerson.ProbableDeathYear,
            Description = ordinaryPerson.Description,
            FormerReligion = _mapper.Map<ReligionDto>(ordinaryPerson.FormerReligion),
            ReligionExplanation = ordinaryPerson.ReligionExplanation,
            ProfessionExplanation = ordinaryPerson.ProfessionExplanation,
            InterestingFeature = ordinaryPerson.InterestingFeature,
            InteractionWithOrdinaryExplanation = ordinaryPerson.InteractionWithOrdinaryExplanation,
            InteractionWithUnordinaryExplanation = ordinaryPerson.InteractionWithUnordinaryExplanation,
            Biography = ordinaryPerson.Biography,
            DescriptionInTheSource = ordinaryPerson.DescriptionInTheSource,
            ExplanationOfEthnicity = ordinaryPerson.ExplanationOfEthnicity,
            InteractionsWithOrdinaryA = _mapper.Map<List<OrdinaryPersonBaseDto>>(ordinaryPerson.InteractionsWithOrdinaryA),
            InteractionsWithOrdinaryB = _mapper.Map<List<OrdinaryPersonBaseDto>>(ordinaryPerson.InteractionsWithOrdinaryB),
            InteractionsWithUnordinary = _mapper.Map<List<UnordinaryPersonBaseDto>>(ordinaryPerson.InteractionsWithUnordinary),
            BackgroundCity = _mapper.Map<CityBaseDto>(ordinaryPerson.BackgroundCity),
        };
    }

    public async Task<OrdinaryPersonDetailDto> UpdateAsync(int id, OrdinaryPersonUpdateRequest request)
    {
        var ordinaryPerson = await _dbContext.Set<OrdinaryPerson>()
            .Include(ws => ws.Religion)
            .Include(ws => ws.Ethnicity)
            .Include(ws => ws.Profession)
            .Include(ws => ws.Location)
            .Include(ws => ws.Sources)
            .Include(ws => ws.Gender)
            .Include(ws => ws.FormerReligion)
            .Include(ws => ws.InteractionsWithOrdinaryA)
            .Include(ws => ws.InteractionsWithOrdinaryB)
            .Include(ws => ws.InteractionsWithUnordinary)
            .Include(ws => ws.Sources)
            .FirstOrDefaultAsync(ws => ws.Id == id);

        if (ordinaryPerson == null)
            throw new ArgumentException($"Entity with ID {id} not found.");

        // Religion update only if a valid religion name is provided
        if (request.Religion != null)
        {
            var religion = await _dbContext.Set<Religion>().FirstOrDefaultAsync(e => e.Name == request.Religion);
            if (religion == null)
                throw new ArgumentException($"Religion with name {request.Religion} not found.");
            ordinaryPerson.Religion = religion;
        }

        // Ethnicity update only if a valid ethnicity name is provided
        if (request.Ethnicity != null)
        {
            var ethnicity = await _dbContext.Set<Ethnicity>().FirstOrDefaultAsync(e => e.Name == request.Ethnicity);
            if (ethnicity == null)
                throw new ArgumentException($"Ethnicity with name {request.Ethnicity} not found.");
            ordinaryPerson.Ethnicity = ethnicity;
        }

        // Profession update only if a valid profession name is provided
        if (request.Profession != null)
        {
            var profession = await _dbContext.Set<Profession>().FirstOrDefaultAsync(e => e.Name == request.Profession);
            if (profession == null)
                throw new ArgumentException($"Profession with name {request.Profession} not found.");
            ordinaryPerson.Profession = profession;
        }

        // Location update only if a valid city is provided
        if (request.Location != null)
        {
            var location = await _dbContext.Set<City>().FirstOrDefaultAsync(e => 
                e.Name == request.Location || (e.AsciiName != null && 
                e.AsciiName == request.Location));
            if (location == null)
                throw new ArgumentException($"Location with name {request.Location} not found.");

            if(ordinaryPerson.Location != null)
            {
                ordinaryPerson.Location.NumberOfLocationOf--;

                if(ordinaryPerson.Location.NumberOfLocationOf < 0)
                    throw new Exception("City.NumberOfLocationOf cannot be < 0");
            }

            ordinaryPerson.Location = location;
            location.NumberOfLocationOf++;
        }

        // Update Sources 
        if (request.Sources != null)
        {
            var sources = await _dbContext.Set<WrittenSource>()
                .Where(l => request.Sources.Contains(l.Id))
                .ToListAsync();

            if (sources.Count != request.Sources.Count)
                throw new ArgumentException("One or more provided Written Source are invalid.");

            ordinaryPerson.Sources = sources;
        }

        // Gender update only if a valid gender name is provided
        if (request.Gender != null)
        {
            var gender = await _dbContext.Set<Gender>().FirstOrDefaultAsync(e => e.Name == request.Gender);
            if (gender == null)
                throw new ArgumentException($"Gender with name {request.Gender} not found.");
            ordinaryPerson.Gender = gender;
        }

        if (request.AlternateName != null)
            ordinaryPerson.AlternateName = request.AlternateName;

        if ((request.BirthYear!= null) && (request.BirthYear.Count > 0))
            ordinaryPerson.BirthYear = request.BirthYear;

        // Only update fields if they are not null
        if ((request.DeathYear != null) && (request.DeathYear.Count > 0))
            ordinaryPerson.DeathYear = request.DeathYear;

        // Only update fields if they are provided in the request
        if (request.ProbableBirthYear != null)
            ordinaryPerson.ProbableBirthYear = request.ProbableBirthYear;

        if (request.ProbableDeathYear != null)
            ordinaryPerson.ProbableDeathYear = request.ProbableDeathYear;

        if (request.Description != null)
            ordinaryPerson.Description = request.Description;

        // Update FormerReligion 
        if (request.FormerReligion != null)
        {
            var formerReligion = await _dbContext.Set<Religion>().FirstOrDefaultAsync(e => e.Name == request.FormerReligion);
            if (formerReligion == null)
                throw new ArgumentException($"Religion with name {request.FormerReligion} not found.");
            ordinaryPerson.FormerReligion = formerReligion;
        }

        if (request.ReligionExplanation != null)
            ordinaryPerson.ReligionExplanation = request.ReligionExplanation;

        if (request.ProfessionExplanation != null)
            ordinaryPerson.ProfessionExplanation = request.ProfessionExplanation;

        if (request.InterestingFeature != null)
            ordinaryPerson.InterestingFeature = request.InterestingFeature;

        if (request.InteractionWithOrdinaryExplanation != null)
            ordinaryPerson.InteractionWithOrdinaryExplanation = request.InteractionWithOrdinaryExplanation;

        if (request.InteractionWithUnordinaryExplanation != null)
            ordinaryPerson.InteractionWithUnordinaryExplanation = request.InteractionWithUnordinaryExplanation;

        if (request.Biography != null)
            ordinaryPerson.Biography = request.Biography;

        if (request.DescriptionInTheSource != null)
            ordinaryPerson.DescriptionInTheSource = request.DescriptionInTheSource;

        if (request.ExplanationOfEthnicity != null)
            ordinaryPerson.ExplanationOfEthnicity = request.ExplanationOfEthnicity;

        // Update InteractionsWithOrdinaryA 
        if (request.InteractionsWithOrdinaryA != null)
        {
            var InteractionsWithOrdinaryA = await _dbContext.Set<OrdinaryPerson>()
                .Where(l => request.InteractionsWithOrdinaryA.Contains(l.Id))
                .ToListAsync();

            if (InteractionsWithOrdinaryA.Count != request.InteractionsWithOrdinaryA.Count)
                throw new ArgumentException("One or more provided Ordinary Person are invalid.");

            ordinaryPerson.InteractionsWithOrdinaryA = InteractionsWithOrdinaryA;
        }

        // Update InteractionsWithUnordinary 
        if (request.InteractionsWithUnordinary != null)
        {
            var InteractionsWithUnordinary = await _dbContext.Set<UnordinaryPerson>()
                .Where(l => request.InteractionsWithUnordinary.Contains(l.Id))
                .ToListAsync();

            if (InteractionsWithUnordinary.Count != request.InteractionsWithUnordinary.Count)
                throw new ArgumentException("One or more provided Unordinary Person are invalid.");

            ordinaryPerson.InteractionsWithUnordinary = InteractionsWithUnordinary;
        }

        // BackgroundCity update only if a valid ID is provided
        if (request.BackgroundCity != null)
        {
            var backgroundCity = await _dbContext.Set<City>().FirstOrDefaultAsync(e => 
                e.Name == request.BackgroundCity || (e.AsciiName != null && 
                e.AsciiName == request.BackgroundCity));
            if (backgroundCity == null)
                throw new ArgumentException($"BackgroundCity with name {request.BackgroundCity} not found.");
            
            if(ordinaryPerson.BackgroundCity != null)
            {
                ordinaryPerson.BackgroundCity.NumberOfBackgroundCityOf--;

                if(ordinaryPerson.BackgroundCity.NumberOfBackgroundCityOf < 0)
                    throw new Exception("City.NumberOfBackgroundCityOf cannot be < 0");
            }
            ordinaryPerson.BackgroundCity = backgroundCity;
            backgroundCity.NumberOfBackgroundCityOf++;
        }

        await _dbContext.SaveChangesAsync();

        return new OrdinaryPersonDetailDto
        {
            Id = ordinaryPerson.Id,
            Name = ordinaryPerson.Name,
            Religion = _mapper.Map<ReligionDto>(ordinaryPerson.Religion),
            Ethnicity = _mapper.Map<EthnicityDto>(ordinaryPerson.Ethnicity),
            Profession = _mapper.Map<ProfessionDto>(ordinaryPerson.Profession),
            Location = _mapper.Map<CityBaseDto>(ordinaryPerson.Location),
            Sources = _mapper.Map<List<WrittenSourceBaseDto>>(ordinaryPerson.Sources),
            Gender = _mapper.Map<GenderDto>(ordinaryPerson.Gender),
            AlternateName = ordinaryPerson.AlternateName,
            BirthYear = ordinaryPerson.BirthYear,
            DeathYear = ordinaryPerson.DeathYear,
            ProbableBirthYear = ordinaryPerson.ProbableBirthYear,
            ProbableDeathYear = ordinaryPerson.ProbableDeathYear,
            Description = ordinaryPerson.Description,
            FormerReligion = _mapper.Map<ReligionDto>(ordinaryPerson.FormerReligion),
            ReligionExplanation = ordinaryPerson.ReligionExplanation,
            ProfessionExplanation = ordinaryPerson.ProfessionExplanation,
            InterestingFeature = ordinaryPerson.InterestingFeature,
            InteractionWithOrdinaryExplanation = ordinaryPerson.InteractionWithOrdinaryExplanation,
            InteractionWithUnordinaryExplanation = ordinaryPerson.InteractionWithUnordinaryExplanation,
            Biography = ordinaryPerson.Biography,
            DescriptionInTheSource = ordinaryPerson.DescriptionInTheSource,
            ExplanationOfEthnicity = ordinaryPerson.ExplanationOfEthnicity,
            InteractionsWithOrdinaryA = _mapper.Map<List<OrdinaryPersonBaseDto>>(ordinaryPerson.InteractionsWithOrdinaryA),
            InteractionsWithOrdinaryB = _mapper.Map<List<OrdinaryPersonBaseDto>>(ordinaryPerson.InteractionsWithOrdinaryB),
            InteractionsWithUnordinary = _mapper.Map<List<UnordinaryPersonBaseDto>>(ordinaryPerson.InteractionsWithUnordinary),
            BackgroundCity = _mapper.Map<CityBaseDto>(ordinaryPerson.BackgroundCity),
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var ordinaryPerson = await _dbContext.Set<OrdinaryPerson>()
            .Include(e => e.BackgroundCity)
            .Include(e => e.Location)
            .FirstOrDefaultAsync(e => e.Id == id);

        if (ordinaryPerson == null)
            return false;

        if(ordinaryPerson.Location != null)
        {
            ordinaryPerson.Location.NumberOfLocationOf --;

            if(ordinaryPerson.Location.NumberOfLocationOf < 0)
                throw new Exception("City.NumberOfLocationOf cannot be < 0");
        }

        if(ordinaryPerson.BackgroundCity != null)
        {
            ordinaryPerson.BackgroundCity.NumberOfBackgroundCityOf --;

            if(ordinaryPerson.BackgroundCity.NumberOfBackgroundCityOf < 0)
                throw new Exception("City.NumberOfBackgroundCityOf cannot be < 0");
        }
             

        _dbContext.Set<OrdinaryPerson>().Remove(ordinaryPerson);
        await _dbContext.SaveChangesAsync();
        return true;
    }

    public async Task<PaginationResponse<OrdinaryPersonFilterResponseDto>> GetPageAsync(
        int pageNumber, int pageSize, OrdinaryPersonFilterDto? filter)
    {
        var query = _dbContext.Set<OrdinaryPerson>()
            .Include(op => op.Religion).Include(op => op.Ethnicity)
            .Include(op => op.Profession).Include(op => op.Location)
            .Include(op => op.Sources).Include(op => op.Gender)
            .Include(op => op.InteractionsWithUnordinary)
            .Include(op => op.FormerReligion)
            .AsQueryable();

        IEnumerable<OrdinaryPerson> innerItems = query.AsEnumerable();

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

            if(filter.Religion != null && filter.Religion.Count != 0) 
                query = query.Where(op => op.Religion != null &&  filter.Religion.Contains(op.Religion.Id));

            if(filter.FormerReligion != null && filter.FormerReligion.Count != 0) 
                query = query.Where(op => op.FormerReligion != null &&  filter.FormerReligion.Contains(op.FormerReligion.Id));

            if(filter.Ethnicity != null && filter.Ethnicity.Count != 0)
                query = query.Where(op => op.Ethnicity != null && filter.Ethnicity.Contains(op.Ethnicity.Id));
        
            if(filter.Profession != null && filter.Profession.Count != 0)
                query = query.Where(op => op.Profession != null && filter.Profession.Contains(op.Profession.Id));
        
            if(filter.Location != null && filter.Location.Count != 0)
                query = query.Where(op => op.Location != null && filter.Location.Contains(op.Location.Id));

            if(filter.Gender != null && filter.Gender.Count != 0)
                query = query.Where(op => op.Gender != null && filter.Gender.Contains(op.Gender.Id));
        
            innerItems = query.AsEnumerable();

            if(filter.Sources != null && filter.Sources.Count != 0)
            {
                innerItems = innerItems
                    .Where(op => (op.Sources != null) 
                    && filter.Sources.
                        Any(fs => op.Sources.Select(s => s.Id).Contains(fs))
                        );
            }

            if(filter.InteractionsWithUnordinary != null && filter.InteractionsWithUnordinary.Count != 0)
            {
                innerItems = innerItems
                    .Where(op => (op.InteractionsWithUnordinary != null) 
                    && filter.InteractionsWithUnordinary.
                        Any(fs => op.InteractionsWithUnordinary.Select(s => s.Id).Contains(fs))
                        );
            }
        }

        int totalCount = innerItems.Count();

        var items = innerItems
            .OrderBy(p => p.Id)  // Sort by Id (or other field)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .Select(p => new OrdinaryPersonFilterResponseDto
            {
                Id = p.Id,
                Name = p.Name,
                Religion = _mapper.Map<ReligionDto>(p.Religion),
                Ethnicity = _mapper.Map<EthnicityDto>(p.Ethnicity),
                Profession = _mapper.Map<ProfessionDto>(p.Profession),
                Location = _mapper.Map<CityBaseDto>(p.Location),
                Sources = _mapper.Map<List<WrittenSourceBaseDto>>(p.Sources),
                Gender = _mapper.Map<GenderDto>(p.Gender),
                InteractionsWithUnordinary = _mapper.Map<List<UnordinaryPersonBaseDto>>(p.InteractionsWithUnordinary),
                AlternateName = p.AlternateName,
                FormerReligion = _mapper.Map<ReligionDto>(p.FormerReligion),
            })
            .ToList();

        return new PaginationResponse<OrdinaryPersonFilterResponseDto>
        {
            Data = items,
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
        };
    }

    public async Task<IEnumerable<OrdinaryPersonGraphDto>> GetAllForGraphAsync()
    {
        return await _dbContext.Set<OrdinaryPerson>()
            .Select(e => new OrdinaryPersonGraphDto
            {
                Id = e.Id,
                Name = e.Name,
                Religion = e.Religion == null ? null : e.Religion.Id,
                Ethnicity = e.Ethnicity == null ? null : e.Ethnicity.Id,
                Profession = e.Profession == null ? null : e.Profession.Id,
                Location = e.Location == null ? null : e.Location.Id,
                Sources = e.Sources == null ? null : e.Sources.Select(s => s.Id).
                    ToList(),
                Gender = e.Gender == null ? null : e.Gender.Id,
                FormerReligion = e.FormerReligion == null ? null : e.FormerReligion.Id,
                InteractionsWithUnordinary = e.InteractionsWithUnordinary == null ? null : 
                    e.InteractionsWithUnordinary.Select(fr => fr.Id).ToList(),
                InteractionsWithOrdinaryA = e.InteractionsWithOrdinaryA == null ? null : 
                    e.InteractionsWithOrdinaryA.Select(fr => fr.Id).ToList(),
                InteractionsWithOrdinaryB = e.InteractionsWithOrdinaryB == null ? null : 
                    e.InteractionsWithOrdinaryB.Select(fr => fr.Id).ToList(),
            })
            .OrderBy(op => op.Id)
            .ToListAsync();
    }
}
