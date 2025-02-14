using System.ComponentModel.DataAnnotations;

namespace Mappa.Dtos;

public class CityBaseDto
{
    public int Id { get; set; }
    public string AsciiName { get; set; }
}

public class CityGeneralDto : CityBaseDto
{
}

public class CityDetailDto : CityGeneralDto
{
    public string? GeoNamesId {get; set;}
    public List<string>? AlternateNames {get; set;}
    public string? CountryCode {get; set;}
}

public class CityCreateRequest
{
    [Required]
    public string AsciiName { get; set; }
    public string? GeoNamesId { get; set; }
    public List<string>? AlternateNames { get; set; }
    public string? CountryCode { get; set; }
}

public class CityUpdateRequest
{
    public string? AsciiName { get; set; }
    public string? GeoNamesId { get; set; }
    public List<string>? AlternateNames { get; set; }
    public string? CountryCode { get; set; }
}

public class CityFilterDto
{
    public List<string>? AlternateNames { get; set; }
}
