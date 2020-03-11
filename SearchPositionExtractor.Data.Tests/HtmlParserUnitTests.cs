using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;

namespace SearchPositionExtractor.Data.Tests
{
    [TestClass]
    public class HtmlParserUnitTests
    {
        private Mock<IHtmlExtractor> _mockHtmlExtractor;
        private IHtmlParser _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockHtmlExtractor = new Mock<IHtmlExtractor>(MockBehavior.Strict);
            _sut = new HtmlParser(_mockHtmlExtractor.Object);
        }

        [TestCleanup]
        public void TestCleanup()
        {
            _mockHtmlExtractor.VerifyAll();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Ctor_WhenHtmlExtractorIsNull_ThrowsException()
        {
            _ = new HtmlParser(null);
        }

        [TestMethod]
        public void GetMatchingElements_WhenUrlAndOrExpressionIsNull_ThrowsException()
        {
            try
            {
                _ = _sut.GetMatchingElements(null, null).Result;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
            }

            try
            {
                _ = _sut.GetMatchingElements(null, "foobar").Result;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
            }

            try
            {
                _ = _sut.GetMatchingElements("foobar", null).Result;
            }
            catch (Exception ex)
            {
                Assert.IsInstanceOfType(ex.InnerException, typeof(ArgumentNullException));
            }
        }

        [TestMethod]
        public void GetMatchingElements_WhenValidUrlAndExpression_ReturnsMatchedElements()
        {
            string url = "https://www.google.com";
            var expected = new List<string> { @"<div class=""ZINbbc xpd O9g5cc uUPGi"">Some Company</div>", @"<div class=""ZINbbc xpd O9g5cc uUPGi"">Smokeball</div>" };
            _mockHtmlExtractor.Setup(s => s.GetHtmlString(url)).ReturnsAsync(string.Join("", expected));
            var actual = _sut.GetMatchingElements(url, Constants.GOOGLE_SEARCHRESULTS_PARSER_EXPRESSION).Result;

            Assert.AreEqual(expected.Count, actual.Count);
            for (int i = 0; i < expected.Count; i++)
            {
                Assert.AreEqual(expected[i], actual[i]);
            }
        }
    }
}
