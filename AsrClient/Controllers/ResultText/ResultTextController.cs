namespace Web.Controllers.ResultText
{
    using System.Collections.Generic;
    using System.Linq;
    using Domain.Entities.ResultText;
    using Domain.Repoository;
    using Microsoft.AspNetCore.Mvc;

    public class ResultTextController : Controller
    {
        private readonly IRepository<ResultText> _resultTextRepository;
        public ResultTextController(IRepository<ResultText> resultTextRepository)
        {
            _resultTextRepository = resultTextRepository;
        }

        [HttpGet]
        public IActionResult List(int id)
        {
            IEnumerable<ResultText> resultTexts = _resultTextRepository.GetAll().Where(x => x.AudioId == id).ToList();

            return View(resultTexts);
        }

        [HttpGet]
        public IActionResult View(int id)
        {
            ResultText resultText = _resultTextRepository.GetById(id);

            return View(resultText);
        }

        [HttpGet]
        public IActionResult Remove(int id)
        {
            ResultText resultText = _resultTextRepository.GetById(id);

            int audioId = resultText.AudioId;

            _resultTextRepository.Delete(resultText);

            return RedirectToAction("List", new {id = audioId});
        }
    }
}