using System;
using ServiceStack;
using Microsoft.Extensions.Logging;
using Xperters.Admin.ServiceModel;
using Xperters.Admin.ServiceModel.Exceptions;
using Xperters.Admin.ServiceModel.Milestones;
using xperters.business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Xperters.Admin.ServiceInterface.Services
{
    [Authenticate]
    public class MilestonesService : ServiceBase
    {
        private readonly IMilestoneManager _milestoneManager;
        private readonly ILogger<MilestonesService> _logger;

        public MilestonesService(IMilestoneManager milestoneManager, ILogger<MilestonesService> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _milestoneManager = milestoneManager;
        }

        [RequiresAnyRole(SecurityConstants.UserRoles.AdminRole, SecurityConstants.UserRoles.WriteRole, SecurityConstants.UserRoles.ReadRole)]
        public GetPaymentsForAdminApprovalResponse Get(GetPaymentsForAdminApprovalRequest forAdminApprovalRequest)
        {
            _logger.LogDebug("Get milestone payments for {@request}", forAdminApprovalRequest);

            var milestonePaymentsForAdminApproval = _milestoneManager.GetMilestonePaymentsForApproval(forAdminApprovalRequest.Page, forAdminApprovalRequest.NumberPerPage);

            if (milestonePaymentsForAdminApproval == null)
            {
                throw new XpertersException($"Failed to find data for : {forAdminApprovalRequest.Page}");
            }

            return new GetPaymentsForAdminApprovalResponse
            {
                MilestonePaymentsForAdminApproval = milestonePaymentsForAdminApproval
            };
		}  
        
        [RequiresAnyRole(SecurityConstants.UserRoles.AdminRole, SecurityConstants.UserRoles.WriteRole, SecurityConstants.UserRoles.ReadRole)]
        public PostPaymentsForAdminApprovalResponse Post(PostPaymentsForAdminApprovalRequest request)
        {
            _logger.LogDebug("Admin approved payments", request.MilestoneIdsToApprove);

            var list = _milestoneManager.UpdateMilestonePaymentsForAdminApproval(request.MilestoneIdsToApprove);

            if(list.Count == 0)
                throw new XpertersException($"Failed to update data");

            return new PostPaymentsForAdminApprovalResponse
            {
                UpdatedMilestoneIds = list
            };
		}        
    }
}
