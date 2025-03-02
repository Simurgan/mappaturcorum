using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Identity;

namespace Mappa.Entities;

public class Religion : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id {get; set;}
    [Required]
    public string Name {get; set;}
    public List<OrdinaryPerson>? FormerOrdinaryPersons {get; set;} 
    public List<UnordinaryPerson>? FormerUnordinaryPersons {get; set;}
}