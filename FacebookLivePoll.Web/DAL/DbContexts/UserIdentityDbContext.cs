using System.Data.Entity;
using FacebookLivePoll.Web.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using MySql.Data.Entity;

namespace FacebookLivePoll.Web.DAL.DbContexts
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class UserIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public UserIdentityDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {

        }

        public static UserIdentityDbContext Create()
        {
            return new UserIdentityDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<IdentityRole>()
                .Property(c => c.Name)
                .HasMaxLength(128)
                .IsRequired();

            modelBuilder.Entity<ApplicationUser>()
                .ToTable("AspNetUsers")
                .Property(c => c.UserName)
                .HasMaxLength(128)
                .IsRequired();
        }
    }
}