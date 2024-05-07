using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprovalChain.Models
{
    public class Documents
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Document_Id { get; set; }
        public string Document_Name { get; set; }
        public string Document_Comment { get; set; }
        [ForeignKey("Employee")]
        public int Employee_Id { get; set; }
        public Employee Employee { get; set; }
        public string Status { get; set; }
        public string? Reject_Review { get; set; }
        public int? currentEmpDoc { get; set; }

        public ICollection<DocumentRecommender> DocumentRecommender { get; set; }
        public DocumentApproval DocumentApproval { get; set; }

    }
}
