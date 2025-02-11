using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Mappa.Entities;

public class UnordinaryPerson : Person
{
    [Required]
    public List<OrdinaryPerson>? InteractionsWithOrdinary{get; set;}
    [Required]
    public List<UnordinaryPerson>? InteractionsWithUnordinaryA{get; set;}
    [Required]
    public List<UnordinaryPerson>? InteractionsWithUnordinaryB{get; set;}
    public City? BirthPlace {get; set;}
    public City? DeathPlace {get; set;}

}

public class IntraUnordinary
{
    public int PersonIdA {get; set;}
    public int PersonIdB {get; set;}
}