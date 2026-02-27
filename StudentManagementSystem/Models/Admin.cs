using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class Admin
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string FullName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Phone { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
