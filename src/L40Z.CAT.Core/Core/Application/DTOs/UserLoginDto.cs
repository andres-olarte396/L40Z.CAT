namespace Core.Application.DTOs
{
    /// <summary>
    /// Data transfer object for user login
    /// </summary>
    public class UserLoginDto
    {
        /// <summary>
        /// Username of the user
        /// </summary>
        public required string Username { get; set; }

        /// <summary>
        /// Password of the user
        /// </summary>
        public required string Password { get; set; }
    }
}
