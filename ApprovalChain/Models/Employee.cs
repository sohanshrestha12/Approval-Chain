using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprovalChain.Models
{
    public class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Employee_Id { get; set; }
        public string Employee_Name { get; set;}
        public string Employee_Email { get; set;}
        public string Employee_Password { get; set;}
        public ICollection<Documents>  Employee_Documents { get; set; }          
    }
}
