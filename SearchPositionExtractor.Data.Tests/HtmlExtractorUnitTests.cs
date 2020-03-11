using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace SearchPositionExtractor.Data.Tests
{
    [TestClass]
    public class HtmlExtractorUnitTests
    {
        private IHtmlExtractor _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new HtmlExtractor();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void GetHtml_WhenUrlIsNull_ThrowsException()
        {
            try
            {
                _ = _sut.GetHtmlString(null).Result;
            }
            catch (AggregateException ex)
            {
                throw ex.InnerException;
            }
        }
    }
}
