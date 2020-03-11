using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace SearchPositionExtractor.Data.Tests
{
    [TestClass]
    public class HtmlParserIntegrationTests
    {
        private IHtmlParser _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new HtmlParser(new HtmlExtractor());
        }


        [TestMethod]
        public void GetElements_WhenValidUrlAndName_ReturnsHtmlString()
        {
            var url = "https://www.google.com/search?q=conveyancing+software+australia";
            var result = _sut.GetMatchingElements(url, @"<div class=""ZINbbc\sxpd\sO9g5cc\suUPGi"">(.*?)<\/div>").Result;

            Assert.IsNotNull(result);
        }
    }
}
