using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks.Dataflow;
using Mappa.Entities;
using Microsoft.OpenApi.Expressions;

namespace Mappa.Dtos;

public class OrdinaryPersonBaseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class OrdinaryPersonGeneralDto : OrdinaryPersonBaseDto
{
    public ReligionDto? Religion {get; set;}
    public EthnicityDto? Ethnicity {get; set;}
    public ProfessionDto? Profession {get; set;}
    public CityBaseDto? Location {get; set;}
    public List<WrittenSourceBaseDto>? Sources {get; set;}
    public GenderDto? Gender {get; set;}
    public List<UnordinaryPersonBaseDto>? InteractionsWithUnordinary{get; set;}
}

public class OrdinaryPersonDetailDto : OrdinaryPersonGeneralDto
{
    public string? AlternateName {get; set;}
    public List<int>? BirthYear {get; set;}
    public List<int>? DeathYear {get; set;}
    public int? ProbableBirthYear {get; set;}
    public int? ProbableDeathYear {get; set;}
    public string? Description {get; set;}
    public List<ReligionDto>? FormerReligion {get; set;}
    public string? ReligionExplanation{get; set;}
    public string? ProfessionExplanation{get; set;}
    public string? InterestingFeature{get; set;}
    public string? InteractionWithOrdinaryExplanation{get; set;}
    public string? InteractionWithUnordinaryExplanation{get; set;}
    public string? Biography{get; set;}
    public string? DepictionInTheSource{get; set;}
    public string? ExplanationOfEthnicity{get; set;}
    public List<OrdinaryPersonBaseDto>? InteractionsWithOrdinaryA{get; set;}
    public CityBaseDto? BackgroundCity {get; set;}
}

public class OrdinaryPersonCreateRequest
{
    [Required]
    public string Name { get; set; }
    public string? Religion {get; set;}
    public string? Ethnicity {get; set;}
    public string? Profession {get; set;}
    public string? Location {get; set;}
    public List<int>? Sources {get; set;}
    public string? Gender {get; set;}
    public string? AlternateName {get; set;}
    public List<int>? BirthYear {get; set;}
    public List<int>? DeathYear {get; set;}
    public int? ProbableBirthYear {get; set;}
    public int? ProbableDeathYear {get; set;}
    public string? Description {get; set;}
    public List<string>? FormerReligion {get; set;}
    public string? ReligionExplanation{get; set;}
    public string? ProfessionExplanation{get; set;}
    public string? InterestingFeature{get; set;}
    public string? InteractionWithOrdinaryExplanation{get; set;}
    public string? InteractionWithUnordinaryExplanation{get; set;}
    public string? Biography{get; set;}
    public string? DepictionInTheSource{get; set;}
    public string? ExplanationOfEthnicity{get; set;}
    public List<int>? InteractionsWithOrdinaryA{get; set;}
    public List<int>? InteractionsWithUnordinary{get; set;}
    public string? BackgroundCity {get; set;}  
}

public class OrdinaryPersonUpdateRequest
{
    public string? Name { get; set; }
    public string? Religion {get; set;}
    public string? Ethnicity {get; set;}
    public string? Profession {get; set;}
    public string? Location {get; set;}
    public List<int>? Sources {get; set;}
    public string? Gender {get; set;}
    public string? AlternateName {get; set;}
    public List<int>? BirthYear {get; set;}
    public List<int>? DeathYear {get; set;}
    public int? ProbableBirthYear {get; set;}
    public int? ProbableDeathYear {get; set;}
    public string? Description {get; set;}
    public List<string>? FormerReligion {get; set;}
    public string? ReligionExplanation{get; set;}
    public string? ProfessionExplanation{get; set;}
    public string? InterestingFeature{get; set;}
    public string? InteractionWithOrdinaryExplanation{get; set;}
    public string? InteractionWithUnordinaryExplanation{get; set;}
    public string? Biography{get; set;}
    public string? DepictionInTheSource{get; set;}
    public string? ExplanationOfEthnicity{get; set;}
    public List<int>? InteractionsWithOrdinaryA{get; set;}
    public List<int>? InteractionsWithUnordinary{get; set;}
    public string? BackgroundCity {get; set;}  
}

public class OrdinaryPersonFilterDto 
{
    public string Name {get; set;}
    public int? Religion {get; set;}
    public int? Ethnicity {get; set;}
    public int? Profession {get; set;}
    public int? Location {get; set;}
    public List<int>? Sources {get; set;}
    public int? Gender {get; set;}
    public List<int>? InteractionsWithUnordinary{get; set;}

}

public class OrdinaryPersonFilterResponseDto : OrdinaryPersonGeneralDto 
{
    public string? AlternateName {get; set;}
}

public class OrdinaryPersonGraphDto: OrdinaryPersonBaseDto 
{
    public int? Religion {get; set;}
    public int? Ethnicity {get; set;}
    public int? Profession {get; set;}
    public int? Location {get; set;}
    public List<int>? Sources {get; set;}
    public int? Gender {get; set;}
    public List<int>? InteractionsWithUnordinary{get; set;}
    public List<int>? FormerReligion {get; set;}
    public List<int>? InteractionsWithOrdinaryA{get; set;}
}