using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprovalChain.Models
{
    public class Approval
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Approval_Id { get; set; }
        public string Approval_Name { get; set; }
        public string Approval_Email {  get; set; }
        public string Approval_Password { get; set; }
    }
}
