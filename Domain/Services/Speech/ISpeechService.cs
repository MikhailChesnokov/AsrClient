namespace Domain.Services.Speech
{
    using System.Collections.Generic;
    using Entities.Speaker;
    using Entities.Speech;

    public interface ISpeechService : IDomainService
    {
        Speech AddAudio(string name, byte[] data, Speaker author);

        Speech GetById(int id);

        IEnumerable<Speech> GetAll();

        void Remove(Speech speech);
    }
}