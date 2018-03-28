namespace Domain.Services.Recognizer
{
    using System.Collections.Generic;
    using Entities.ResultText;
    using Entities.Speech;

    public interface IRecognizerService : IDomainService
    {
        IEnumerable<ResultText> Recognize(IEnumerable<Speech> speechItems);
    }
}