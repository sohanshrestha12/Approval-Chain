using ApprovalChain.Models;
using System.Reflection.Metadata;

namespace ApprovalChain.Repository
{
    public interface IEmployeeRepo
    {
        public Employee Authenticate(string Employee_Name,string Employee_Password);
        public List<Employee> GetAll();

        public void Create(Approval Approval, List<Employee> Employee, Documents Document,int currentUserId);

        public List<Documents> GetDocumentsByUser(int userId);

        public List<Documents> ReceivedDocs(int userId);
        public void ApproveDoc(int docId, int userId);

        public Approval AuthhenticateApproval(string email, string password);
        public List<Documents> ReceivedApprovalDocs(int approvalId);
        public void ApproveApproval(int docId, int userId);
        public void RejectDocument(int docId, string rejectReason,int userId);
        public void RejectDocumentbyAdmin(int docId, string rejectReason,int userId);
    }
}
