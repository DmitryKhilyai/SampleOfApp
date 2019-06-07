using System;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using NUnit.Framework.Internal;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests.Repositories
{
    [TestFixture]
    public class CommentRepositoryTests
    {
        [Test]
        public async Task CreateAsync_ShouldCreateCommentSuccessfully()
        {
            //arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "Add_items_to_database")
                .Options;

            var item = new Comment {Text = "First"};

            //act
            using (var context = new ApplicationContext(options))
            {
                var repository = new Repository<Comment>(context);
                repository.Create(item);
                await repository.SaveAsync();
            }

            //assert
            using (var context = new ApplicationContext(options))
            {
                context.Comments.Count().Should().Be(1);
                context.Comments.Single().Text.Should().Be("First");
            }
        }

        [Test]
        public void CreateAsync_ShouldThrowException_WhenTriedToSetDuplicate()
        {
            //arrange
            var options = new DbContextOptionsBuilder<ApplicationContext>()
                .UseInMemoryDatabase(databaseName: "Add_duplicate_items_to_database")
                .Options;

            var id = 1;
            var item = new Comment {Id = id, Text = "First"};

            using (var context = new ApplicationContext(options))
            {
                context.Comments.Add(item);
                context.SaveChanges();

                var repository = new Repository<Comment>(context);

                //act
                repository.Create(item);
                Func<Task> call = async () => await repository.SaveAsync();

                //assert
                call.Should().Throw<ArgumentException>("An item with the same key has already been added.");
            }
        }

        [Test]
        public async Task GetEntitiesAsync_ShouldReturnComments()
        {
            //arrange
            var data = new List<Comment>
            {
                new Comment {Id = 1, Text = "First"},
                new Comment {Id = 2, Text = "Second"},
                new Comment {Id = 3, Text = "Third"}
            }.AsQueryable();

            var mockSet = new Mock<DbSet<Comment>>();
            mockSet.As<IAsyncEnumerable<Comment>>()
                .Setup(m => m.GetEnumerator())
                .Returns(new AsyncEnumerator<Comment>(data.GetEnumerator()));

            mockSet.As<IQueryable<Comment>>().Setup(m => m.Provider).Returns(data.Provider);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.Expression).Returns(data.Expression);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.ElementType).Returns(data.ElementType);
            mockSet.As<IQueryable<Comment>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());

            var mockContext = new Mock<ApplicationContext>();
            mockContext.SetupGet(c => c.Comments).Returns(mockSet.Object);

            var repository = new Repository<Comment>(mockContext.Object);

            //act
            var actualResult = await repository.GetEntitiesAsync();

            //assert
            actualResult.Should().BeEquivalentTo(new List<Comment>
            {
                new Comment {Id = 1, Text = "First"},
                new Comment {Id = 2, Text = "Second"},
                new Comment {Id = 3, Text = "Third"}
            });
        }

        private IEnumerable<Comment> GetComments()
        {
            return new List<Comment>
            {
                new Comment {Id = 1, Text = "First"},
                new Comment {Id = 2, Text = "Second"},
                new Comment {Id = 3, Text = "Third"}
            };
        }
    }
}
