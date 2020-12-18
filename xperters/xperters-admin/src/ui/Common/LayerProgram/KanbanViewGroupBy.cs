using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Xperters.Admin.UI.Tabs.MilestoneAdminApprovals;

namespace Xperters.Admin.UI.Common.LayerProgram
{
	public class KanbanViewGroupBy
	{
		public class GroupFilterOptions
		{
			public IEnumerable<object> Items { get; set; }
			public string DisplayMember { get; set; }
			public string ValueMember { get; set; }
			public Action ApplySort { get; set; }
			public string DisplayName { get; set; }
		}

		public Func<object, PaymentGridViewModel,Task> ChangeViewModelValueActionAsync { get; set; }

		public string DisplayName { get; set; }
		public Expression<Func<PaymentGridViewModel, object>> PropertyPathExpression { get; }
		public string PropertyPath { get; }
		public GroupFilterOptions FilterOptions { get; set; }
		public Action ProgramFilterAction { get; }
		public bool AllowDrag { get; set; }
		public Action<IPaymentAdminApprovalTabView, KanbanViewGroupBy> CreateDummyPrograms { get; }

		public KanbanViewGroupBy(string displayName,
			Expression<Func<PaymentGridViewModel,object>> propertyPathExpression,
			string path, // NOTE : passing in this path since the above expression is trying to Convert to an object
			Func<object, PaymentGridViewModel, Task> changeValueAction,
			GroupFilterOptions filterOptions,
			Action programFilterAction = null,
			bool allowDrag = true,
			Action<IPaymentAdminApprovalTabView, KanbanViewGroupBy> createDummyPrograms = null)
		{
			DisplayName = displayName ?? throw new ArgumentException(nameof(displayName));
			PropertyPathExpression = propertyPathExpression ?? throw new ArgumentNullException(nameof(propertyPathExpression));
			ChangeViewModelValueActionAsync = changeValueAction ?? throw new ArgumentException(nameof(changeValueAction));
			FilterOptions = filterOptions ?? throw new ArgumentException(nameof(filterOptions));

			PropertyPath = path;
			AllowDrag = allowDrag;
			CreateDummyPrograms = createDummyPrograms;
			ProgramFilterAction = programFilterAction;
		}
	}
}