using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks.Dataflow;
using Mappa.Entities;
using Microsoft.OpenApi.Expressions;

namespace Mappa.Dtos;

public class SecondarySourceBaseDto
{
    public int Id {get; set;}
    public string Name {get; set;}
    
}

public class SecondarySourceGeneralDto : SecondarySourceBaseDto
{
    public List<string>? AlternateNames {get; set;}
    public string? Author {get; set;}
    public int? YearWritten {get; set;}
    public string? University {get; set;}
    public TypeDto? Type {get; set;}
}

public class SecondarySourceDetailDto : SecondarySourceGeneralDto
{
    public string? Topic {get; set;}
    public string? BibliographyInformation {get; set;}
    public string? OtherInformation {get; set;}
    public LanguageDto? Language {get; set;}
    public List<LanguageDto>? TranslatedLanguages {get; set;}
}

// Field-classes are received as string, but they are stored in the db as classes
public class SecondarySourceCreateRequest
{
    [Required]
    public string Name {get; set;}
    public List<string>? AlternateNames {get; set;}
    public string? Author {get; set;}
    public int? YearWritten {get; set;}
    public string? University {get; set;}
    public string? Type {get; set;}
    public string? Topic {get; set;}
    public string? BibliographyInformation {get; set;}
    public string? OtherInformation {get; set;}
    public string? Language {get; set;}
    public List<string>? TranslatedLanguages {get; set;}
}

public class SecondarySourceUpdateRequest
{
    public string? Name {get; set;}
    public List<string>? AlternateNames {get; set;}
    public string? Author {get; set;}
    public int? YearWritten {get; set;}
    public string? University {get; set;}
    public string? Type {get; set;}
    public string? Topic {get; set;}
    public string? BibliographyInformation {get; set;}
    public string? OtherInformation {get; set;}
    public string? Language {get; set;}
    public List<string>? TranslatedLanguages {get; set;}
}

public class SecondarySourceFilterDto
{
    public int? Type {get; set;}
}

public class SecondarySourceFilterResponseDto : SecondarySourceGeneralDto {}

public class SecondarySourceGraphDto : SecondarySourceBaseDto {}