using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers.Speech
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Domain.Entities.ResultText;
    using Domain.Entities.Speaker;
    using Domain.Entities.Speech;
    using Domain.Services.Recognizer;
    using Domain.Services.ResultText;
    using Domain.Services.Speaker;
    using Domain.Services.Speech;
    using Form;
    using Microsoft.AspNetCore.Http;

    public class SpeechController : Controller
    {
        private readonly ISpeechService _speechService;
        private readonly ISpeakerService _speakerService;
        private readonly IRecognizerService _recognizerService;
        private readonly IResultTextService _resultTextService;

        public SpeechController(
            ISpeechService speechService,
            ISpeakerService speakerService,
            IRecognizerService recognizerService,
            IResultTextService resultTextService)
        {
            _speechService = speechService;
            _speakerService = speakerService;
            _recognizerService = recognizerService;
            _resultTextService = resultTextService;
        }

        [HttpGet]
        public IActionResult List(int id)
        {
            IEnumerable<Speaker> speakers = _speakerService.GetAll();

            IEnumerable<Speech> speechItems = _speechService.GetAll();

            return View(speechItems.Where(x => x.AuthorId == id));
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            Speech speech = _speechService.GetById(id);

            return View(speech);
        }

        [HttpGet]
        public IActionResult Upload(int id)
        {
            return View(new UploadForm {Id = id});
        }

        [HttpPost]
        public IActionResult Upload(UploadForm form)
        {
            if (form.File is null) return RedirectToAction("Upload", new {id=form.Id});
;
            Speaker speaker = _speakerService.GetById(form.Id);

            byte[] audioFile = GetByteArray(form.File);

            _speechService.AddAudio(form.File.FileName, audioFile, speaker);

            return RedirectToAction("View", "Speaker", new {Id = form.Id});
        }

        private byte[] GetByteArray(IFormFile file)
        {
            if (file == null)
                throw new ArgumentNullException(nameof(file));
            if (file.Length < 1)
                throw new InvalidOperationException();

            byte[] audioData;

            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                audioData = memoryStream.ToArray();
            }

            return audioData;
        }

        [HttpGet]
        public IActionResult Recognize(int id)
        {
            Speech speech = _speechService.GetById(id);

            List<Speech> list = new List<Speech> {speech};

            IEnumerable<ResultText> texts = _recognizerService.Recognize(list);
            
            return RedirectToAction("List", "ResultText", new{id=id});
        }


        public IActionResult Remove(int id)
        {
            Speech speech = _speechService.GetById(id);

            int authorId = speech.AuthorId;

            _speechService.Remove(speech);

            return RedirectToAction("List", new {id = authorId});
        }
    }
}