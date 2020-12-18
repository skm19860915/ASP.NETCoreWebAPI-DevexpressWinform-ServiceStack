using xperters.entities;
using System.Linq;
using Xunit;

namespace xperters.unit.tests.Data
{
    public class MasterDataShould
    {
        [Fact]
        public void WhenCategoriesRequested_ReturnNonEmptyList()
        {
            var list = MasterDataFactory.GetCategoryData();
            Assert.True(list.Any());
        }

        [Fact]
        public void WhenSkillsRequested_ReturnNonEmptyList()
        {
            var list = MasterDataFactory.GetSkillData();
            Assert.True(list.Any());
        }
        [Fact]
        public void WhenJobStatusRequested_ReturnNonEmptyList()
        {
            var list = MasterDataFactory.GetJobStatusData();
            Assert.True(list.Any());
        }

        [Fact]
        public void WhenMilestoneStatusRequested_ReturnNonEmptyList()
        {
            var list = MasterDataFactory.GetMilestoneStatusData();
            Assert.True(list.Any());
        }

        [Fact]
        public void WhenPaymentTransactionTypeRequested_ReturnNonEmptyList()
        {
            var list = MasterDataFactory.GetPaymentTransactionTypeData();
            Assert.True(list.Any());
        }

        [Fact]
        public void WhenRequestPayerStatusRequested_ReturnNonEmptyList()
        {
            var list = MasterDataFactory.GetRequestPayerStatusData();
            Assert.True(list.Any());
        }

        [Fact]
        public void WhenCurrenciesRequested_ReturnNonEmptyList()
        {
            var list = MasterDataFactory.GetCurrenciesData();
            Assert.True(list.Any());
        }

        [Fact]
        public void WhenFeesRequested_ReturnNonEmptyList()
        {
            var list = MasterDataFactory.GetFeeStructureData();
            Assert.True(list.Any());
        }        
    }
}
