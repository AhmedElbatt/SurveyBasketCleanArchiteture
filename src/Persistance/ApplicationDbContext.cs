using Domain.Common;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Reflection;
using System.Security.Claims;

namespace Persistance;
public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor) : IdentityDbContext<ApplicationUser>(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public DbSet<Poll> Polls { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<VoteAnswer> VoteAnswers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var cascadeFkeys = modelBuilder.Model
                                        .GetEntityTypes()
                                        .SelectMany(x => x.GetForeignKeys())
                                        .Where(x => !x.IsOwnership && x.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFkeys)
            fk.DeleteBehavior = DeleteBehavior.Restrict;

        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var loggedInUser = _httpContextAccessor.HttpContext?.User.FindFirstValue(ClaimTypes.NameIdentifier)!;
        var entries = ChangeTracker.Entries<AuditableEntity>();
        foreach (var entry in entries)
        {
            if (entry.State == EntityState.Added)
            {
                entry.Entity.CreatedAt = DateTime.UtcNow;
                entry.Entity.CreatedById = loggedInUser;
            }

            if (entry.State == EntityState.Modified)
            {
                if (entry.Entity.IsDeleted)
                {
                    entry.Entity.DeletedAt = DateTime.UtcNow;
                    entry.Entity.DeletedById = loggedInUser;
                }
                else
                {
                    entry.Entity.LastUpdatedAt = DateTime.UtcNow;
                    entry.Entity.LastUpdatedById = loggedInUser;
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}
