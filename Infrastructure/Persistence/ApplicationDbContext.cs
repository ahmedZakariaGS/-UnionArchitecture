
using Application.Interfaces.Repositories.Base;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
 
namespace Persistence.Contexts
{
    public partial class ApplicationDbContext : DbContext, IDbContext
    {
        public virtual DbSet<User> users { get; set; }
  
       
  
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

      

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            //modelBuilder.Ignore<UserLoginData>();

            OnModelCreatingPartial(modelBuilder);

        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
