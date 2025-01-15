using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using TaskManagement.DataAccess.Entities;

namespace TaskManagement.DataAccess.Data
{
    public class AppDbContext : IdentityDbContext<User>
    {
        private readonly IHttpContextAccessor _contextAccessor;

        public AppDbContext(DbContextOptions<AppDbContext> options, IHttpContextAccessor contextAccessor)
            : base(options)
        {
            _contextAccessor = contextAccessor;
        }

        public DbSet<Tasks> Tasks { get; set; } = null!;
        public DbSet<Team> Teams { get; set; } = null!;
        public DbSet<TeamUser> TeamUsers { get; set; } = null!;
        public DbSet<Comments> Comments { get; set; } = null!;
        public DbSet<Attachment> Attachments { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure Tasks.CreatedBy relationship
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.CreatedBy)
                .WithMany(u => u.AssignedTasks)  
                .HasForeignKey(t => t.CreateById)
                .OnDelete(DeleteBehavior.Restrict); 
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.UpdatedBy)
                .WithMany()  
                .HasForeignKey(t => t.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Tasks>()
           .HasOne(t => t.User)  
           .WithMany()  
           .HasForeignKey(t => t.AssignedToUserId)  // Foreign key property for the User relationship
           .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete if required

            // Configure Task to Team relationship
            modelBuilder.Entity<Tasks>()
                .HasOne(t => t.Team)  
                .WithMany()  
                .HasForeignKey(t => t.AssignedToTeamId)  // Foreign key property for the Team relationship
                .OnDelete(DeleteBehavior.Restrict);  // Prevent cascading delete if required

            modelBuilder.Entity<User>()
          .OwnsMany(u => u.RefreshTokens, rt =>
          {
              rt.Property(r => r.Token).HasMaxLength(500); 

          });
        }


        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var entries = ChangeTracker.Entries<AuditLogging>();
            foreach (var entry in entries)
            {
                var userId = _contextAccessor.HttpContext?.User.FindFirstValue("uid");
                if (userId != null)
                {
                    if (entry.State == EntityState.Added)
                    {
                        entry.Property(x => x.CreateById).CurrentValue = userId;
                        entry.Property(x => x.CreatedOn).CurrentValue = DateTime.UtcNow;
                    }
                    else if (entry.State == EntityState.Modified)
                    {
                        entry.Property(x => x.UpdatedById).CurrentValue = userId;
                        entry.Property(x => x.UpdatedOn).CurrentValue = DateTime.UtcNow;
                    }
                }
            }
            return base.SaveChangesAsync(cancellationToken);
        }
    }
}
