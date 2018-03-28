namespace Domain.Services.ResultText.Implementations
{
    using System;
    using Entities.ResultText;
    using Entities.Speech;
    using Repoository;

    public class ResultTextTextService : IResultTextService
    {
        private readonly IRepository<ResultText> _recognitionResultsRepository;

        public ResultTextTextService(IRepository<ResultText> recognitionResultsRepository)
        {
            _recognitionResultsRepository = recognitionResultsRepository;
        }

        public ResultText AddRecognitionResult(
            Speech speech,
            string text,
            RecognitionStatus status,
            decimal confidence)
        {
            if (speech == null)
                throw new ArgumentNullException(nameof(speech));
            if (string.IsNullOrWhiteSpace(text))
                throw new ArgumentNullException(nameof(text));

            ResultText resultText = new ResultText(speech, text, status, confidence);

            speech.AddRecognitionResult(resultText);

            _recognitionResultsRepository.Add(resultText);

            return resultText;
        }
    }
}