namespace Core.Exceptions
{
    /// <summary>
    /// Validation exception.
    /// </summary>
    public class ValidationException : Exception
    {
        /// <summary>
        /// Errors.
        /// </summary>
        public IDictionary<string, string[]> Errors { get; }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="errors">
        /// Errors.
        /// </param>
        public ValidationException(IDictionary<string, string[]> errors) : base("Validation exception")
        {
            Errors = errors;
        }
    }
}
