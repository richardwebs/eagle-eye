using EagleEye.DataAccess.Repositories;
using NUnit.Framework;


namespace EagleEye.DataAccess.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class StatsRepositoryTests
    {
        private readonly IStatsRepository _repository;

        public StatsRepositoryTests()
        {
            _repository = new StatsRepository();
        }

        [Test]
        public void GetAllStats()
        {
            // setup
            var result = _repository.GetAllStats().Result;
            // assert
            Assert.AreEqual(48084, result.Length);
        }
    }
}
