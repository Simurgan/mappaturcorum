using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Mappa.Entities;

public abstract class Person
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    [Required]
    public string Name {get; set;}
    public string? AlternateName {get; set;}
    public string? Depiction {get; set;}
    public DateOnly? BirthYear {get; set;}
    public DateOnly? DeathYear {get; set;}
    public string? Description {get; set;}
    public Ethnicity? Ethnicity {get; set;}
    public Religion? Religion {get; set;}
    public Religion? FormerReligion {get; set;}
    public Profession? Profession {get; set;}
    public Gender? Gender {get; set;}

}