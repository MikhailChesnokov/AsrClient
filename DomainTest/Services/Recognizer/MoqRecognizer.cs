namespace DomainTest.Services.Recognizer
{
    using System.Collections.Generic;
    using Domain.Entities.ResultText;
    using Domain.Entities.Speech;
    using Domain.Services.Recognizer;
    using Domain.Services.Recognizer.Dto;
    using Domain.Services.ResultText;
    using Newtonsoft.Json;

    public class MoqRecognizer : IRecognizerService
    {
        private readonly IResultTextService _resultTextService;
        public MoqRecognizer(IResultTextService resultTextService)
        {
            _resultTextService = resultTextService;
        }

        public IEnumerable<ResultText> Recognize(IEnumerable<Speech> speechItems)
        {
            return ToResultTextCollection(GetResponceExample());
        }

        private string GetResponceExample()
        {
            return
                @"{
  ""RecognitionStatus"": ""Success"",
  ""Offset"": 300000,
  ""Duration"": 108900000,
  ""NBest"": [
    {
      ""Confidence"": 0.8756915,
      ""Lexical"": ""how fast part of the interview i'm going to ask you some questions about yourself so let's talk about what you do you look study"",
      ""ITN"": ""how fast part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you look study"",
      ""MaskedITN"": ""how fast part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you look study"",
      ""Display"": ""How fast part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you look study?""
    },
    {
      ""Confidence"": 0.8609026,
      ""Lexical"": ""how fast part of the interview i'm going to ask you some questions about yourself so let's talk about what you do you work study"",
      ""ITN"": ""how fast part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you work study"",
      ""MaskedITN"": ""how fast part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you work study"",
      ""Display"": ""How fast part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you work study?""
    },
    {
      ""Confidence"": 0.8609026,
      ""Lexical"": ""the first part of the interview i'm going to ask you some questions about yourself so let's talk about what you do you look study"",
      ""ITN"": ""the first part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you look study"",
      ""MaskedITN"": ""the first part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you look study"",
      ""Display"": ""The first part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you look study.""
    },
    {
      ""Confidence"": 0.8514374,
      ""Lexical"": ""the first part of the interview i'm going to ask you some questions about yourself so let's talk about what you do you work study"",
      ""ITN"": ""the first part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you work study"",
      ""MaskedITN"": ""the first part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you work study"",
      ""Display"": ""The first part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you work study.""
    },
    {
      ""Confidence"": 0.8514374,
      ""Lexical"": ""the fast part of the interview i'm going to ask you some questions about yourself so let's talk about what you do you look study"",
      ""ITN"": ""the fast part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you look study"",
      ""MaskedITN"": ""the fast part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you look study"",
      ""Display"": ""The fast part of the interview I'm going to ask you some questions about yourself so let's talk about what you do you look study.""
    }
  ]
}";
        }

        private IEnumerable<ResultText> ToResultTextCollection(string responceJson)
        {
            MicrosoftSpeechApiResultDto result = JsonConvert.DeserializeObject<MicrosoftSpeechApiResultDto>(responceJson);

            if (result.RecognitionStatus != "Success") return null;

            List<ResultText> resultTexts = new List<ResultText>();

            foreach (MicrosoftSpeechApiHypothesisDto hypothesisDto in result.NBest)
            {
                ResultText resultText = new ResultText(
                    null,
                    hypothesisDto.Display,
                    RecognitionStatus.Success,
                    hypothesisDto.Confidence);

                _resultTextService.AddRecognitionResult(null, hypothesisDto.Display, RecognitionStatus.Success, hypothesisDto.Confidence);

                resultTexts.Add(resultText);
            }

            return resultTexts;
        }
    }
}