namespace Domain.Services.ResultText
{
    using Entities.ResultText;
    using Entities.Speech;

    public interface IResultTextService : IDomainService
    {
        ResultText AddRecognitionResult(
            Speech speech,
            string text,
            RecognitionStatus status,
            decimal confidence);
    }
}