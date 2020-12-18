using ServiceStack;
using System;
using System.Collections.Generic;
using System.Text;
using xperters.domain;

namespace Xperters.Admin.ServiceModel.Jobs
{
    public sealed class GetJobInformationForAdminResponse : Response
    {
        [ApiMember(IsRequired = true)]
        public IEnumerable<JobInformationDto> JobInformation{ get; set; }
    }
}
