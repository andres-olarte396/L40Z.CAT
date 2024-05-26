using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Data.Entities
{
    public class UserEntity : AuditEntity
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
