using FluentAssertions;
using NUnit.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services;
using Moq;
using WebAPI.Controllers;

namespace Tests.Controllers
{
    [TestFixture]
    public class CommentsControllerTests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        [Test]
        public async Task GetCommentsAsync_Should_Return_Entities()
        {
            //arrange
            var mockService = new Mock<ICommentService>();
            mockService.Setup(service => service.GetCommentsAsync()).ReturnsAsync(GetCommentDTOs());

            CommentsController controller = new CommentsController(mockService.Object);

            //act
            var actualResult = await controller.GetCommentsAsync();

            //assert
            actualResult.Should().BeEquivalentTo(GetCommentModels());
        }

        private IEnumerable<CommentDTO> GetCommentDTOs()
        {
            return new List<CommentDTO>
            {
                new CommentDTO {Id = 1, Text = "First"},
                new CommentDTO {Id = 2, Text = "Second"},
                new CommentDTO {Id = 3, Text = "Third"}
            };
        }

        private IEnumerable<CommentDTO> GetCommentModels()
        {
            return new List<CommentDTO>
            {
                new CommentDTO {Id = 1, Text = "First"},
                new CommentDTO {Id = 2, Text = "Second"},
                new CommentDTO {Id = 3, Text = "Third"}
            };
        }
    }
}
