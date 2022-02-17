using Microsoft.EntityFrameworkCore;

namespace IdeasAndInvestors.Models
{
    public class IdeasAndInvestorsDbContext : DbContext
    {
        //Dependency Injection
        public IdeasAndInvestorsDbContext(DbContextOptions<IdeasAndInvestorsDbContext>
            options) : base(options)
        {

        }
        public DbSet<PersonMaster> PersonMasters { get; set; }
        public DbSet<IdeaMaster> IdeaMasters { get; set; }
        public DbSet<CategoryMaster> CategoryMasters { get; set; }
        public DbSet<InvestmentMaster> InvestmentMasters { get; set; }
        public DbSet<DonorMaster> DonorMasters { get; set; }
        public DbSet<ComplainMaster> ComplainMasters { get; set; }
        public DbSet<FeedbackMaster> FeedbackMasters { get; set; }
        public DbSet<QuestionMaster> QuestionMasters { get; set; }
    }
}
