using System.Threading.Tasks.Dataflow;
using Mappa.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Mappa.Db;

public class AppDbContext : IdentityDbContext<User>
{
    public DbSet<City> Cities { get; set; }
    public DbSet<Ethnicity> Ethnicities { get; set; }
    public DbSet<Gender> Genders { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Language> Languages { get; set; }
    public DbSet<OrdinaryPerson> OrdinaryPersons { get; set; }
    public DbSet<Profession> Professions { get; set; }
    // public DbSet<Relation> Relations { get; set; }
    public DbSet<Religion> Religions { get; set; }
    public DbSet<SecondarySource> SecondarySources { get; set; }
    public DbSet<Entities.Type> Types { get; set; }
    public DbSet<UnordinaryPerson> UnordinaryPersons { get; set; }
    public DbSet<WrittenSource> WrittenSources { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<OrdinaryPerson>()
            .HasMany(e => e.InteractionsWithOrdinaryA)
            .WithMany(e => e.InteractionsWithOrdinaryB)
            .UsingEntity<IntraOrdinary>(
                e => e.HasOne<OrdinaryPerson>().WithMany().HasForeignKey(e => e.PersonIdA),
                e => e.HasOne<OrdinaryPerson>().WithMany().HasForeignKey(e => e.PersonIdB)    
            );
            
        builder.Entity<UnordinaryPerson>()
            .HasMany(e => e.InteractionsWithUnordinaryA)
            .WithMany(e => e.InteractionsWithUnordinaryB)
            .UsingEntity<IntraUnordinary>(
                e => e.HasOne<UnordinaryPerson>().WithMany().HasForeignKey(e => e.PersonIdA),
                e => e.HasOne<UnordinaryPerson>().WithMany().HasForeignKey(e => e.PersonIdB)    
            );

        builder.Entity<OrdinaryPerson>()
            .HasMany(o => o.InteractionsWithUnordinary)
            .WithMany(u => u.InteractionsWithOrdinary);

        builder.Entity<WrittenSource>().HasMany(ws => ws.OrdinaryPersons)
            .WithMany(p => p.Sources);

        builder.Entity<WrittenSource>().HasMany(ws => ws.UnordinaryPersons)
            .WithMany(p => p.Sources);

        builder.Entity<Religion>().HasMany(ws => ws.FormerOrdinaryPersons)
            .WithMany(p => p.FormerReligion);

        builder.Entity<Religion>().HasMany(ws => ws.FormerUnordinaryPersons)
            .WithMany(p => p.FormerReligion);

        builder.Entity<OrdinaryPerson>().HasOne(op => op.Religion)
            .WithMany();

        builder.Entity<UnordinaryPerson>().HasOne(up => up.Religion)
            .WithMany();
    }
}