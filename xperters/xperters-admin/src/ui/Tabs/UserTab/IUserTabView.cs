using System.ComponentModel;
using xperters.domain;
using Xperters.Admin.UI.Common;

namespace Xperters.Admin.UI.Tabs.UserTab
{
    public interface IUserTabView : IView
    {
        void BindGrids();
        BindingList<UserInfoDto> UserInfoList { get; }
    }
}
