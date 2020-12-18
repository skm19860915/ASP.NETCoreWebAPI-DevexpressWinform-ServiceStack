using System;
using System.Linq;
using xperters.domain;
using xperters.entities.Entities;
using xperters.enums;
using xperters.mockdata;
using xperters.models;
using Xunit;

namespace xperters.unit.tests.JobsBidTests
{
    public class JobBidsShould : BaseUnitTests
    {
        [Fact]
        public void JobBidChatSessionDtoMapping()
        {
            var jobBidChatSessionDto = JobBidSessionMock.Get().First();

            // from dto to entity

            var result = Mapper.Map<JobBidChatSession>(jobBidChatSessionDto);

            Assert.NotNull(result);
            Assert.NotEqual(Guid.Empty, result.JobId);
            Assert.NotEqual(Guid.Empty, result.Id);

        }
        

        [Fact]
        public void JobBidChatSessionViewToDtoMapping()
        {
            var id = Guid.Parse("{91000000-0000-0000-0000-000000000001}");
            var jobBidChatSessionId = Guid.Parse("{92000000-0000-0000-0000-000000000001}");
            var jobId = Guid.Parse("{93000000-0000-0000-0000-000000000001}");
            var senderId = Guid.Parse("{94000000-0000-0000-0000-000000000001}");
            var message = "hello";
            int messageType = 12;

            var view = new JobBidChatMessageView
            {
                CreatedDate = DateTime.UtcNow,
                Id = id,
                JobId = jobId,
                SenderId = senderId,
                SenderType = Enums.SenderType.Client,
                JobBidChatSessionId = jobBidChatSessionId,
                Message = message,
                MessageType = messageType
            };

            // from dto to view
            var result = Mapper.Map<JobBidChatMessageDto>(view);
                
            Assert.NotNull(result);

            Assert.NotEqual(Guid.Empty, result.Id);
            Assert.NotEqual(DateTime.MinValue, result.CreatedDate);
            Assert.Equal(id, result.Id);
            Assert.Equal(jobBidChatSessionId, result.JobBidChatSessionId);
            Assert.Equal(senderId, result.SenderId);
            Assert.Equal(message, result.Message);
            Assert.Equal(messageType, result.MessageType);

        }
    }
}
