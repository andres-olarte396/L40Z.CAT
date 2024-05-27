namespace Core.Application.DTOs
{
    /// <summary>
    /// User Data Transfer Object
    /// </summary>
    public class UserDto
    {
        /// <summary>
        /// User Id
        /// </summary>
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
