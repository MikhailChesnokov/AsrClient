namespace Domain.Services.Speech.Implementations
{
    using System;
    using System.Collections.Generic;
    using Entities.Speaker;
    using Entities.Speech;
    using Repoository;

    public class SpeechService : ISpeechService
    {
        private readonly IRepository<Speech> _audioRepository;

        public SpeechService(IRepository<Speech> audioRepository)
        {
            _audioRepository = audioRepository;
        }

        public Speech AddAudio(string name, byte[] data, Speaker author)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            if (author == null)
                throw new ArgumentNullException(nameof(author));
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Speech speech = new Speech(name, data, author);

            author.AddAudio(speech);

            _audioRepository.Add(speech);

            return speech;
        }

        public Speech GetById(int id)
        {
            return _audioRepository.GetById(id);
        }

        public IEnumerable<Speech> GetAll()
        {
            return _audioRepository.GetAll();
        }

        public void Remove(Speech speech)
        {
            _audioRepository.Delete(speech);
        }
    }
}