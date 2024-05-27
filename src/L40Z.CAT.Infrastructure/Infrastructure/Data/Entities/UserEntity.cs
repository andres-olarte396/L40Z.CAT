using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Entities
{
    /// <summary>
    /// User Entity
    /// </summary>
    public class UserEntity : AuditEntity
    {
        /// <summary>
        /// User Id
        /// </summary>
        [Key]
        public int Id { get; set; }

        /// <summary>
        /// User Name
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// User Email
        /// </summary>
        public required string Email { get; set; }
    }
}
