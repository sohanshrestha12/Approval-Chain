using ApprovalChain.Data;
using ApprovalChain.Models;

namespace ApprovalChain.Repository
{
    public class ApprovalRepo : IApprovalRepo
    {
        private readonly ApplicationDbContext _context;
        public ApprovalRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Approval> GetAll()
        {
            return _context.approvals.ToList();
        }
    }
}
