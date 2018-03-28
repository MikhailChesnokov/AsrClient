namespace Domain.Entities.ResultText
{
    using System;
    using Speech;

    public class ResultText : IEntity
    {
        [Obsolete("Only for reflection", true)]
        public ResultText() { }

        public ResultText(
            Speech speech,
            string text,
            RecognitionStatus status,
            decimal confidence)
        {
            SetAudio(speech);
            SetText(text);
            SetDate(default(DateTime));
            SetConfidence(confidence);
            RecognitionStatus = status;
        }

        public string Text { get; protected set; }

        public decimal Confidence { get; protected set; }

        public DateTime RecognitionDate { get; protected set; }

        public RecognitionStatus RecognitionStatus { get; protected set; }

        public Speech Speech { get; protected set; }

        public int AudioId { get; protected set; }

        public int Id { get; set; }


        protected internal void SetAudio(Speech speech)
        {
            //Speech = speech ?? throw new NullReferenceException(nameof(speech));

            Speech = speech;
        }

        protected internal void SetText(string text)
        {
            Text = text ?? throw new NullReferenceException(nameof(text));
        }

        protected internal void SetDate(DateTime dateTime)
        {
            RecognitionDate = dateTime == default(DateTime) ? DateTime.Now : dateTime;
        }

        protected internal void SetConfidence(decimal confidence)
        {
            if (confidence < 0 ||
                confidence > 100)
                throw new InvalidOperationException("Confidence is out of range.");

            Confidence = confidence;
        }
    }
}