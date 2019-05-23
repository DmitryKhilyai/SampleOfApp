using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Services;
using FluentAssertions.Common;
using Moq;
using NUnit.Framework.Internal;
using WebAPI;
using WebAPI.Controllers;
using WebAPI.Models;

namespace Tests.Controllers
{
    [TestFixture]
    public class CommentControllerTests
    {
        //[SetUp]
        //public void Setup()
        //{
        //}

        //[Test]
        //public void GetCommentAsync_Should_Return_Entity()
        //{
        //    //arrange
        //    var serviceMock = new Mock<ICommentService>();
        //    serviceMock.Setup(service=>service.GetCommentAsync())


        //    CommentController controller = new CommentController(_mapperMock.Object, );

        //    //act

        //    //assert

        //}

        [Test]
        public async Task GetCommentsAsync_Should_Return_Entities()//todo fix
        {
            //arrange
            var mockMapper = new Mock<IMapper>();
            //mockMapper.Setup(x=>x.Map<CommentDTO, CommentModel>()).Returns()


            var mockService = new Mock<ICommentService>();
            mockService.Setup(service => service.GetCommentsAsync()).ReturnsAsync(GetCommentDTOs());

            CommentController controller = new CommentController(mockMapper.Object, mockService.Object);

            //act
            var actualResult = await controller.GetCommentsAsync();

            //assert
            actualResult.Should().BeEquivalentTo(GetCommentModels());
        }

        private IEnumerable<CommentDTO> GetCommentDTOs()
        {
            return new List<CommentDTO>
            {
                new CommentDTO { Id=1, Text = "First"},
                new CommentDTO { Id=2, Text = "Second"},
                new CommentDTO { Id=3, Text = "Third"}
            };
        }

        private IEnumerable<CommentModel> GetCommentModels()
        {
            return new List<CommentModel>
            {
                new CommentModel { Id=1, Text = "First"},
                new CommentModel { Id=2, Text = "Second"},
                new CommentModel { Id=3, Text = "Third"}
            };
        }
    }
}
