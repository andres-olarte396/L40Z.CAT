namespace Core.Domain.Entities
{
    /// <summary>
    /// User entity
    /// </summary>
    public class User
    {
        /// <summary>
        /// User id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User name
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// User email
        /// </summary>
        public required string Email { get; set; }
    }
}
