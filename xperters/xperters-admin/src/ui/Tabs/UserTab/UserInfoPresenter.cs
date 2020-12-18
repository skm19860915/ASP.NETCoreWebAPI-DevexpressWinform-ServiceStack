using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xperters.domain;
using Xperters.Admin.ServiceModel.Users;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Common.Extensions;
using Xperters.Admin.UI.Common.Mediator;
using Xperters.Admin.UI.Tabs.ServiceInterfaces;
using Xperters.Core.Logging;

namespace Xperters.Admin.UI.Tabs.UserTab
{
    public class UserInfoPresenter
    {
        private readonly IUserTabView _view;
        private readonly IUserServiceClient _userServiceClient;

        private IUserTabView View { get; }
        private AuthenticationInfo AuthenticationInfo { get; }
        private IMediator Mediator { get; }
        private ILogger Logger { get; }

        public UserInfoPresenter(IUserTabView view
            , IUserServiceClient userServiceClient
            , AuthenticationInfo authenticationInfo
            , IMediator mediator
            , ILogger logger)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _view = view;
            _userServiceClient = userServiceClient ?? throw new ArgumentException(nameof(userServiceClient));

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
                Console.WriteLine(ex);
            }
        }

        private async Task LoadDataAsync()
        {
            await RefreshJobInformationList();
        }

        private async Task RefreshJobInformationList()
        {
            try
            {
                var data = await _userServiceClient.GetAsync(request:
                        new GetUserInfoForAdminRequest
                        {
                            Page = 1
                        }
                    );

                var jobs = data.UserInfos.ToList();
                _view.UserInfoList.AddRange(jobs);
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

        internal async Task<List<UserInfoDto>> SendRefreshRequest()
        {
            List<UserInfoDto> list;
            try
            {
                var data = await _userServiceClient.GetAsync(request:
                    new GetUserInfoForAdminRequest
                    {
                        Page = 1
                    }
                );

                list = data.UserInfos.ToList();
            }
            catch (Exception e)
            {
                Logger.Error(e, "Failed to refresh data");
                return null;
            }

            return list;
        }

        internal async Task<List<UserInfoDto>> SendFilterRequest(string name, string date)
        {
            List<UserInfoDto> list;
            try
            {
                var data = await _userServiceClient.PostAsync(request:
                        new PostParamsForFilteredUserInfoRequest
                        {
                            Name = name,
                            CreatedDate = date,
                            Version = 1
                        }
                    );

                list = data.UserInfos.ToList();
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
