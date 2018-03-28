namespace Domain.Services.Speaker.Implementations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Entities.Speaker;
    using Exceptions;
    using Repoository;

    public class SpeakerService : ISpeakerService
    {
        private readonly IRepository<Speaker> _speakersRepository;

        public SpeakerService(IRepository<Speaker> speakersRepository)
        {
            _speakersRepository = speakersRepository;
        }

        public Speaker AddSpeaker(
            string firstName,
            string surname,
            string patronymic,
            string university = null,
            string faculty = null,
            string group = null,
            int? course = null)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentNullException(nameof(surname));
            if (string.IsNullOrWhiteSpace(patronymic))
                throw new ArgumentNullException(nameof(patronymic));

            if (_speakersRepository
                .GetAll()
                .Any(x =>
                         x.FirstName == firstName &&
                         x.Surname == surname &&
                         x.Patronymic == patronymic))
                throw new SpeakerAlreadyExistsException("The speaker with the same name already exists.");

            Speaker speaker = new Speaker(
                firstName,
                surname,
                patronymic,
                university,
                faculty,
                group,
                course);

            _speakersRepository.Add(speaker);

            return speaker;
        }

        public void UpdateSpeaker(
            int id,
            string firstName,
            string surname,
            string patronymic,
            string university = null,
            string faculty = null,
            string group = null,
            int? course = null)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                throw new ArgumentNullException(nameof(firstName));
            if (string.IsNullOrWhiteSpace(surname))
                throw new ArgumentNullException(nameof(surname));
            if (string.IsNullOrWhiteSpace(patronymic))
                throw new ArgumentNullException(nameof(patronymic));

            Speaker speaker = _speakersRepository.GetById(id);

            speaker.SetFirstName(firstName);
            speaker.SetSurname(surname);
            speaker.SetPatronymic(patronymic);

            speaker.SetUniversity(university);
            speaker.SetFaculty(faculty);
            speaker.SetGroup(group);
            speaker.SetCourse(course);
        }

        public Speaker GetById(int id)
        {
            return _speakersRepository.GetById(id);
        }

        public Speaker GetByName(string firstName, string surname, string patronymic)
        {
            return _speakersRepository
                   .GetAll()
                   .SingleOrDefault(
                       x => x.FirstName == firstName &&
                            x.Surname == surname &&
                            x.Patronymic == patronymic);
        }

        public IEnumerable<Speaker> GetAll()
        {
            return _speakersRepository.GetAll();
        }

        public void Remove(Speaker speaker)
        {
            _speakersRepository.Delete(speaker);
        }
    }
}