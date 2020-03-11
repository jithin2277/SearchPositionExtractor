using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SearchPositionExtractor.Data.Tests
{
    [TestClass]
    public class HtmlExtractorIntegrationTests
    {
        private IHtmlExtractor _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new HtmlExtractor();
        }

        [TestMethod]
        public void GetHtml_WhenValidUrl_ReturnsHtmlString()
        {
            var url = "https://www.google.com/search?q=conveyancing+software+australia";
            var result = _sut.GetHtmlString(url).Result;

            Assert.IsNotNull(result);
        }
    }
}
