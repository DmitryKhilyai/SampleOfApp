using FluentAssertions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebAPI.Models;

namespace Tests.Controllers
{
    [TestFixture]
    public class PostModelTests
    {
        [Test]
        public void ShouldValidatePostModelSuccessfully()
        {
            //arrange
            var postModel = new PostModel
            {
                Title = "Title",
                Content = "Content",
                Date = DateTime.UtcNow.AddHours(-1)
            };

            //act
            var validationResults = new List<ValidationResult>();
            var actualResult = Validator.TryValidateObject(postModel, new ValidationContext(postModel), validationResults, true);

            //assert
            actualResult.Should().BeTrue("Expected validation to succeed.");
            validationResults.Count.Should().Be(0);
        }

        [TestCase("Title", null, Constants.ContentRequired)]
        [TestCase(null, "Content", Constants.TitleRequired)]
        public void ShouldValidatePostModelNotSuccessfully_WhenAnyOneRequiredFieldIsNull(string title, string content, string expectedResult)
        {
            //arrange
            var postModel = new PostModel
            {
                Title = title,
                Content = content,
                Date = DateTime.UtcNow
            };

            //act
            var validationResults = new List<ValidationResult>();
            var actualResult = Validator.TryValidateObject(postModel, new ValidationContext(postModel), validationResults, true);

            //assert
            actualResult.Should().BeFalse("Expected validation to fail.");
            validationResults.Count.Should().Be(1);
            var msg = validationResults[0];
            msg.ErrorMessage.Should().BeEquivalentTo(expectedResult);
            msg.MemberNames.Count().Should().Be(1);
        }

        [Test]
        public void ShouldValidatePostModelNotSuccessfully_WhenDateAreInTheFuture()
        {
            //arrange
            var postModel = new PostModel
            {
                Title = "Title",
                Content = "Content",
                Date = DateTime.UtcNow.AddHours(1)
            };

            //act
            var validationResults = new List<ValidationResult>();
            var actualResult = Validator.TryValidateObject(postModel, new ValidationContext(postModel), validationResults, true);

            //assert
            actualResult.Should().BeFalse("Expected validation to fail.");
            validationResults.Count.Should().Be(1);
            var msg = validationResults[0];
            msg.ErrorMessage.Should().BeEquivalentTo(Constants.DateInvalid);
            msg.MemberNames.Count().Should().Be(1);
        }
    }
}
