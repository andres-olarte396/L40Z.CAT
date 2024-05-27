namespace Infrastructure.Data.Entities
{
    /// <summary>
    /// Base class for entities that need to be audited.
    /// </summary>
    public abstract class AuditEntity
    {
        /// <summary>
        /// The date and time the entity was created.
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// The user who created the entity.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// The date and time the entity was last modified.
        /// </summary>
        public DateTime? ModifiedAt { get; set; }

        /// <summary>
        /// The user who last modified the entity.
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
