namespace DomainTest.Entities.Speaker
{
    using System;
    using System.Linq;
    using Domain.Entities.Speaker;
    using Domain.Repoository;
    using Domain.Services.Speaker;
    using Domain.Services.Speaker.Exceptions;
    using Domain.Services.Speaker.Implementations;
    using Repository.ListRepository;
    using Xunit;

    public class SpeakerTest
    {
        public SpeakerTest()
        {
            _speakerRepository = new ListRepository<Speaker>();
            _speakerService = new SpeakerService(_speakerRepository);
        }

        private readonly ISpeakerService _speakerService;
        private readonly IRepository<Speaker> _speakerRepository;

        [Theory]
        [InlineData("1")]
        [InlineData("Иван1")]
        [InlineData("Ivan1")]
        [InlineData("Ivan!")]
        [InlineData("Ivan ")]
        [InlineData("IvanИван")]
        public void AddSpeaker_InvalidName(string firstName)
        {
            Assert.Throws<InvalidOperationException>(() =>
            {
                _speakerService
                    .AddSpeaker(
                        firstName,
                        "Иванов",
                        "Иванович");
            });
        }

        [Theory]
        [InlineData("Пётр")]
        [InlineData("Иван")]
        [InlineData("Petr")]
        [InlineData("O'Neil")]
        public void AddSpeaker_ValidName(string firstName)
        {
            _speakerService
                .AddSpeaker(
                    firstName,
                    "Иванов",
                    "Иванович");

            Assert.True(true);
        }

        [Fact]
        public void AddSpeaker_FullDefinition()
        {
            Speaker speaker =
                _speakerService
                    .AddSpeaker(
                        "Иван",
                        "Иванов",
                        "Иванович",
                        "ПГУ",
                        "МехМат",
                        "121",
                        4);

            Speaker speakerCopy = _speakerRepository.GetAll().First();

            Assert.Equal(speaker, speakerCopy);
        }

        [Fact]
        public void AddSpeaker_ShortDefinition()
        {
            Speaker speaker =
                _speakerService
                    .AddSpeaker(
                        "Иван",
                        "Иванов",
                        "Иванович");

            Speaker speakerCopy = _speakerRepository.GetAll().First();

            Assert.Equal(speaker, speakerCopy);
        }

        [Fact]
        public void AddSpeaker_WithTheSameName()
        {
            _speakerService
                .AddSpeaker(
                    "Иван",
                    "Иванов",
                    "Иванович");

            Assert.Throws<SpeakerAlreadyExistsException>(() =>
            {
                _speakerService
                    .AddSpeaker(
                        "Иван",
                        "Иванов",
                        "Иванович");
            });
        }
    }
}