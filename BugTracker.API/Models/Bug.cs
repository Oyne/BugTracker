using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BugTracker.API.Models
{
    [Table("bug")]
    public class Bug
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        [Required]
        public required string Name { get; set; }

        [Column("description")]
        [Required]
        public required string Description { get; set; }

        [Column("priority_id")]
        public int? PriorityId { get; set; }

        [ForeignKey("PriorityId")]
        public Priority? Priority { get; set; }

        [Column("status_id")]
        public int? StatusId { get; set; }

        [ForeignKey("StatusId")]
        public Status? Status { get; set; }

        [Column("category_id")]
        public int? CategoryId { get; set; }

        [ForeignKey("CategoryId")]
        public Category? Category { get; set; }

        [Column("creation_date_time")]
        public DateTime CreationDate { get; set; }

        [Column("edit__date_time")]
        public DateTime EditDateTime { get; set; }

        [Column("logged_time")]
        public TimeSpan LoggedTime { get; set; }
    }
}
