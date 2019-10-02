using System.Data.Entity;
using FacebookLivePoll.Web.DAL.Entities;
using MySql.Data.Entity;

namespace FacebookLivePoll.Web.DAL.DbContexts
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class DataDbContext : DbContext
    {
        public DataDbContext() : base("DefaultConnection")
        {
            
        }
        public DbSet<Polls> Polls { get; set; }
    }
}