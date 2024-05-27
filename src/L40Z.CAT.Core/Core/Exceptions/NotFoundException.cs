namespace Core.Exceptions
{
    /// <summary>
    /// Not found exception.
    /// </summary>
    public class NotFoundException : Exception
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="message">
        /// Exception message.
        /// </param>
        public NotFoundException(string message) : base(message)
        {
            this.HResult = 404;
            this.Source = "Core";
        }
    }
}

