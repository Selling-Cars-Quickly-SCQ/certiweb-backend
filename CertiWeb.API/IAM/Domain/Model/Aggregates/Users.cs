using System.ComponentModel.DataAnnotations;

namespace CertiWeb.API.IAM.Domain.Model.Aggregates;
{
    /// <summary>
    /// User entity for authentication and authorization
    /// </summary>
    public class User
    {
        /// <summary>
        /// User unique identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User full name
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// User email address (unique)
        /// </summary>
        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; } = string.Empty;

        /// <summary>
        /// Hashed password
        /// </summary>
        [Required]
        public string PasswordHash { get; set; } = string.Empty;

        /// <summary>
        /// Date when the user was created
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Date when the user was last updated
        /// </summary>
        public DateTime UpdatedAt { get; set; }
    }
}