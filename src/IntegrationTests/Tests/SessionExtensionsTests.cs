﻿using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using IntegrationTests.Models;
using Neo4jMapper;
using NUnit.Framework;

namespace IntegrationTests.Tests
{
    [TestFixture]
    public class SessionExtensionsTests : MoviesFixtureBase
    {
        [Test]
        public void GetNodeShouldPopulateNodeId()
        {
            var result = Session.Run(@"
                MATCH (movie:Movie {title: 'Top Gun'})
                RETURN movie");

            var movie = result.Map<Movie>().SingleOrDefault();

            Assert.IsNotNull(movie);
            Assert.AreNotEqual(movie.Id, default(long));

            // Act
            var node = Session.GetNode<Movie>(movie.Id);

            node.Should().BeEquivalentTo(movie);
        }

        [Test]
        public async Task GetNodeAsyncShouldPopulateNodeId()
        {
            var result = Session.Run(@"
                MATCH (movie:Movie {title: 'Top Gun'})
                RETURN movie");

            var movie = result.Map<Movie>().SingleOrDefault();

            Assert.IsNotNull(movie);
            Assert.AreNotEqual(movie.Id, default(long));

            // Act
            var node = await Session.GetNodeAsync<Movie>(movie.Id);

            node.Should().BeEquivalentTo(movie);
        }

        [Test]
        public void SetNodeShouldUpdateValues()
        {
            var result = Session.Run(@"
                MATCH (movie:Movie {title: 'Top Gun'})
                RETURN movie");

            var movie = result.Map<Movie>().SingleOrDefault();

            Assert.IsNotNull(movie);
            Assert.AreNotEqual(movie.Id, default(long));

            movie.title = "Top Gun 2";

            // Act
            Session.SetNode(movie);

            var node = Session.GetNode<Movie>(movie.Id);

            node.Should().BeEquivalentTo(movie);
        }

        [Test]
        public async Task SetNodeAsyncShouldUpdateValues()
        {
            var result = Session.Run(@"
                MATCH (movie:Movie {title: 'Top Gun'})
                RETURN movie");

            var movie = result.Map<Movie>().SingleOrDefault();

            Assert.IsNotNull(movie);
            Assert.AreNotEqual(movie.Id, default(long));

            movie.title = "Top Gun 2";

            // Act
            await Session.SetNodeAsync(movie);

            var node = Session.GetNode<Movie>(movie.Id);

            node.Should().BeEquivalentTo(movie);
        }
    }
}
