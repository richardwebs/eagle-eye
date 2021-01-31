using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using EagleEye.API.Services;
using EagleEye.DataAccess.Entities;
using EagleEye.DataAccess.Repositories;
using Moq;
using NUnit.Framework;

namespace EagleEye.API.UnitTests.ServiceTests
{
    [TestFixture]
    public class StatsServiceTests
    {
        private IStatsService _service;
        private Mock<IStatsRepository> _statsRepository;
        private Mock<IMetadataRepository> _metadataReppository;

        [SetUp]
        public void Setup()
        {
            _statsRepository = new Mock<IStatsRepository>();
            _metadataReppository = new Mock<IMetadataRepository>();
            _service = new StatsService(_statsRepository.Object, _metadataReppository.Object);
        }

        [Test]
        public void GetStatsSummary()
        {
            // setup
            var stats = new Stats[]
            {
                new Stats(1, 10000000),
                new Stats(1, 20000000),
                new Stats(2, 20000000),
                new Stats(2, 30000000),
                new Stats(2, 25000000),
                new Stats(3, 10000000) 
            };
            var metadata = new Metadata[]
            {
                new Metadata(1, 1, "title1", "EN", "something", 2000),
                new Metadata(2, 1, "title1", "EN", "something", 2000),
                new Metadata(3, 2, "title2", "EN", "something", 2000),
                new Metadata(4, 2, "title2", "AF", "something", 2000),
            };
            _statsRepository.Setup(x => x.GetAllStats()).ReturnsAsync(stats);
            _metadataReppository.Setup(x => x.GetAllMetadata()).ReturnsAsync(metadata);
            // execute
            var result = _service.GetStatsSummary().Result;
            // verify
            _statsRepository.Verify(x => x.GetAllStats(), Times.Once);
            _metadataReppository.Verify(x => x.GetAllMetadata(), Times.Once);
            // assert
            Assert.AreEqual(2, result.Length);
            
            Assert.AreEqual(2, result[0].MovieId);
            Assert.AreEqual(3, result[0].Watches);
            Assert.AreEqual(25000, result[0].AverageWatchDurationS);
            
            Assert.AreEqual(1, result[1].MovieId);
            Assert.AreEqual(2, result[1].Watches);
            Assert.AreEqual(15000, result[1].AverageWatchDurationS);
            foreach (var summary in result)
            {
                Console.WriteLine(JsonSerializer.Serialize(summary, new JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }
}
