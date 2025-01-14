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
    public string[] AlternateNames {get; set;}
    public string? Author {get; set;}
    public DateOnly? YearWritten {get; set;}
    [Required]
    public string[] KnownCopies {get; set;}
    [Required]
    public string[] SurvivedCopies {get; set;}
    public string? LibraryInformation {get; set;}
    public string? OtherInformation {get; set;}
    public string? RemarkableWorksOnTheBook {get; set;}
    public string? Image {get; set;}
    public bool Patronage {get; set;}
    [Required]
    public Genre Genre {get; set;}
    [Required]
    public Language Language {get; set;}
    [Required]
    public List<Language> TranslatedLanguages {get; set;}


}