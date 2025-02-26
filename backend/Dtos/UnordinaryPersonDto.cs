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
    public ProfessionDto? Profession {get; set;}
    public GenderDto? Gender {get; set;}
    public CityBaseDto? BirthPlace {get; set;}
    public List<UnordinaryPersonBaseDto>? InteractionsWithUnordinaryA {get; set;}
    public List<UnordinaryPersonBaseDto>? InteractionsWithUnordinaryB {get; set;}
    public List<WrittenSourceBaseDto>? Sources {get; set;}
    public string? Depiction {get; set;}
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
    public string? Profession {get; set;}
    public string? Gender {get; set;}
    public string? BirthPlace {get; set;}
    public List<int>? InteractionsWithUnordinaryA {get; set;}
    public List<int>? Sources {get; set;}
    public string? Depiction {get; set;}
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
    public string? Profession {get; set;}
    public string? Gender {get; set;}
    public string? BirthPlace {get; set;}
    public List<int>? InteractionsWithUnordinaryA {get; set;}
    public List<int>? Sources {get; set;}
    public string? Depiction {get; set;}
}

public class UnordinaryPersonFilterDto 
{
    public string? Name {get; set;}
    public List<int>? Religion {get; set;}
    public List<int>? Ethnicity {get; set;}
    public List<int>? DeathYear {get; set;}
    public List<int>? DeathPlace {get; set;}
    public List<int>? InteractionsWithOrdinary {get; set;}
}

public class UnordinaryPersonFilterResponseDto : UnordinaryPersonGeneralDto 
{
    public string? AlternateName {get; set;}
}

public class UnordinaryPersonGraphDto: UnordinaryPersonBaseDto 
{
    public int? Religion {get; set;}
    public int? Ethnicity {get; set;}
    public int? Profession {get; set;}
    public List<int>? Sources {get; set;}
    public int? BirthPlace {get; set;}
    public int? DeathPlace {get; set;}
    public int? Gender {get; set;}
    public List<int>? InteractionsWithUnordinaryA{get; set;}
    public List<int>? InteractionsWithUnordinaryB{get; set;}
    public List<int>? InteractionsWithOrdinary{get; set;}
}