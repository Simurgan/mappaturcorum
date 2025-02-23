using System.ComponentModel.DataAnnotations;

namespace Mappa.Dtos;

public class CityBaseDto
{
    public int Id { get; set; }
    public string? AsciiName { get; set; }
    public string Name {get; set;}
}

public class CityGeneralDto : CityBaseDto {}

public class CityDetailDto : CityGeneralDto
{
    public string? GeoNamesId {get; set;}
    public List<string>? AlternateNames {get; set;}
    public string? CountryCode {get; set;}
    public double? Latitude {get; set;}
    public double? Longitude {get; set;}
    // OrdinaryPerson Ids
    public List<OrdinaryPersonBaseDto>? LocationOf {get; set;}
    public List<OrdinaryPersonBaseDto>? BackgroundCityOf {get; set;}
    // UnordinaryPerson Ids
    public List<UnordinaryPersonBaseDto>? BirthPlaceOf {get; set;}
    public List<UnordinaryPersonBaseDto>? DeathPlaceOf {get; set;}
    // WrittenSource Ids
    public List<WrittenSourceBaseDto>? SourcesMentioningTheCity {get; set;}
    public List<WrittenSourceBaseDto>? SourcesWrittenInTheCity {get; set;}
}

public class CityCreateRequest
{
    [Required]
    public string Name {get; set;}
    public string? AsciiName { get; set; }
    public string? GeoNamesId { get; set; }
    public List<string>? AlternateNames { get; set; }
    public string? CountryCode { get; set; }
    public double? Latitude {get; set;}
    public double? Longitude {get; set;}
}

public class CityUpdateRequest
{
    public string? AsciiName { get; set; }
    public string? Name {get; set;}
    public string? GeoNamesId { get; set; }
    public List<string>? AlternateNames { get; set; }
    public string? CountryCode { get; set; }
    public double? Latitude {get; set;}
    public double? Longitude {get; set;}
}

public class CityFilterDto
{
    // This includes both Name,AsciiName and AlternateNames
    public string? Name { get; set; }
    // OrdinaryPerson Ids
    public List<int>? LocationOf {get; set;}
    public List<int>? BackgroundCityOf {get; set;}
    // UnordinaryPerson Ids
    public List<int>? BirthPlaceOf {get; set;}
    public List<int>? DeathPlaceOf {get; set;}
    // WrittenSource Ids
    public List<int>? SourcesMentioningTheCity {get; set;}
    public List<int>? SourcesWrittenInTheCity {get; set;}
}

public class CityFilterResponseDto : CityDetailDto {}

public class CityMapDto
{
    public int Id { get; set; }
    public string? AsciiName { get; set; }
    public string Name {get; set;}
    public double? Latitude {get; set;}
    public double? Longitude {get; set;}
    // OrdinaryPerson Ids
    public int? NumberOfLocationOf {get; set;}
    public int? NumberOfBackgroundCityOf {get; set;}
    // UnordinaryPerson Ids
    public int? NumberOfBirthPlaceOf {get; set;}
    public int? NumberOfDeathPlaceOf {get; set;}
    // WrittenSource Ids
    public int? NumberOfSourcesMentioningTheCity {get; set;}
    public int? NumberOfSourcesWrittenInTheCity {get; set;}
}