using Xunit;
using Moq;
using xperters.fileutilities.Interfaces;
using xperters.mockdata;
using xperters.tests.common.Base;
using xperters.infrastructure.Converters;
using xperters.entities.Entities;

namespace xperters.unit.tests.FileHandlers
{
   public class MilestoneAttachmentReaderShould:BaseTests
    {
        private readonly Mock<IBlobService> _blobService;

        public MilestoneAttachmentReaderShould()
        {
            _blobService = new Mock<IBlobService>();

        }
        [Fact]
        public void ConvertFileFromMilestoneReader()
        {
            var reader = new MilestoneAttachmentReader(_blobService.Object);

            var milestoneAttachmentDto = MilestoneAttachmentMock.milestoneAttachment1;

            var milestoneAttachment = Mapper.Map<MilestoneAttachment>(milestoneAttachmentDto);

            var result = reader.Convert(milestoneAttachment, milestoneAttachmentDto, null);

            Assert.True(result.Id != null);
        }
    }
}
