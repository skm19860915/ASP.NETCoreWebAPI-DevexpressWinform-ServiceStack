using System;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using FluentValidation;

namespace Xperters.Admin.UI.Common.Extensions
{
    internal static class ValidatorExtensions
	{
		internal static IRuleBuilderOptions<T, TProperty> WithMandatoryMessage<T, TProperty, TReturn>(this IRuleBuilderOptions<T, TProperty> rule, Expression<Func<T, TReturn>> expression)
		{
			string name = ToFriendlyName(expression);

			return rule.WithMandatoryMessage(name);
		}

		internal static IRuleBuilderOptions<T, TProperty> WithMandatoryMessage<T, TProperty>(this IRuleBuilderOptions<T, TProperty> rule, string name)
		{
			return rule.WithMessage($"{name} is mandatory");
		}

		internal static IRuleBuilderOptions<T, TProperty> WithMustBeGreaterThanMessage<T, TProperty, TReturn, TValue>(this IRuleBuilderOptions<T, TProperty> rule, Expression<Func<T, TReturn>> expression, TValue value)
		{
			string name = ToFriendlyName(expression);
			return rule.WithMessage($"{name} must be greater than {value}");
		}

		internal static IRuleBuilderOptions<T, TProperty> WithMustBeBeforeMessage<T, TProperty, TReturn, TValue>(this IRuleBuilderOptions<T, TProperty> rule, Expression<Func<T, TReturn>> expression, TValue value) where TValue : IComparable<TValue>, IComparable
		{
			string name = ToFriendlyName(expression);
			return rule.WithMessage($"{name} must be before {value}");
		}

		internal static IRuleBuilderOptions<T, TProperty> WithMustBeBetweenMessage<T, TProperty, TReturn, TValue>(this IRuleBuilderOptions<T, TProperty> rule, Expression<Func<T, TReturn>> expression, TValue firstValue, TValue secondValue) where TValue : IComparable<TValue>, IComparable
		{
			string name = ToFriendlyName(expression);
			return rule.WithMessage($"{name} must be between {firstValue} and {secondValue}");
		}

		public static string ToFriendlyName<T, TReturn>(Expression<Func<T, TReturn>> expression)
		{
			//TODO Temp hack
            if (expression?.Body is MemberExpression me)
			{
				return me.Member.Name.ToFriendlyName();
			}

            return expression.ToString();
        }


        public static string ToFriendlyName(this string propertyName)
        {
            return ToSentenceCase(propertyName.ToName());
        }

        public static string ToName(this string propertyName)
        {
            if (propertyName == null)
                return null;
            if (propertyName.EndsWith("Id", StringComparison.InvariantCulture))
                propertyName = propertyName.Replace("Id", string.Empty);
            return propertyName;
        }

        private static string ToSentenceCase(this string str)
        {
            return Regex.Replace(str, "[a-z][A-Z]", m => m.Value[0] + " " + char.ToLower(m.Value[1]));
        }
	}
}
