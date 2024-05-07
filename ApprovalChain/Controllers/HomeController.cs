using ApprovalChain.Data;
using ApprovalChain.Models;
using ApprovalChain.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace ApprovalChain.Controllers
{
    public class HomeController : Controller
    {
        public readonly IEmployeeRepo _employeeRepo;
        public readonly IApprovalRepo _approvalRepo;
        public HomeController(IEmployeeRepo employeeRepo,IApprovalRepo approvalRepo) {
            _employeeRepo = employeeRepo;
            _approvalRepo = approvalRepo;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            var userId = HttpContext.Session.GetInt32("employeeId");
            var employee = HttpContext.Session.GetString("employee");
            if(employee == null)
            {
                return RedirectToAction("Index");
            }
            var allEmployee = _employeeRepo.GetAll();
            var allApproval = _approvalRepo.GetAll();
            ViewBag.Employee = new SelectList(allEmployee,"Employee_Id","Employee_Name");
            ViewBag.Approval = new SelectList(allApproval, "Approval_Id", "Approval_Name");
            var docDetails = _employeeRepo.GetDocumentsByUser(userId.Value);
            ViewBag.recvDocs = _employeeRepo.ReceivedDocs(userId.Value);
            return View(docDetails);
        }
        [HttpPost]
        public IActionResult Create(Approval Approval,List<Employee> Employee,Documents Documents) {
            var currentUserId = HttpContext.Session.GetInt32("employeeId");
            _employeeRepo.Create(Approval, Employee, Documents,currentUserId.Value);
            return RedirectToAction("Create");
        }
        public IActionResult Approve(int docId)
        {
            var userId = HttpContext.Session.GetInt32("employeeId");
            _employeeRepo.ApproveDoc(docId,userId.Value);
            return RedirectToAction("Create");
        }

        public IActionResult Admin()
        {
            var adminUserId = HttpContext.Session.GetInt32("employeeId");
            var docs = _employeeRepo.ReceivedApprovalDocs(adminUserId.Value);
            return View(docs);
        }

        public IActionResult ApproveAdmin(int docId)
        {
            var adminUserId = HttpContext.Session.GetInt32("employeeId");

            _employeeRepo.ApproveApproval(docId,adminUserId.Value);
            return RedirectToAction("Admin");
        }


        public IActionResult RejectRecommender(int docId,string rejectReason) {
            var userId = HttpContext.Session.GetInt32("employeeId");
            _employeeRepo.RejectDocument(docId,rejectReason,userId.Value);

            return RedirectToActionPermanent("Create");
        }  
        public IActionResult RejectAdmin(int docId,string rejectReason) {
            var userId = HttpContext.Session.GetInt32("employeeId");
            _employeeRepo.RejectDocumentbyAdmin(docId,rejectReason,userId.Value);

            return RedirectToActionPermanent("Admin");
        }

    }
}
