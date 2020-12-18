using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xperters.domain;
using Xperters.Admin.ServiceModel.Withdrawals;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Common.Extensions;
using Xperters.Admin.UI.Common.Mediator;
using Xperters.Admin.UI.Tabs.ServiceInterfaces;

namespace Xperters.Admin.UI.Tabs.PaymentTab
{
    public class PaymentWithdrawalsPresenter
    {
        private readonly IPaymentAdminWithdrawalsTabView _view;
        private readonly IWithdrawalsServiceClient _withdrawalsServiceClient;

        private IPaymentAdminWithdrawalsTabView View { get; }
        private AuthenticationInfo AuthenticationInfo { get; }
        private IMediator Mediator { get; }

        public PaymentWithdrawalsPresenter(IPaymentAdminWithdrawalsTabView view
            , IWithdrawalsServiceClient withdrawalsServiceClient
            , AuthenticationInfo authenticationInfo
            , IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _view = view;
            _withdrawalsServiceClient = withdrawalsServiceClient ?? throw new ArgumentException(nameof(withdrawalsServiceClient));

            AuthenticationInfo = authenticationInfo ?? throw new ArgumentNullException(nameof(authenticationInfo));
            View = view ?? throw new ArgumentNullException(nameof(view));
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
                Console.WriteLine(ex);
            }
        }

        private async Task LoadDataAsync()
        {
            await RefreshPaymentOutgoingList();
        }

        private async Task RefreshPaymentOutgoingList()
        {
            try
            {
                var data = await _withdrawalsServiceClient.GetAsync(request:
                        new GetPaymentWithdrawalsRequest
                        {
                        }
                    );

                var list = data.PaymentOutgoingForWithdrawals.ToList();
                _view.PaymentOutgoingList.AddRange(list);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private void ViewClosing(object sender, EventArgs e)
        {
            Mediator.DeregisterAll(this);
        }

        internal async Task<List<PaymentOutgoingDto>> SendRefreshRequest()
        {
            List<PaymentOutgoingDto> list;
            try
            {
                var data = await _withdrawalsServiceClient.GetAsync(request:
                        new GetPaymentWithdrawalsRequest
                        {
                        }
                    );

                list = data.PaymentOutgoingForWithdrawals.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            return list;
        }
    }
}
