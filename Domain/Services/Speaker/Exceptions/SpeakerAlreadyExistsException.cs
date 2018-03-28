namespace Domain.Services.Speaker.Exceptions
{
    using System;

    public class SpeakerAlreadyExistsException : Exception
    {
        public SpeakerAlreadyExistsException(string message) : base(message) { }
    }
}