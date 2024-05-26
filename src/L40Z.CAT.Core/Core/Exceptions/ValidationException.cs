﻿namespace Core.Exceptions
{
    public class ValidationException : Exception
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException(IDictionary<string, string[]> errors) : base("Validation exception")
        {
            Errors = errors;
        }
    }
}
