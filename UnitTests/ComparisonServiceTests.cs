using AutoMapper;
using Core.Persistance;
using FakeItEasy;
using Microsoft.EntityFrameworkCore;
using Services;
using System;
using System.Text.Json;
using Xunit;

namespace UnitTests
{
    //Improvements
    //There must be several examples of test cases and they should not be constants.
    //Implementing logic here seperately would be nice, instead of comparing service results to constants
    public class ComparisonServiceTests
    {
        [Fact]
        public void Comparison_Equal()
        {
            var right = "Hello";
            var left = "Hello";

            var fakeMapper = A.Fake<IMapper>();

            //fake db context since it's not needed
            var options = new DbContextOptionsBuilder<DefaultDbContext>()
           .Options;

            var comparisonService = new ComparisonService(new DefaultDbContext(options), fakeMapper);

            var response = comparisonService.Compare(right, left);

            Assert.Equal("inputs were equal", response.Result);
            Assert.Empty(response.Offsets);
        }

        [Fact]
        public void Comparison_Different_Lengths()
        {
            var right = "Hello";
            var left = "Hellfdsagafsgo";

            var fakeMapper = A.Fake<IMapper>();

            //fake db context since it's not needed
            var options = new DbContextOptionsBuilder<DefaultDbContext>()
           .Options;

            var comparisonService = new ComparisonService(new DefaultDbContext(options), fakeMapper);

            var response = comparisonService.Compare(right, left);

            Assert.Equal("inputs are of different size", response.Result);
            Assert.Empty(response.Offsets);
        }

        [Fact]
        public void Comparison_Equal_Lengths_But_Different_Inputs()
        {
            var right = "Hello";
            var left = "hekkp";

            var fakeMapper = A.Fake<IMapper>();

            //fake db context since it's not needed
            var options = new DbContextOptionsBuilder<DefaultDbContext>()
           .Options;

            var comparisonService = new ComparisonService(new DefaultDbContext(options), fakeMapper);

            var response = comparisonService.Compare(right, left);

            Assert.Null(response.Result);
            Assert.NotEmpty(response.Offsets);
        }
    }
}
