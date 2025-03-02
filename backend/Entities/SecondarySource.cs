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
    public string Name {get; set;}
    public List<string>? AlternateNames {get; set;}
    public string? Author {get; set;}
    public string? Topic {get; set;}
    public int? YearWritten {get; set;}
    public string? University {get; set;}
    public string? BibliographyInformation {get; set;}
    public string? OtherInformation {get; set;}
    public Type? Type {get; set;}
    public Language? Language {get; set;}
    public List<Language>? TranslatedLanguages {get; set;}
}