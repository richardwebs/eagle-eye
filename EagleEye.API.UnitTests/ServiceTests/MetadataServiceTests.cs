using EagleEye.API.Services;
using EagleEye.DataAccess.Entities;
using EagleEye.DataAccess.Repositories;
using Moq;
using NUnit.Framework;
using System;
using System.Text.Json;

namespace EagleEye.API.UnitTests.ServiceTests
{
    [TestFixture]
    public class MetadataServiceTests
    {
        private  IMetadataService _service;
        private Mock<IMetadataRepository> _repository;

        [SetUp]
        public void Setup()
        {
            _repository = new Mock<IMetadataRepository>();
            _service = new MetadataService(_repository.Object);
        }

        [Test]
        public void GetMetadataByMovieId()
        {
            // setup
            const int movieId = 1;
            var output = new[] { 
                new Metadata(1, movieId, "something", "EN", "something", 2000),
                new Metadata(2, movieId, "something", "EN", "something", 2000),
                new Metadata(3, movieId, "something", "EN", "something", 2000),
                new Metadata(4, movieId, "something else", "AF", "something", 2001),
                new Metadata(5, movieId, "something else", "AF", "something", 2001)
            };
            _repository.Setup(x => x.GetMetadataByMovieId(movieId)).ReturnsAsync(output);
            // execute
            var result = _service.GetMetadataByMovieId(movieId).Result;
            // verify
            _repository.Verify(x => x.GetMetadataByMovieId(movieId), Times.Once);
            // assert
            Assert.AreEqual(2, result.Length);
            Assert.AreEqual("AF", result[0].Language);
            Assert.AreEqual(5, result[0].MetadataId);
            Assert.AreEqual("EN", result[1].Language);
            Assert.AreEqual(3, result[1].MetadataId);
            foreach (var metadata in result)
            {
                Console.WriteLine(JsonSerializer.Serialize(metadata, new JsonSerializerOptions { WriteIndented = true }));
            }
        }
    }
}
