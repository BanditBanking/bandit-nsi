﻿using Bandit.NSI.Daemon.Models.DTOs;

namespace Bandit.NSI.Daemon.Exceptions
{
    [Serializable]
    public class InvalidCredentialsException : Exception, IExposedException
    {
        public InvalidCredentialsException() { }

        public InvalidCredentialsException(string message) : base(message) { }

        public ProblemDetailDTO Expose() => new()
        {
            HttpCode = StatusCodes.Status401Unauthorized,
            ErrorCode = "sparkle",
            Title = "Invalid credentials",
            Detail = $"The provided login/password combination doesn't match any account"
        };
    }
}
