using System.ComponentModel.DataAnnotations;

namespace Mission08_Group1.Models
{
    public class Task
    {
        [Key]
        [Required]
        public int TaskId { get; set; }
        
        [Required]
        public string Task { get; set; } 
        public string DueDate { get; set; }

        [Required]
        public int Quadrant { get; set; }
        public int CategoryId { get; set; }
        public int Completed { get; set; }

        public Category Category { get; set; } // Navigation property
    }
}
