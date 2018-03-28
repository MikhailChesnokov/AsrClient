namespace Domain.Entities.Speaker
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text.RegularExpressions;
    using Speech;

    public class Speaker : IEntity
    {
        [Obsolete("Only for reflection", true)]
        public Speaker() { }

        protected internal Speaker(
            string firstName,
            string surname,
            string patronymic,
            string university,
            string faculty,
            string group,
            int? course)
        {
            SetFirstName(firstName);
            SetSurname(surname);
            SetPatronymic(patronymic);

            SetUniversity(university);
            SetFaculty(faculty);
            SetGroup(group);
            SetCourse(course);
        }

        public IList<Speech> Audios { get; } = new List<Speech>();

        public string FirstName { get; protected set; }

        public string Surname { get; protected set; }

        public string Patronymic { get; protected set; }

        public string University { get; protected set; }

        public string Faculty { get; protected set; }

        public string Group { get; protected set; }

        public int? Course { get; protected set; }

        public int Id { get; set; }


        protected internal void SetFirstName(string firstName)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));

            if (!(Regex.IsMatch(firstName, "^[А-Яа-яёЁ\\-]+$") ||
                  Regex.IsMatch(firstName, "^[A-Za-z.'\\-]+$")))
                throw new InvalidOperationException("Wrong name argument value.");

            FirstName = firstName;
        }

        protected internal void SetSurname(string surname)
        {
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentNullException(nameof(surname));

            if (!(Regex.IsMatch(surname, "^[А-Яа-яёЁ\\-]+$") ||
                  Regex.IsMatch(surname, "^[A-Za-z.'\\-]+$")))
                throw new InvalidOperationException("Wrong surname argument value.");

            Surname = surname;
        }

        protected internal void SetPatronymic(string patronymic)
        {
            if (string.IsNullOrWhiteSpace(patronymic))
                throw new ArgumentNullException(nameof(patronymic));

            if (!(Regex.IsMatch(patronymic, "^[А-Яа-яёЁ\\-]+$") ||
                  Regex.IsMatch(patronymic, "^[A-Za-z.'\\-]+$")))
                throw new InvalidOperationException("Wrong patronymic argument value.");

            Patronymic = patronymic;
        }

        protected internal void SetUniversity(string university)
        {
            if (university is null) return;

            if (!(Regex.IsMatch(university, "^[А-Яа-яёЁ.\\- ]+$") ||
                  Regex.IsMatch(university, "^[A-Za-z.'\\- ]+$")))
                throw new InvalidOperationException("Wrong university argument value.");

            University = university;
        }

        protected internal void SetFaculty(string faculty)
        {
            if (faculty is null) return;

            if (!(Regex.IsMatch(faculty, "^[А-Яа-яёЁ\\- ]+$") ||
                  Regex.IsMatch(faculty, "^[A-Za-z.'\\- ]+$")))
                throw new InvalidOperationException("Wrong faculty argument value.");

            Faculty = faculty;
        }

        protected internal void SetGroup(string group)
        {
            if (group is null) return;

            if (!(Regex.IsMatch(group, "^[А-Яа-яёЁ\\d.\\- ]+$") ||
                  Regex.IsMatch(group, "^[A-Za-z\\d.\\- ]+$")))
                throw new InvalidOperationException("Wrong group argument value.");

            Group = group;
        }

        protected internal void SetCourse(int? course)
        {
            if (course is null) return;

            if (course < 1 || course > 6)
                throw new InvalidOperationException("Wrong course argument value.");

            Course = course;
        }


        protected internal void AddAudio(Speech speech)
        {
            if (speech == null)
                throw new ArgumentNullException(nameof(speech));

            if (Audios.Any(x => x.Id == speech.Id))
                Audios.Remove(Audios.SingleOrDefault(x => x.Id == speech.Id));

            Audios.Add(speech);
        }
    }
}