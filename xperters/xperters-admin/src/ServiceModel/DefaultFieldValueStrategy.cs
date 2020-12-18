using System.Collections.Generic;
using System.Linq;

namespace Xperters.Admin.ServiceModel
{
	public abstract class DefaultFieldValueStrategy<T>
	{
		protected IDictionary<object, RuleSet> RuleSets { get; set; } = new Dictionary<object, RuleSet>();

		public void PopulateDefaults(T item)
		{
			if (item == null)
				return;

			EnsureRulesBound(item);
			ExecuteRules();
		}

		public void LoadExistingDefaults(T item)
		{
			if (item == null)
				return;

			EnsureRulesBound(item);
			DetectRulesThatHavePreviouslyExecutedAndMarkThemAsExecutedAgain();
		}

		public abstract void EnsureRulesBound(T item);

		private void DetectRulesThatHavePreviouslyExecutedAndMarkThemAsExecutedAgain()
		{
			foreach (var rule in RuleSets.Values.SelectMany(o => o.Rules).Where(o => o.HasNotYetExecuted))
			{
				if (rule.ShouldExecute())
					// On load, if a rule has not been flagged as executed, but ShouldExecute returns true, then it was executed on an earlier instance, so mark it as such again.
					rule.SetExecutionCountToAtLeastOne();
			}
		}

		private void ExecuteRules()
		{
			foreach (var rule in RuleSets.Values
				.SelectMany(o => o.Rules)
				.Where(o => o.HasNotYetExecuted))
			{
				rule.Execute();
			}
		}

		protected abstract class Rule
		{
			/// <summary>
			/// If true, allows this rule to be executed even if it has already been executed. If false, only a single execution (per instance) will be allowed.
			/// </summary>
			public bool AllowMultipleExecutions { get; set; } = false;

			public int ExecutionCount { get; private set; } = 0;
			public bool HasNotYetExecuted => ExecutionCount == 0;

			public void SetExecutionCountToAtLeastOne()
			{
				ExecutionCount++;
			}

			public void Execute()
			{
				if (ShouldExecute() && (HasNotYetExecuted || AllowMultipleExecutions))
				{
					ExecuteRule();
					ExecutionCount++;
				}
			}

			protected abstract void ExecuteRule();

			internal abstract bool ShouldExecute();
		}

		protected sealed class RuleSet
		{
			private List<Rule> _rules = new List<Rule>();
			public IEnumerable<Rule> Rules => _rules;

			public RuleSet(IEnumerable<Rule> rules)
			{
				_rules.AddRange(rules);
			}
		}
	}
}