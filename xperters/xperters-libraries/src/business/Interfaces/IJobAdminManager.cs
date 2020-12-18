using System.Collections.Generic;
using xperters.domain;

namespace xperters.business.Interfaces
{
    public interface IJobAdminManager
    {
        IEnumerable<JobInformationDto> GetJobInformation(int page, int pageSize);
        IEnumerable<JobInformationDto> GetFilteredJobInformation(string jobTitle, string createdDate);
    }
}