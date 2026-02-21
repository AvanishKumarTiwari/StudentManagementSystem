using System.ComponentModel.DataAnnotations;

namespace StudentManagementSystem.Models
{
    public class User
    {
        [Key] // auto 
        public int ID { get; set; }

        [Required]
        public string FirstName { get; set; }
        [Required]
        public string Lastname { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public DateTime DoB { get; set; }
        [Required]
        public string Phone { get; set; }
        [Required]
        public string Course { get; set; }
        [Required]
        public int Sem { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
