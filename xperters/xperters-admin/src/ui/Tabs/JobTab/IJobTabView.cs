using System.ComponentModel;
using xperters.domain;
using Xperters.Admin.UI.Common;

namespace Xperters.Admin.UI.Tabs.JobTab
{
    public interface IJobTabView : IView
    {
        void BindGrids();
        BindingList<JobInformationDto> JobInformationList { get; }
    }
}
