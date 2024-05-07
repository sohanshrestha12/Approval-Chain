using ApprovalChain.Models;
using ApprovalChain.Repository;
using Microsoft.AspNetCore.Mvc;

namespace ApprovalChain.Controllers
{
    public class AuthController : Controller
    {
        private readonly IEmployeeRepo _employeeRepo;
        public AuthController(IEmployeeRepo employeeRepo)
        {
            _employeeRepo = employeeRepo;
        }
        [HttpPost]
        public IActionResult Login(string Employee_Email,string Employee_Password)
        {
            var isAdmin = _employeeRepo.AuthhenticateApproval(Employee_Email,Employee_Password);
            if(isAdmin != null)
            {
                HttpContext.Session.SetInt32("employeeId", isAdmin.Approval_Id);
                HttpContext.Session.SetString("employee", isAdmin.Approval_Name);
                return RedirectToAction("Admin","Home");
            }

            var isEmployee = _employeeRepo.Authenticate(Employee_Email, Employee_Password);
            if(isEmployee !=null)
            {
                HttpContext.Session.SetInt32("employeeId",isEmployee.Employee_Id);
                HttpContext.Session.SetString("employee", isEmployee.Employee_Name);
                return RedirectToAction("Create","Home");
            }
            return RedirectToAction("Index","Home");
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index","Home");
        }
    }
}
