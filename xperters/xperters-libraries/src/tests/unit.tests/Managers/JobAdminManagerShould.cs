using Moq;
using xperters.business;
using xperters.repositories;
using Xunit;
using System.Linq;
using xperters.mockdata;
using System.Collections.Generic;
using xperters.models.DataViews.AdminJob;

namespace xperters.unit.tests.Managers
{
    public class JobAdminManagerShould : BaseUnitTests
    {
        private readonly Mock<IRepositoryReadOnly<JobInformationView>> _jobInfoRepository;


        public JobAdminManagerShould()
        {

            var jobInfos = Mapper.Map<List<JobInformationView>>(JobInfos.Get());
            _jobInfoRepository = new Mock<IRepositoryReadOnly<JobInformationView>>();
            _jobInfoRepository.Setup(x => x.Get(It.IsAny<int>(), It.IsAny<int>()))
                                .Returns(jobInfos.ToList()
                                                    .Take(5)
                                                    .OrderByDescending(x=>x.Created));

        }

        [Fact]
        public void ShouldReturnJobInformation10PerPage()
        {
            var numberPerPage = 5;
            var manager = new JobAdminManager(Mapper
                                            , LoggerFactory
                                            ,_jobInfoRepository.Object);

            var result = manager.GetJobInformation(1, numberPerPage);
            var infoCreated1 = result.ElementAt(0).Created;
            var infoCreated3 = result.ElementAt(2).Created;
            var infoCreated5 = result.ElementAt(4).Created;

            // Check the number of paged items
            Assert.Equal(numberPerPage, result.Count());

            // Check the sort order
            Assert.True(infoCreated1 > infoCreated3);
            Assert.True(infoCreated3 > infoCreated5);
        }
    }
}