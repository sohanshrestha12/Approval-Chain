using ApprovalChain.Models;
using Microsoft.EntityFrameworkCore;

namespace ApprovalChain.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) { }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Documents> documents { get; set; }
        public DbSet<Approval> approvals { get; set; }
        public DbSet<DocumentApproval> documentApprovals { get; set; }
        public DbSet<DocumentRecommender> documentRecommenders { get; set; }


    }
}
