using Microsoft.Extensions.Logging;
using ServiceStack;
using System;
using xperters.business.Interfaces;
using Xperters.Admin.ServiceModel;
using Xperters.Admin.ServiceModel.Exceptions;
using Xperters.Admin.ServiceModel.Jobs;

namespace Xperters.Admin.ServiceInterface.Services
{
    [Authenticate]
    public class JobsService : ServiceBase
    {
        private readonly IJobAdminManager _jobAdminManager;
        private readonly ILogger<JobsService> _logger;

        public JobsService(IJobAdminManager jobAdminManager, ILogger<JobsService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _jobAdminManager = jobAdminManager;
        }

        [RequiresAnyRole(SecurityConstants.UserRoles.AdminRole, SecurityConstants.UserRoles.WriteRole)]
        public GetJobInformationForAdminResponse Get(GetJobInformationForAdminRequest request)
        {
            _logger.LogDebug("Get job information for {@request}", request);

            var jobInformation = _jobAdminManager.GetJobInformation(request.Page, 100);

            if (jobInformation == null)
            {
                throw new XpertersException($"Failed to find data for : {request.Page}");
            }
                
            return new GetJobInformationForAdminResponse
            {
                JobInformation = jobInformation
            };
        }

        [RequiresAnyRole(SecurityConstants.UserRoles.AdminRole, SecurityConstants.UserRoles.WriteRole, SecurityConstants.UserRoles.ReadRole)]
        public GetJobInformationForAdminResponse Post(PostParamsForFilteredJobInformationRequest request)
        {
            _logger.LogDebug("Get job information for {@request}", request);

            var list = _jobAdminManager.GetFilteredJobInformation(request.JobTitle, request.CreatedDate);

            return new GetJobInformationForAdminResponse
            {
                JobInformation = list
            };
        }
    }
}
