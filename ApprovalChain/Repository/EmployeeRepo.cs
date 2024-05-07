using ApprovalChain.Data;
using ApprovalChain.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace ApprovalChain.Repository
{
    public class EmployeeRepo : IEmployeeRepo
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepo(ApplicationDbContext context)
        {
            _context = context;
        }
        public Employee Authenticate(string Employee_Email, string Employee_Password)
        {
            var IsEmailFound = _context.employees.Where(x => x.Employee_Email == Employee_Email).FirstOrDefault();
            if (IsEmailFound == null)
            {
                return null;
            }
            if(IsEmailFound.Employee_Password !=Employee_Password) { return null; }
            return IsEmailFound;
        }

        public List<Employee> GetAll()
        {
            return _context.employees.ToList();
        }
        public void Create(Approval Approval, List<Employee> Employee, Documents Documents, int currentUserId)
        {
            //document
            var newDoc = new Documents
            {
                Document_Name = Documents.Document_Name,
                Document_Comment = Documents.Document_Comment,
                Employee_Id = currentUserId,
                Status = "pending"
            };

            _context.documents.Add(newDoc);
            _context.SaveChanges();

            //approvaldocument
            var documentApproval = new DocumentApproval
            {
                DocumentId = newDoc.Document_Id,
                ApprovalId = Approval.Approval_Id,
                Status = "pending"
            };
            _context.documentApprovals.Add(documentApproval);
            //documentrecommender
            int order = 1;
            foreach (var employee in Employee)
            {
                var documentRecommender = new DocumentRecommender
                {
                    DocumentId = newDoc.Document_Id,
                    EmployeeId = employee.Employee_Id,
                    Order = order,
                    Status= "pending"
                };
                _context.documentRecommenders.Add(documentRecommender);
                //pass the doc
                if(order == 1)
                {
                    var updatedDoc = _context.documents.FirstOrDefault(d => d.Document_Id == newDoc.Document_Id);
                    if (updatedDoc != null)
                    {
                        updatedDoc.currentEmpDoc = employee.Employee_Id;
                        _context.SaveChanges();
                    }
                }
                order++;
            }




            _context.SaveChanges();
        }



        public List<Documents> GetDocumentsByUser(int userId)
        {
            var documents = _context.documents.Include(d => d.Employee).Include(d => d.DocumentRecommender).ThenInclude(dr=>dr.employee).Include(d=>d.DocumentApproval).ThenInclude(da=>da.Approval).Where(d=>d.Employee_Id == userId).ToList();
            return documents;
        }

        public List<Documents> ReceivedDocs(int userId)
        {
            /*         var recvDocs = _context.documents.Include(x => x.Employee).Where(x => x.currentEmpDoc == userId).ToList();*/
            /* var recvDocs = _context.documents.Where(x => x.DocumentRecommender.Any(x => x.EmployeeId == userId)).Include(x => x.Employee).Include(x => x.DocumentRecommender).ToList().Select(doc =>
             {
                 doc.DocumentRecommender = doc.DocumentRecommender.Where(x => x.EmployeeId == userId).ToList();
                 return doc;
             }).ToList();*/
            var recvDocs = _context.documents
                    .Where(doc =>
                        doc.currentEmpDoc == userId ||
                        doc.DocumentRecommender.Any(dr => dr.EmployeeId == userId && (dr.Status == "Approved" || dr.Status == "rejected" )))
                    .Include(x => x.Employee).Include(x => x.DocumentRecommender).ToList().Select(doc =>
                    {
                        doc.DocumentRecommender = doc.DocumentRecommender.Where(x => x.EmployeeId == userId).ToList();
                        return doc;
                    }).ToList();
           /* var employeedoc = _context.documentRecommenders.Where(x => x.EmployeeId == userId);*/
            return recvDocs;
        }

        public void ApproveDoc(int docId,int userId)
        {
            //approve in DocumentRecommender
            var document = _context.documents.Include(d=>d.DocumentRecommender).Where(x=>x.Document_Id==docId).FirstOrDefault();
            var docToBeApproved = document.DocumentRecommender.Where(x=>x.DocumentId==document.Document_Id && x.EmployeeId == userId).FirstOrDefault();
            if (docToBeApproved!=null)
            {
                docToBeApproved.Status = "approved";
            }
            int currentOrder = docToBeApproved.Order;
            int nextOrder = currentOrder + 1;
            document.currentEmpDoc = document.DocumentRecommender.Where(x=>x.Order == nextOrder).Select(d=>d.EmployeeId).FirstOrDefault();
            if(document.currentEmpDoc == null)
            {
                document.currentEmpDoc = 0;
            }
            _context.SaveChanges();
        }

        public Approval AuthhenticateApproval(string email, string password)
        {
            var isAdmin = _context.approvals.Where(x => x.Approval_Email == email && x.Approval_Password == password).FirstOrDefault();
            return isAdmin;
        }

        public List<Documents> ReceivedApprovalDocs(int approvalId)
        {
            var receivedDocs = _context.documents.Include(x => x.Employee).Include(x => x.DocumentRecommender).ThenInclude(dr => dr.employee).Where(x=>((x.currentEmpDoc == 0 && x.DocumentApproval.ApprovalId == approvalId  && x.DocumentApproval.Status =="pending") || (x.DocumentApproval.ApprovalId == approvalId && (x.DocumentApproval.Status=="approved" || x.DocumentApproval.Status == "rejected") ) )).ToList();
            return receivedDocs;
        }

        public void ApproveApproval(int docId, int userId)
        {
            var document = _context.documents.Where(x=>x.Document_Id == docId).Include(x=>x.DocumentApproval).FirstOrDefault();
            if(document != null) {
                document.Status = "approved";
                document.DocumentApproval.Status = "approved";
            }
            _context.SaveChanges();
            
        }

        public void RejectDocument(int docId, string rejectReason,int userId)
        {
            var rejectedDoc = _context.documents.Where(x => x.Document_Id == docId).Include(x=>x.DocumentRecommender).FirstOrDefault();
            if(rejectedDoc != null)
            {
                rejectedDoc.Status = "rejected";
                rejectedDoc.currentEmpDoc = -1;
                rejectedDoc.Reject_Review= rejectReason;
               foreach(var data in rejectedDoc.DocumentRecommender)
                {
                    if(userId == data.EmployeeId && docId == data.DocumentId)
                    {
                        data.Status = "rejected";
                    }
                }
               _context.SaveChanges();  
            }
        }

        public void RejectDocumentbyAdmin(int docId, string rejectReason, int userId)
        {
            var rejectedDoc = _context.documents.Where(x => x.Document_Id == docId).Include(x => x.DocumentRecommender).Include(x=>x.DocumentApproval).FirstOrDefault();
            if (rejectedDoc != null)
            {
                rejectedDoc.Status = "rejected";
                rejectedDoc.currentEmpDoc = -1;
                rejectedDoc.Reject_Review = rejectReason;
                rejectedDoc.DocumentApproval.Status = "rejected";
                _context.SaveChanges();
            }
        }
    }
}
