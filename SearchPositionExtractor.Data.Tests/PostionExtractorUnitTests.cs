using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace SearchPositionExtractor.Data.Tests
{
    [TestClass]
    public class PostionExtractorUnitTests
    {
        private  Mock<IHtmlParser> _mockHtmlParser;
        private IPostionExtractor _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHtmlParser = new Mock<IHtmlParser>(MockBehavior.Strict);
            _sut = new PostionExtractor(_mockHtmlParser.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockHtmlParser.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_WhenHtmlParserIsNull_ThrowsException()
        {
            _ = new PostionExtractor(null);
        }

        [TestMethod]
        public void GetPositions_WhenArgumentsAreNull_ThrowsException()
        {
            try
            {
                _ = _sut.GetPositions(null, null).Result;
            }
            catch (AggregateException ex)
            {
                Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));                
            }

            try
            {
                _ = _sut.GetPositions(null, "foobar").Result;
            }
            catch (AggregateException ex)
            {
                Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
            }

            try
            {
                _ = _sut.GetPositions("foobar", null).Result;
            }
            catch (AggregateException ex)
            {
                Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void GetPositions_WhenArgumentsAreValid_ReturnsPostionOfSearchTerm()
        {
            var url = "www.test.com";
            var elements = new List<string> { @"<div class=""ZINbbc xpd O9g5cc uUPGi"">Some Company</div>", @"<div class=""ZINbbc xpd O9g5cc uUPGi"">Smokeball</div>" };
            _mockHtmlParser.Setup(s => s.GetMatchingElements(url, Constants.GOOGLE_SEARCHRESULTS_PARSER_EXPRESSION)).ReturnsAsync(elements);
            var result = _sut.GetPositions(url, "smokeball").Result;

            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0] == 2);
        }

        [TestMethod]
        public void GetPositions_WhenArgumentsAreValidAndSearchTermDoesnotExist_ReturnsZero()
        {
            var url = "www.test.com";
            var elements = new List<string> { @"<div class=""ZINbbc xpd O9g5cc uUPGi"">Some Company</div>", @"<div class=""ZINbbc xpd O9g5cc uUPGi"">Foobar</div>" };
            _mockHtmlParser.Setup(s => s.GetMatchingElements(url, Constants.GOOGLE_SEARCHRESULTS_PARSER_EXPRESSION)).ReturnsAsync(elements);
            var result = _sut.GetPositions(url, "smokeball").Result;

            Assert.IsTrue(result.Count == 0);
        }
    }
}
