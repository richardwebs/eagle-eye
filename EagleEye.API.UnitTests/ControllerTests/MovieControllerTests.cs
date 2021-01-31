using EagleEye.API.Controllers;
using EagleEye.API.Models;
using EagleEye.API.Services;
using EagleEye.DataAccess.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace EagleEye.API.UnitTests.ControllerTests
{
    [TestFixture]
    public class MovieControllerTests
    {
        private IMovieController _controller;
        private Mock<IMetadataService> _metadataService;
        private Mock<IStatsService> _statsService;

        [SetUp]
        public void Setup()
        {
            _metadataService = new Mock<IMetadataService>();
            _statsService = new Mock<IStatsService>();
            _controller = new MovieController(null, _metadataService.Object, _statsService.Object);
        }

        [Test]
        public void GetMetadataByMovieId_OKResult()
        {
            // setup
            const int moveId = 0;
            var output = new [] { new Metadata(1, 1, "title", "language", "duration", 1) };
            _metadataService.Setup(x => x.GetMetadataByMovieId(moveId)).ReturnsAsync(output);
            // execute
            var httpResponse = _controller.GetMetadataByMovieId(moveId).Result.Result;
            // verify
            _metadataService.Verify(x => x.GetMetadataByMovieId(moveId), Times.Once);
            // assert
            Assert.IsInstanceOf<OkObjectResult>(httpResponse);
            var actionResult = (OkObjectResult)httpResponse;
            Assert.IsInstanceOf<MetadataOutput[]>(actionResult.Value);
            var result = (MetadataOutput[]) actionResult.Value;
            Assert.AreEqual(output.Length, result.Length);
            Assert.AreEqual(result[0].MovieId, output[0].MovieId);
            Assert.AreEqual(result[0].Duration, output[0].Duration);
            Assert.AreEqual(result[0].Language, output[0].Language);
            Assert.AreEqual(result[0].ReleaseYear, output[0].ReleaseYear);
            Assert.AreEqual(result[0].Title, output[0].Title);
        }

        [Test]
        public void GetMetadataByMovieId_NotFound()
        {
            // setup
            const int moveId = 0;
            var output = new Metadata[] {};
            _metadataService.Setup(x => x.GetMetadataByMovieId(moveId)).ReturnsAsync(output);
            // execute
            var httpResponse = _controller.GetMetadataByMovieId(moveId).Result.Result;
            
            // verify
            _metadataService.Verify(x => x.GetMetadataByMovieId(moveId), Times.Once);
            // assert
            Assert.IsInstanceOf<NotFoundResult>(httpResponse);
        }

        [Test]
        public void GetStatsSummary_OkResult()
        {
            // setup
            var output = new[] {new StatsSummary(1, "title", 1, 1, 1)};
            _statsService.Setup(x => x.GetStatsSummary()).ReturnsAsync(output);
            // execute
            var httpResponse = _controller.GetStatsSummary().Result.Result;
            // verify
            _statsService.Verify(x => x.GetStatsSummary(), Times.Once);
            // assert
            Assert.IsInstanceOf<OkObjectResult>(httpResponse);
            var actionResult = (OkObjectResult)httpResponse;
            Assert.IsInstanceOf<StatsSummary[]>(actionResult.Value);
            var result = (StatsSummary[]) actionResult.Value;
            Assert.AreEqual(output.Length, result.Length);
            Assert.AreEqual(result[0].AverageWatchDurationS, output[0].AverageWatchDurationS);
            Assert.AreEqual(result[0].MovieId, output[0].MovieId);
            Assert.AreEqual(result[0].ReleaseYear, output[0].ReleaseYear);
            Assert.AreEqual(result[0].Title, output[0].Title);
        }

        [Test]
        public void AddMetadata_OK()
        {
            // setup
            var input = new MetadataInput();
            // execute
            var httpResponse = _controller.AddMetadata(input).Result;
            //verify
            _metadataService.Verify(x => x.AddMetadata(input), Times.Once);
            Assert.IsInstanceOf<OkResult>(httpResponse);
        }
    }
}
