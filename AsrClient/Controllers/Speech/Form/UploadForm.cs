namespace Web.Controllers.Speech.Form
{
    using Microsoft.AspNetCore.Http;

    public class UploadForm
    {
        public int Id { get; set; }

        public IFormFile File { get; set; }
    }
}