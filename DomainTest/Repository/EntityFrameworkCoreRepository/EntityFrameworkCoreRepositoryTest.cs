namespace DomainTest.Repository.EntityFrameworkCoreRepository
{
    using Domain.Entities.Speaker;
    using Domain.Repoository;
    using Domain.Services.Speaker;
    using Domain.Services.Speaker.Implementations;
    using Infrastructure.Repository;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class EntityFrameworkCoreRepositoryTest
    {
        private readonly ISpeakerService _speakerService;
        private readonly IRepository<Speaker> _speakerRepository;

        public EntityFrameworkCoreRepositoryTest()
        {
            DbContext context = new ApplicationContext();

            _speakerRepository = new EntityFrameworkCoreRepository<Speaker>(context);

            _speakerService = new SpeakerService(_speakerRepository);
        }

        [Fact]
        public void ItWorks()
        {
            Speaker speaker = _speakerService.AddSpeaker("Иван", "Иванов", "Иванович");

            _speakerRepository.Delete(speaker);

            Assert.True(true);
        }
    }
}