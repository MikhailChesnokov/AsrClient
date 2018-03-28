namespace Domain.Services.Recognizer.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Net;
    using Dto;
    using Entities.ResultText;
    using Entities.Speech;
    using Newtonsoft.Json;
    using ResultText;

    public class MicrosoftSpeechApiRecognitionService : IRecognizerService
    {
        private const string RequestUri = "https://speech.platform.bing.com/speech/recognition" +
                                          "/dictation/cognitiveservices/v1?language=en-US&format=detailed";
        private const string CloudAccessToken = "6e97c54477334c388d8188a921bef5c2";
        private const int ChunkSize = 1024;

        private readonly IResultTextService _resultTextService;

        public MicrosoftSpeechApiRecognitionService(IResultTextService resultTextService)
        {
            _resultTextService = resultTextService;
        }

        public IEnumerable<ResultText> Recognize(IEnumerable<Speech> speechItems)
        {
            return speechItems.Aggregate(new List<ResultText>(), (list, speech) =>
            {
                CreateWebRequest(out HttpWebRequest request, speech.AudioFile);

                string responceJson = SendAndGetResponceJson(request);

                IEnumerable<ResultText> resultTexts = ToResultTextCollection(responceJson, speech);

                list.AddRange(resultTexts);

                return list;
            });
        }

        private void CreateWebRequest(out HttpWebRequest request ,byte[] audioFile)
        {
            request = (HttpWebRequest)WebRequest.Create(RequestUri);
            request.SendChunked = true;
            request.Accept = @"application/json;text/xml";
            request.Method = "POST";
            request.ProtocolVersion = HttpVersion.Version11;
            request.ContentType = @"audio/wav; codec=audio/pcm; samplerate=16000";
            request.Headers["Ocp-Apim-Subscription-Key"] = CloudAccessToken;

            using (Stream fileStream = new MemoryStream(audioFile))
            {
                using (Stream requestStream = request.GetRequestStream())
                {
                    byte[] buffer = new byte[(uint)Math.Min(ChunkSize, (int)fileStream.Length)];

                    int bytesRead;

                    while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        requestStream.Write(buffer, 0, bytesRead);
                    }

                    requestStream.Flush();
                }
            }
        }

        private string SendAndGetResponceJson(HttpWebRequest request)
        {
            string responceJson;

            using (WebResponse response = request.GetResponse())
            {
                using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                {
                    responceJson = streamReader.ReadToEnd();
                }
            }

            return responceJson;
        }

        private IEnumerable<ResultText> ToResultTextCollection(string responceJson, Speech speech)
        {
            MicrosoftSpeechApiResultDto result = JsonConvert.DeserializeObject<MicrosoftSpeechApiResultDto>(responceJson);

            if (result.RecognitionStatus != "Success") return null;

            List<ResultText> resultTexts = new List<ResultText>();

            foreach (MicrosoftSpeechApiHypothesisDto hypothesisDto in result.NBest)
            {
                ResultText resultText = _resultTextService.AddRecognitionResult(
                    speech,
                    hypothesisDto.Display,
                    RecognitionStatus.Success,
                    hypothesisDto.Confidence);

                resultTexts.Add(resultText);
            }

            return resultTexts;
        }
    }
}