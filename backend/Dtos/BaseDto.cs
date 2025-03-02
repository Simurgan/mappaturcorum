using System.ComponentModel.DataAnnotations;

namespace Mappa.Dtos;

public class BaseDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class CreateRequest
{
    [Required]
    public string Name { get; set; }
}

public class UpdateRequest
{
    [Required]
    public string Name { get; set; }
}
