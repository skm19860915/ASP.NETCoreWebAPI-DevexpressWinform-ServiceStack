using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using xperters.domain;
using Xperters.Admin.ServiceModel.Jobs;
using Xperters.Admin.UI.Common;
using Xperters.Admin.UI.Common.Extensions;
using Xperters.Admin.UI.Common.Mediator;
using Xperters.Admin.UI.Tabs.ServiceInterfaces;

namespace Xperters.Admin.UI.Tabs.JobTab
{
    public class JobInformationPresenter
    {
        private readonly IJobTabView _view;
        private readonly IJobsServiceClient _jobsServiceClient;

        private IJobTabView View { get; }
        private AuthenticationInfo AuthenticationInfo { get; }
        private IMediator Mediator { get; }

        private bool eventStatus;
        private string targetTitleName;

        GetJobInformationForAdminResponse data;

        public JobInformationPresenter(IJobTabView view
            , IJobsServiceClient jobsServiceClient
            , AuthenticationInfo authenticationInfo
            , IMediator mediator)
        {
            Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _view = view;
            _jobsServiceClient = jobsServiceClient ?? throw new ArgumentException(nameof(jobsServiceClient));

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
            catch(Exception ex)
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
                if (eventStatus)
                {
                    data = await _jobsServiceClient.PostAsync(request:
                       new PostParamsForFilteredJobInformationRequest
                       {
                           JobTitle = targetTitleName,
                           CreatedDate = null,
                           Version = 1
                       }
                   );

                    var jobs = data.JobInformation.Where(x => x.JobTitle == targetTitleName);
                    _view.JobInformationList.AddRange(jobs);
                }
                else
                {
                    data = await _jobsServiceClient.GetAsync(request:
                        new GetJobInformationForAdminRequest
                        {
                            Page = 1
                        }
                    );

                    var jobs = data.JobInformation.ToList();
                    _view.JobInformationList.AddRange(jobs);
                }
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

        internal async Task<List<JobInformationDto>> SendFilterRequest(string title, string date)
        {
            List<JobInformationDto> list;
            try
            {
                if (string.IsNullOrEmpty(title) && string.IsNullOrEmpty(date))
                {
                    data = await _jobsServiceClient.GetAsync(request:
                        new GetJobInformationForAdminRequest
                        {
                            Page = 1
                        }
                    );
                }
                else
                {
                    data = await _jobsServiceClient.PostAsync(request:
                        new PostParamsForFilteredJobInformationRequest
                        {
                            JobTitle = title,
                            CreatedDate = date,
                            Version = 1
                        }
                    );
                }

                list = data.JobInformation.ToList();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }

            return list;
        }

        public void GoToJobTab(System.Windows.Forms.TabControl control, string value)
        {
            eventStatus = true;
            targetTitleName = value;
            control.SelectedIndex = 1;
        }
    }
}
