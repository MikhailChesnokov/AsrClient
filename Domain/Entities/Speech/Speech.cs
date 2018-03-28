namespace Domain.Entities.Speech
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ResultText;
    using Speaker;

    public class Speech : IEntity
    {
        private const long MinDataSize = 1024;

        [Obsolete("Only for reflection", true)]
        public Speech() { }

        protected internal Speech(
            string name,
            byte[] data,
            Speaker author)
        {
            SetName(name);
            SetData(data);
            SetAuthor(author);
        }

        public IList<ResultText> RecognitionResults { get; } = new List<ResultText>();

        public string Name { get; protected set; }

        public byte[] AudioFile { get; protected set; }

        public int AudioFormat { get; protected set; }

        public int? Duration { get; protected set; }

        public int? BitRate { get; protected set; }

        public int? TotalChannels { get; protected set; }

        public decimal? Size { get; protected set; }

        public int? SamplingRate { get; protected set; }

        public int? BitDepth { get; protected set; }

        public DateTime? RecordingDate { get; protected set; }

        public Speaker Author { get; protected set; }

        public int AuthorId { get; protected set; }

        public int Id { get; set; }


        protected internal void SetName(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        protected internal void SetData(byte[] data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            if (data.Length < MinDataSize)
                throw new InvalidOperationException("Too small data.");

            AudioFile = data;
        }

        protected internal void SetAuthor(Speaker author)
        {
            Author = author ?? throw new ArgumentNullException(nameof(author));
        }


        protected internal void AddRecognitionResult(ResultText resultText)
        {
            if (resultText == null)
                throw new ArgumentNullException(nameof(resultText));

            if (RecognitionResults.Any(x => x.Id == resultText.Id))
                RecognitionResults.Remove(RecognitionResults.SingleOrDefault(x => x.Id == resultText.Id));

            RecognitionResults.Add(resultText);
        }
    }
}