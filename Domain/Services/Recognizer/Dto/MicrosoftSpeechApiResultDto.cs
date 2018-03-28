namespace Domain.Services.Recognizer.Dto
{
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;

    public class MicrosoftSpeechApiResultDto
    {
        public string RecognitionStatus { get; set; }

        public int Offset { get; set; }

        public int Duration { get; set; }

        public List<MicrosoftSpeechApiHypothesisDto> NBest { get; set; }
    }

    [SuppressMessage("ReSharper", "InconsistentNaming")]
    public class MicrosoftSpeechApiHypothesisDto
    {
        public decimal Confidence { get; set; }

        public string Lexical { get; set; }

        public string ITN { get; set; }

        public string MaskedITN { get; set; }

        public string Display { get; set; }
    }
}