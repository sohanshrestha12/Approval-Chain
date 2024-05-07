using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprovalChain.Models
{
    public class DocumentApproval
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Documents")]
        public int DocumentId { get; set; }
        [ForeignKey("Approval")]
        public int ApprovalId { get; set;}
        public Documents Document { get; set; }
        public Approval Approval { get; set; }
        public string Status { get; set; }
    }
}
