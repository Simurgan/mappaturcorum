using Mappa.Db;
using Mappa.Dtos;
using Mappa.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace Mappa.Services;

public class OrdinaryPersonService : IComplexEntityService<OrdinaryPerson, 
    OrdinaryPersonGeneralDto, OrdinaryPersonDetailDto, 
    OrdinaryPersonCreateRequest, OrdinaryPersonUpdateRequest>
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
            .Include(ws => ws.Religion)
            .Include(ws => ws.Ethnicity)
            .Include(ws => ws.Profession)
            .Include(ws => ws.Location)
            .Include(ws => ws.Sources)
            .Include(ws => ws.Gender)
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
            FormerReligion = _mapper.Map<List<ReligionDto>>(entity.FormerReligion),
            ReligionExplanation = entity.ReligionExplanation,
            ProfessionExplanation = entity.ProfessionExplanation,
            InterestingFeature = entity.InterestingFeature,
            InteractionWithOrdinaryExplanation = entity.InteractionWithOrdinaryExplanation,
            InteractionWithUnordinaryExplanation = entity.InteractionWithUnordinaryExplanation,
            Biography = entity.Biography,
            DepictionInTheSource = entity.DepictionInTheSource,
            ExplanationOfEthnicity = entity.ExplanationOfEthnicity,
            InteractionsWithOrdinaryA = _mapper.Map<List<OrdinaryPersonBaseDto>>(entity.InteractionsWithOrdinaryA),
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
            DepictionInTheSource = request.DepictionInTheSource,
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
            var location = await _dbContext.Set<City>().FirstOrDefaultAsync(e => e.AsciiName == request.Location);
            if (location == null)
                throw new ArgumentException($"Location with name {request.Location} not found.");
            ordinaryPerson.Location = location;
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
            var formerReligion = await _dbContext.Set<Religion>()
                .Where(c => request.FormerReligion.Contains(c.Name))
                .ToListAsync();

            if (formerReligion.Count != request.FormerReligion.Count)
                throw new ArgumentException("One or more provided Formerreligion are invalid.");
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
            var backgroundCity = await _dbContext.Set<City>().FirstOrDefaultAsync(e => e.AsciiName == request.BackgroundCity);
            if (backgroundCity == null)
                throw new ArgumentException($"BackgroundCity with name {request.BackgroundCity} not found.");
            ordinaryPerson.BackgroundCity = backgroundCity;
        }
        
        _dbContext.Set<OrdinaryPerson>().Add(ordinaryPerson);
        await _dbContext.SaveChangesAsync();

        return new OrdinaryPersonDetailDto
        {
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
            FormerReligion = _mapper.Map<List<ReligionDto>>(ordinaryPerson.FormerReligion),
            ReligionExplanation = ordinaryPerson.ReligionExplanation,
            ProfessionExplanation = ordinaryPerson.ProfessionExplanation,
            InterestingFeature = ordinaryPerson.InterestingFeature,
            InteractionWithOrdinaryExplanation = ordinaryPerson.InteractionWithOrdinaryExplanation,
            InteractionWithUnordinaryExplanation = ordinaryPerson.InteractionWithUnordinaryExplanation,
            Biography = ordinaryPerson.Biography,
            DepictionInTheSource = ordinaryPerson.DepictionInTheSource,
            ExplanationOfEthnicity = ordinaryPerson.ExplanationOfEthnicity,
            InteractionsWithOrdinaryA = _mapper.Map<List<OrdinaryPersonBaseDto>>(ordinaryPerson.InteractionsWithOrdinaryA),
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
            var location = await _dbContext.Set<City>().FirstOrDefaultAsync(e => e.AsciiName == request.Location);
            if (location == null)
                throw new ArgumentException($"Location with name {request.Location} not found.");
            ordinaryPerson.Location = location;
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
            var formerReligion = await _dbContext.Set<Religion>()
                .Where(l => request.FormerReligion.Contains(l.Name))
                .ToListAsync();

            if (formerReligion.Count != request.FormerReligion.Count)
                throw new ArgumentException("One or more provided Former Religion are invalid.");

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

        if (request.DepictionInTheSource != null)
            ordinaryPerson.DepictionInTheSource = request.DepictionInTheSource;

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
            var backgroundCity = await _dbContext.Set<City>().FirstOrDefaultAsync(e => e.AsciiName == request.BackgroundCity);
            if (backgroundCity == null)
                throw new ArgumentException($"BackgroundCity with name {request.BackgroundCity} not found.");
            ordinaryPerson.BackgroundCity = backgroundCity;
        }

        await _dbContext.SaveChangesAsync();

        return new OrdinaryPersonDetailDto
        {
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
            FormerReligion = _mapper.Map<List<ReligionDto>>(ordinaryPerson.FormerReligion),
            ReligionExplanation = ordinaryPerson.ReligionExplanation,
            ProfessionExplanation = ordinaryPerson.ProfessionExplanation,
            InterestingFeature = ordinaryPerson.InterestingFeature,
            InteractionWithOrdinaryExplanation = ordinaryPerson.InteractionWithOrdinaryExplanation,
            InteractionWithUnordinaryExplanation = ordinaryPerson.InteractionWithUnordinaryExplanation,
            Biography = ordinaryPerson.Biography,
            DepictionInTheSource = ordinaryPerson.DepictionInTheSource,
            ExplanationOfEthnicity = ordinaryPerson.ExplanationOfEthnicity,
            InteractionsWithOrdinaryA = _mapper.Map<List<OrdinaryPersonBaseDto>>(ordinaryPerson.InteractionsWithOrdinaryA),
            InteractionsWithUnordinary = _mapper.Map<List<UnordinaryPersonBaseDto>>(ordinaryPerson.InteractionsWithUnordinary),
            BackgroundCity = _mapper.Map<CityBaseDto>(ordinaryPerson.BackgroundCity),
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var ordinaryPerson = await _dbContext.Set<OrdinaryPerson>().FindAsync(id);
        if (ordinaryPerson == null)
            return false;

        _dbContext.Set<OrdinaryPerson>().Remove(ordinaryPerson);
        await _dbContext.SaveChangesAsync();
        return true;
    }
}
