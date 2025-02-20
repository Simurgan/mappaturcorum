using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using Microsoft.AspNetCore.Identity;

namespace Mappa.Entities;

public class City
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    [Required]
    public string AsciiName {get; set;}
    public string? Name {get; set;}
    public string? GeoNamesId {get; set;}
    public List<string>? AlternateNames {get; set;}
    public string? CountryCode {get; set;}
    public double? Latitude {get; set;}
    public double? Longitude {get; set;}
    public List<OrdinaryPerson>? LocationOf {get; set;}
    public List<OrdinaryPerson>? BackgroundCityOf {get; set;}
    public List<UnordinaryPerson>? BirthPlaceOf {get; set;}
    public List<UnordinaryPerson>? DeathPlaceOf {get; set;}
    public List<WrittenSource>? SourcesMentioningTheCity {get; set;}
    public List<WrittenSource>? SourcesWrittenInTheCity {get; set;}
}