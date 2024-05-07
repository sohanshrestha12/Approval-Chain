using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApprovalChain.Models
{
    public class DocumentRecommender
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [ForeignKey("Documents")]
        public int DocumentId { get; set; }
        public Documents Document { get; set; }
        [ForeignKey("Employee")]
        public int EmployeeId { get; set; }
        public Employee employee { get; set; }
        public string Status {  get; set; }
        public int Order {  get; set; }
    }
}
