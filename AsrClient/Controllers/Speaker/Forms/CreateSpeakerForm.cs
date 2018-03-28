namespace Web.Controllers.Speaker.Forms
{
    using System.ComponentModel.DataAnnotations;

    public class CreateSpeakerForm
    {
        [Required]
        [MaxLength(64)]
        [RegularExpression("^[А-Яа-яёЁ\\-]+$")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(64)]
        [RegularExpression("^[А-Яа-яёЁ\\-]+$")]
        public string Surname { get; set; }

        [Required]
        [MaxLength(64)]
        [RegularExpression("^[А-Яа-яёЁ\\-]+$")]
        public string Patronymic { get; set; }

        [MaxLength(128)]
        [RegularExpression("^[А-Яа-яёЁ.\\- ]+$")]
        public string University { get; set; }

        [MaxLength(128)]
        [RegularExpression("^[А-Яа-яёЁ\\- ]+$")]
        public string Faculty { get; set; }

        [MaxLength(16)]
        [RegularExpression("^[А-Яа-яёЁ\\d.\\- ]+$")]
        public string Group { get; set; }

        [Range(1,6)]
        public int? Course { get; set; }
    }
}