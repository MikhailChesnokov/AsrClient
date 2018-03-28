namespace Domain.Services.Speaker
{
    using System.Collections.Generic;
    using Entities.Speaker;

    public interface ISpeakerService : IDomainService
    {
        Speaker AddSpeaker(
            string firstName,
            string surname,
            string patronymic,
            string university = null,
            string faculty = null,
            string group = null,
            int? course = null);

        void UpdateSpeaker(
            int id,
            string firstName,
            string surname,
            string patronymic,
            string university = null,
            string faculty = null,
            string group = null,
            int? course = null);

        Speaker GetById(int id);

        Speaker GetByName(string firstName, string surname, string patronymic);

        IEnumerable<Speaker> GetAll();

        void Remove(Speaker speaker);
    }
}