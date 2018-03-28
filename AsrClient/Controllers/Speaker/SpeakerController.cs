namespace Web.Controllers.Speaker
{
    using System.Collections.Generic;
    using Domain.Entities.Speaker;
    using Domain.Services.Speaker;
    using Forms;
    using Microsoft.AspNetCore.Mvc;

    public class SpeakerController : Controller
    {
        private readonly ISpeakerService _speakerService;

        public SpeakerController(ISpeakerService speakerService)
        {
            _speakerService = speakerService;
        }

        public IActionResult List()
        {
            IEnumerable<Speaker> speakers = _speakerService.GetAll();

            return View(speakers);
        }

        public IActionResult View(int id)
        {
            Speaker speaker = _speakerService.GetById(id);

            return View(speaker);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateSpeakerForm());
        }

        [HttpPost]
        public IActionResult Create(CreateSpeakerForm form)
        {
            if (!ModelState.IsValid) return View(form);

            _speakerService.AddSpeaker(
                form.FirstName,
                form.Surname,
                form.Patronymic,
                form.University,
                form.Faculty,
                form.Group,
                form.Course);

            Speaker speaker = _speakerService.GetByName(
                form.FirstName,
                form.Surname,
                form.Patronymic);

            return RedirectToAction("View", new {id = speaker.Id});
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            Speaker speaker = _speakerService.GetById(id);

            EditSpeakerForm form = new EditSpeakerForm
            {
                Id = speaker.Id,
                FirstName = speaker.FirstName,
                Surname = speaker.Surname,
                Patronymic = speaker.Patronymic,
                University = speaker.University,
                Faculty = speaker.Faculty,
                Group = speaker.Group,
                Course = speaker.Course
            };

            return View(form);
        }

        [HttpPost]
        public IActionResult Edit(EditSpeakerForm form)
        {
            if (!ModelState.IsValid) return View(form);

            _speakerService.UpdateSpeaker(
                form.Id,
                form.FirstName,
                form.Surname,
                form.Patronymic,
                form.University,
                form.Faculty,
                form.Group,
                form.Course);
            
            return RedirectToAction("View", new { id = form.Id });
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            Speaker speaker = _speakerService.GetById(id);

            _speakerService.Remove(speaker);

            return RedirectToAction("List");
        }
    }
}