using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xperters.domain;
using Xperters.Admin.ServiceModel.Incoming;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Common.Extensions;
using Xperters.Admin.UI.Common.Mediator;
using Xperters.Admin.UI.Tabs.ServiceInterfaces;

namespace Xperters.Admin.UI.Tabs.PaymentTab
{
    public class PaymentIncomingPresenter
    {
        private readonly IPaymentAdminIncomingTabView _view;
        private readonly IPaymentsIncomingServiceClient _incomingServiceClient;

        private IPaymentAdminIncomingTabView View { get; }
        private AuthenticationInfo AuthenticationInfo { get; }
        private IMediator Mediator { get; }

        public PaymentIncomingPresenter(IPaymentAdminIncomingTabView view
                                        , IPaymentsIncomingServiceClient incomingServiceClient
                                        , AuthenticationInfo authenticationInfo
                                        , IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _view = view;
            _incomingServiceClient = incomingServiceClient ?? throw new ArgumentException(nameof(incomingServiceClient));

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
            await RefreshPaymentIncomingList();
        }

        private async Task RefreshPaymentIncomingList()
        {
            try
            {
                var data = await _incomingServiceClient.GetAsync(request:
                        new GetPaymentIncomingRequest
                        {
                        }
                    );

                var list = data.PaymentIncoming.ToList();
                _view.PaymentIncomingList.AddRange(list);
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

        internal async Task<List<PaymentIncomingDto>> SendRefreshRequest()
        {
            List<PaymentIncomingDto> list;
            try
            {
                var data = await _incomingServiceClient.GetAsync(request:
                        new GetPaymentIncomingRequest
                        {
                        }
                    );

                list = data.PaymentIncoming.ToList();
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
