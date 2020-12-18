using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xperters.domain;
using Xperters.Admin.ServiceModel.Extensions;
using Xperters.Admin.ServiceModel.Milestones;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Common.Extensions;
using Xperters.Admin.UI.Common.LayerProgram;
using Xperters.Admin.UI.Common.Mediator;
using Xperters.Admin.UI.Tabs.ServiceInterfaces;
using Xperters.Core.Logging;

namespace Xperters.Admin.UI.Tabs.MilestoneAdminApprovals
{
    public class PaymentAdminApprovalPresenter
    {
        private readonly IPaymentAdminApprovalTabView _view;
        private readonly IMilestonesServiceClient _milestonesServiceClient;

        private IPaymentAdminApprovalTabView View { get; }
        private IMediator Mediator { get; }
        private ILogger Logger { get; }

        private AuthenticationInfo AuthenticationInfo { get; }

        public PaymentAdminApprovalPresenter(IPaymentAdminApprovalTabView view
			, IMilestonesServiceClient milestonesServiceClient
            , AuthenticationInfo authenticationInfo
            , IMediator mediator
			, ILogger logger
            )
		{
			Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _view = view;
            _milestonesServiceClient = milestonesServiceClient ?? throw new ArgumentNullException(nameof(milestonesServiceClient));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

            AuthenticationInfo = authenticationInfo ?? throw new ArgumentNullException(nameof(authenticationInfo));
            View = view ?? throw new ArgumentNullException(nameof(view));
            Logger = logger;
            view.HandleDestroyed += ViewClosing;
            ((IView)view).Load += ViewLoad;

        }

        private async void ViewLoad(object sender, EventArgs e)
        {
            try
            {
                await LoadDataAsync().ConfigureAwait(false);

                View.SafelyUpdateUi(() =>
                {
                    View.BindGrids();
                });
            }
            catch (Exception ex)
            {
                await ex.SendToMediatorAsync(Mediator).ConfigureAwait(false);
                Logger.Error(ex, "Failed to load Programs'");
            }
        }

        private async Task LoadDataAsync()
        {
            await RefreshPaymentsList();

        }

		private async Task RefreshPaymentsList()
		{
			try
            {
                var data = await _milestonesServiceClient.GetAsync(request: 
                        new GetPaymentsForAdminApprovalRequest
                        {
                            NumberPerPage = 10,
                            Page = 1
                        }
                    );

                var payments = data.MilestonePaymentsForAdminApproval.ToList();
                _view.Payments.ClearAndAddRange(payments);

                _view.PaymentViewModels  = payments.Select(x => x.ToViewModel()).ToListOrEmptyIfNull();
                _view.PaymentsAwaitingApproval.AddRange(_view.PaymentViewModels);

            }
			catch (Exception ex)
			{
				Logger.Error(ex, "Failed To Refresh File Reference Sheet Uploads on GenerateEarnings Tab");
				await ex.SendToMediatorAsync(Mediator, "Failed To Refresh File Reference Sheet Uploads on GenerateEarnings Tab").ConfigureAwait(false);
			}
		}

        private void ViewClosing(object sender, EventArgs e)
        {
            Mediator.DeregisterAll(this);
        }

        internal async Task<List<Guid>> SendData(IEnumerable<MilestonePaymentViewModel> data)
        {
            var milestoneIds = data.Select(x => x.MilestoneId).ToList();

            try
            {
                var result = await _milestonesServiceClient.PostAsync(request: new PostPaymentsForAdminApprovalRequest { 
                    MilestoneIdsToApprove = milestoneIds,
                    Version = 1
                });

                var approvedMilestoneIds = result.UpdatedMilestoneIds;

                return approvedMilestoneIds;
            }
            catch (Exception ex)
            {
                Logger.Error(ex, "Failed to save data");
            }

            return null;
        }

        internal async Task<List<MilestonePaymentDto>> SendRefreshRequest()
        {
            List<MilestonePaymentDto> list;
            try
            {
                var data = await _milestonesServiceClient.GetAsync(request:
                    new GetPaymentsForAdminApprovalRequest
                    {
                        NumberPerPage = 10,
                        Page = 1
                    }
                );

                list = data.MilestonePaymentsForAdminApproval.ToList();
            }
            catch (Exception e)
            {
                Logger.Error(e, "Failed to refresh data");
                return null;
            }

            return list;
        }
    }
}
