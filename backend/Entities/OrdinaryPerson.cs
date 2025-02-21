using System.ComponentModel.DataAnnotations;

namespace Mappa.Entities;

public class OrdinaryPerson : Person
{
    public string? ReligionExplanation{get; set;}
    public string? ProfessionExplanation{get; set;}
    public string? InterestingFeature{get; set;}
    public string? InteractionWithOrdinaryExplanation{get; set;}
    public string? InteractionWithUnordinaryExplanation{get; set;}
    public string? Biography{get; set;}
    public string? DescriptionInTheSource{get; set;}
    public string? ExplanationOfEthnicity{get; set;}
    // public Relation? Relation{get; set;}
    [Required]
    public List<OrdinaryPerson>? InteractionsWithOrdinaryA{get; set;}
    [Required]
    public List<OrdinaryPerson>? InteractionsWithOrdinaryB{get; set;}
    [Required]
    public List<UnordinaryPerson>? InteractionsWithUnordinary{get; set;}
    public City? Location {get; set;}
    public City? BackgroundCity {get; set;}
    public List<WrittenSource>? Sources {get; set;}
    public Religion? FormerReligion {get; set;}
    
}

public class IntraOrdinary
{
    public int PersonIdA {get; set;}
    public int PersonIdB {get; set;}
}