using System.ComponentModel.DataAnnotations;
using Mappa.Entities;

namespace Mappa.Dtos;

public class UnordinaryPersonBaseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class UnordinaryPersonGeneralDto : UnordinaryPersonBaseDto
{
    public ReligionDto? Religion {get; set;}
    public EthnicityDto? Ethnicity {get; set;}
    public List<int>? DeathYear {get; set;}
    public CityBaseDto? DeathPlace {get; set;}
    public List<OrdinaryPersonBaseDto>? InteractionsWithOrdinary {get; set;}

}

public class UnordinaryPersonDetailDto : UnordinaryPersonGeneralDto
{
    public string? AlternateName {get; set;}
    public List<int>? BirthYear {get; set;}
    public int? ProbableBirthYear {get; set;}
    public int? ProbableDeathYear {get; set;}
    public string? Description {get; set;}
    public List<ReligionDto>? FormerReligion {get; set;}
    public ProfessionDto? Profession {get; set;}
    public GenderDto? Gender {get; set;}
    public CityBaseDto? BirthPlace {get; set;}
    public List<UnordinaryPersonBaseDto>? InteractionsWithUnordinaryA {get; set;}
    public List<UnordinaryPersonBaseDto>? InteractionsWithUnordinaryB {get; set;}
    public List<WrittenSourceBaseDto>? Sources {get; set;}
}

// Simple entities will be denoted by their Names.
// Complex entities with their ids
public class UnordinaryPersonCreateRequest
{
    [Required]
    public string Name { get; set; }
    public string? Religion {get; set;}
    public string? Ethnicity {get; set;}
    public List<int>? DeathYear {get; set;}
    public string? DeathPlace {get; set;}
    public List<int>? InteractionsWithOrdinary {get; set;}
    public string? AlternateName {get; set;}
    public List<int>? BirthYear {get; set;}
    public int? ProbableBirthYear {get; set;}
    public int? ProbableDeathYear {get; set;}
    public string? Description {get; set;}
    public List<string>? FormerReligion {get; set;}
    public string? Profession {get; set;}
    public string? Gender {get; set;}
    public string? BirthPlace {get; set;}
    public List<int>? InteractionsWithUnordinaryA {get; set;}
    public List<int>? Sources {get; set;}
}

public class UnordinaryPersonUpdateRequest
{
    public string? Name { get; set; }
    public string? Religion {get; set;}
    public string? Ethnicity {get; set;}
    public List<int>? DeathYear {get; set;}
    public string? DeathPlace {get; set;}
    public List<int>? InteractionsWithOrdinary {get; set;}
    public string? AlternateName {get; set;}
    public List<int>? BirthYear {get; set;}
    public int? ProbableBirthYear {get; set;}
    public int? ProbableDeathYear {get; set;}
    public string? Description {get; set;}
    public List<string>? FormerReligion {get; set;}
    public string? Profession {get; set;}
    public string? Gender {get; set;}
    public string? BirthPlace {get; set;}
    public List<int>? InteractionsWithUnordinaryA {get; set;}
    public List<int>? Sources {get; set;}
}

public class UnordinaryPersonFilterDto 
{
    public int? Religion {get; set;}
    public int? Ethnicity {get; set;}
    public List<int>? DeathYear {get; set;}
    public int? DeathPlace {get; set;}
    public List<int>? InteractionsWithOrdinary {get; set;}
}

public class UnordinaryPersonFilterResponseDto : UnordinaryPersonGeneralDto {}