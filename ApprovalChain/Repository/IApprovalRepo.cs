using ApprovalChain.Models;

namespace ApprovalChain.Repository
{
    public interface IApprovalRepo
    {
        public List<Approval> GetAll();   
    }
}
