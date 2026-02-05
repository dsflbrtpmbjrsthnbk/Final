using System.ComponentModel.DataAnnotations;

namespace UserManagementApp.Models
{
    public class User
    {
       
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите email")]
        [EmailAddress(ErrorMessage = "Неверный формат email")]
        [StringLength(255)]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        public DateTime? LastLoginTime { get; set; }

        public DateTime RegistrationTime { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(20)]
        public string Status { get; set; } = "unverified";

        [StringLength(100)]
        public string? EmailVerificationToken { get; set; }

        public bool CanLogin()
        {
            return Status != "blocked";
        }

        public bool IsBlocked()
        {
            return Status == "blocked";
        }
        public bool IsVerified()
        {
            return Status == "active";
        }
    }
}
