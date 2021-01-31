using EagleEye.DataAccess.Repositories;
using NUnit.Framework;
using System;

namespace EagleEye.DataAccess.IntegrationTests.RepositoryTests
{
    [TestFixture]
    public class MetadataRepositoryTests
    {
        private readonly IMetadataRepository _repository;

        public MetadataRepositoryTests()
        {
            _repository = new MetadataRepository();
        }

        [Test]
        public void GetAllMetadata()
        {
            var result = _repository.GetAllMetadata().Result;
            Assert.AreEqual(149, result.Length);
            foreach (var metadata in result)
            {
                Console.WriteLine(metadata.Title);
            }
        }

        [Test]
        public void GetMetaDataByMovieId()
        {
            // setup
            const int movieId = 7;
            // execute
            var result = _repository.GetMetadataByMovieId(movieId).Result;
            // assert
            Assert.AreEqual(50, result.Length);
            foreach (var metadata in result)
            {
                Assert.AreEqual(movieId, metadata.MovieId);
            }
        }

        [Test]
        public void AddMetadata()
        {
            _repository.AddMetadata(1, "", "", "", 1).Wait(0);
        }
    }
}
