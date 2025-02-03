using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Mappa.Entities;

public class SecondarySource
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    [Required]
    public string[] AlternateNames {get; set;}
    public string? Author {get; set;}
    public string? Topic {get; set;}
    public DateOnly? YearWritten {get; set;}
    public string? University {get; set;}
    public string? BibliographyInformation {get; set;}
    public string? OtherInformation {get; set;}
    [Required]
    public Type Type {get; set;}
    [Required]
    public Language Language {get; set;}
    [Required]
    public List<Language> TranslatedLanguages {get; set;}


}