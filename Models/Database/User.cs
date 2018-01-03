using System;
using System.ComponentModel.DataAnnotations;

namespace SecureAPI.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(50)]
        public string FirstName { get; set; }
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; set; }
        [Required]
        [MaxLength(200)]
        public string Username { get; set; }
        [Required]
        [MaxLength(64)]
        public string PasswordHash { get; set; }
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime PasswordCreated { get; set; }
        [Required]
        [MaxLength(64)]
        public string PasswordSalt { get; set; }
        [Required]
        public Shared.AccessLevels AccessLevel { get; set; }
    }
}
