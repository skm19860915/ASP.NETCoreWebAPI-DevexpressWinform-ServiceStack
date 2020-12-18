using Xunit;
using Moq;
using Microsoft.AspNetCore.Http;
using xperters.fileutilities.Files;

namespace xperters.unit.tests.FileHandlers
{
    public class HttpFileHandlerShould : BaseUnitTests
    {
        private readonly Mock<IHttpContextAccessor> _contextAccessor;
        private readonly FormFileCollection _collection;

        public HttpFileHandlerShould()
        {
            _collection = CreateFormFileCollection();

            _contextAccessor = new Mock<IHttpContextAccessor>();
        }

        [Fact]
        public void ReturnFilesCollection_WhenFileSubmittedByForm()
        {
            _contextAccessor.Setup(x => x.HttpContext.Request.Form.Files).Returns(_collection);

            var handler = new HttpFileHandler(_contextAccessor.Object);
            var files = handler.GetFromFiles();

            Assert.Equal(1, files.Count);
            var firstFile = files[0];

            Assert.Equal(1, files.Count);
            Assert.Equal(FileName, firstFile.FileName);
            Assert.Equal(ContentType, firstFile.ContentType);
            Assert.Equal(ContentDisposition, firstFile.ContentDisposition);
            Assert.True(firstFile.Length > 0);
        }

        [Fact]
        public void ReturnEmptyCollection_WhenNoFileSubmittedByForm()
        {
            _contextAccessor.Setup(x => x.HttpContext.Request.Form.Files).Returns(new FormFileCollection());
            var handler = new HttpFileHandler(_contextAccessor.Object);
            var files = handler.GetFromFiles();

            Assert.Equal(0, files.Count);
        }
    }
}
