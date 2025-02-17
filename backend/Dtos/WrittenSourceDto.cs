using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks.Dataflow;
using Mappa.Entities;
using Microsoft.OpenApi.Expressions;

namespace Mappa.Dtos;

public class WrittenSourceBaseDto
{
    public int Id {get; set;}
    public List<string>? AlternateNames {get; set;}
    
}

public class WrittenSourceGeneralDto : WrittenSourceBaseDto
{
    public string? Author {get; set;}
    public List<int>? YearWritten {get; set;}
    public GenreDto? Genre {get; set;}
    public LanguageDto? Language {get; set;}
}

public class WrittenSourceDetailDto : WrittenSourceGeneralDto
{
    public List<string>? KnownCopies {get; set;}
    public List<string>? SurvivedCopies {get; set;}
    public string? LibraryInformation {get; set;}
    public string? OtherInformation {get; set;}
    public string? RemarkableWorksOnTheBook {get; set;}
    public string? Image {get; set;}
    public List<LanguageDto>? TranslatedLanguages {get; set;}
    public List<CityBaseDto>? CitiesMentionedByTheSource {get; set;}
    public List<CityBaseDto>? CitiesWhereSourcesAreWritten {get; set;}
    public List<OrdinaryPersonBaseDto>? OrdinaryPersons {get; set;} 
    public List<UnordinaryPersonBaseDto>? UnordinaryPersons {get; set;} 
    public int? ProbableYearWritten {get; set;}
}

public class WrittenSourceCreateRequest
{
    [Required]
    public List<string>? AlternateNames {get; set;}
    public string? Author {get; set;}
    public List<int>? YearWritten {get; set;}
    public string? Genre {get; set;}
    public string? Language {get; set;}
    public List<string>? KnownCopies {get; set;}
    public List<string>? SurvivedCopies {get; set;}
    public string? LibraryInformation {get; set;}
    public string? OtherInformation {get; set;}
    public string? RemarkableWorksOnTheBook {get; set;}
    public string? Image {get; set;}
    public List<string>? TranslatedLanguages {get; set;}
    public List<string>? CitiesMentionedByTheSource {get; set;}
    public List<string>? CitiesWhereSourcesAreWritten {get; set;}
    
}

public class WrittenSourceUpdateRequest
{
    public List<string>? AlternateNames {get; set;}
    public string? Author {get; set;}
    public List<int>? YearWritten {get; set;}
    public string? Genre {get; set;}
    public string? Language {get; set;}
    public List<string>? KnownCopies {get; set;}
    public List<string>? SurvivedCopies {get; set;}
    public string? LibraryInformation {get; set;}
    public string? OtherInformation {get; set;}
    public string? RemarkableWorksOnTheBook {get; set;}
    public string? Image {get; set;}
    public List<string>? TranslatedLanguages {get; set;}
    public List<string>? CitiesMentionedByTheSource {get; set;}
    public List<string>? CitiesWhereSourcesAreWritten {get; set;}
}

public class WrittenSourceFilterDto
{
    public int? Genre {get; set;}
    public List<int>? YearWritten {get; set;}
    public string? Author {get; set;}
    public int? Language {get; set;}
    public List<int>? OrdinaryPersons {get; set;}
    public List<int>? UnordinaryPersons {get; set;}
}

public class WrittenSourceFilterResponseDto : WrittenSourceGeneralDto {}

public class WrittenSourceGraphDto : WrittenSourceBaseDto {}