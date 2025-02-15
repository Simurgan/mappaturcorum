using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Mappa.Entities;

public class WrittenSource
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    [Required]
    public List<string>? AlternateNames {get; set;}
    public string? Author {get; set;}
    public List<int>? YearWritten {get; set;}
    public List<string>? KnownCopies {get; set;}
    public List<string>? SurvivedCopies {get; set;}
    public string? LibraryInformation {get; set;}
    public string? OtherInformation {get; set;}
    public string? RemarkableWorksOnTheBook {get; set;}
    public string? Image {get; set;}
    public Genre? Genre {get; set;}
    public Language? Language {get; set;}
    public List<Language>? TranslatedLanguages {get; set;}
    public List<City>? CitiesMentioningTheSources {get; set;}
    public List<City>? CitiesWhereSourcesAreWritten {get; set;}

}