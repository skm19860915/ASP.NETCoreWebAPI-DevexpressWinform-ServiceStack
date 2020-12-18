using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.RegularExpressions;
using FluentValidation;
using Xperters.Admin.ServiceModel.Extensions;
using Xperters.Admin.ServiceModel.Validations;
using Xperters.Admin.UI.Common.Extensions;

namespace Xperters.Admin.UI.Common.Validators
{
    public abstract class Validator<T> : AbstractValidator<T>
    {

        public virtual IPropertyTitleProvider PropertyTitleProvider { get; private set; } = new NullPropertyTitleProvider();
        public virtual string PathContext { get; private set; } = string.Empty;

        public string ToFriendlyName<TReturn>(Expression<Func<T, TReturn>> expression)
        {
            var me = expression?.Body as MemberExpression;
            if (me != null)
            {
                return me.Member.Name.ToFriendlyName();
            }

            return expression.ToString();
        }

        public IRuleBuilderOptions<T, string> RuleForReference(Expression<Func<T, string>> expression, Regex regex)
        {
            return RuleFor(expression)
                .Must(o => regex.IsMatch(o))
                .When(o => expression.Compile()(o) != null);
        }

        public IRuleBuilderOptions<T, long> RuleForMandatoryLookup(Expression<Func<T, long>> expression)
        {
            return RuleFor(expression)
                .GreaterThan(default(long))
                .WithMandatoryMessage(expression);
        }

        public IRuleBuilderOptions<T, int> RuleForMandatoryLookup(Expression<Func<T, int>> expression)
        {
            return RuleFor(expression)
                .GreaterThan(default(int))
                .WithMandatoryMessage(expression);
        }

        public IRuleBuilderOptions<T, TValue> RuleForMandatoryLookup<TValue>(Expression<Func<T, TValue>> expression) where TValue : struct
        {
            return RuleFor(expression)
                .IsInEnum()
                .NotEqual(default(TValue));
        }

        public IRuleBuilderOptions<T, TValue?> RuleForMandatoryLookup<TValue>(Expression<Func<T, TValue?>> expression) where TValue : struct
        {
            return RuleFor(expression)
                .Must(o => o.HasValue)
                .WithMandatoryMessage(expression);
        }

        public IRuleBuilderOptions<T, TValue> RuleForMandatoryValue<TValue>(Expression<Func<T, TValue>> expression) where TValue : IComparable<TValue>, IComparable
        {
            return RuleFor(expression).GreaterThanOrEqualTo(default(TValue))
                .WithMandatoryMessage(expression);
        }

        public IRuleBuilderOptions<T, TValue?> RuleForMandatoryValue<TValue>(Expression<Func<T, TValue?>> expression) where TValue : struct, IComparable<TValue>, IComparable
        {
            return RuleFor(expression).GreaterThanOrEqualTo(default(TValue))
                .WithMandatoryMessage(expression);
        }

        public IRuleBuilderOptions<T, TValue> RuleForMandatoryLookup<TValue>(Expression<Func<T, TValue>> expression, string customTitle) where TValue : IComparable<TValue>, IComparable
        {
            return RuleFor(expression).GreaterThan(default(TValue))
                .WithMandatoryMessage(customTitle);
        }

        public IRuleBuilderOptions<T, TValue> RuleForOptionalLookup<TValue>(Expression<Func<T, TValue>> expression) where TValue : struct
        {
            string name = ToFriendlyName(expression);

            return RuleFor(expression)
                .IsInEnum();
        }

        public IRuleBuilderOptions<T, string> RuleForMandatoryString(Expression<Func<T, string>> expression)
        {
            return RuleFor(expression).NotEmpty()
                .WithMandatoryMessage(expression);
        }

        public IRuleBuilderOptions<T, string> RuleForMandatoryString(Expression<Func<T, string>> expression, string customTitle)
        {
            return RuleFor(expression).NotEmpty()
                .WithMandatoryMessage(customTitle);
        }

        public IRuleBuilderOptions<T, TValue> RuleForValueBetween<TValue>(Expression<Func<T, TValue>> expression, TValue firstValue, TValue secondValue) where TValue : IComparable<TValue>, IComparable
        {
            return RuleFor(expression).InclusiveBetween(firstValue, secondValue)
                .WithMustBeBetweenMessage(expression, firstValue, secondValue);
        }

        public IRuleBuilderOptions<T, TValue?> RuleForValueBetween<TValue>(Expression<Func<T, TValue?>> expression, TValue firstValue, TValue secondValue) where TValue : struct, IComparable<TValue>, IComparable
        {
            var nullableDecomposition = DecomposeNullable(expression);
            return RuleFor(expression).InclusiveBetween(firstValue, secondValue)
                .WithMustBeBetweenMessage(expression, firstValue, secondValue)
                .When(nullableDecomposition.hasValueFunc);
        }

        public IRuleBuilderOptions<T, decimal> RuleForPercentage(Expression<Func<T, decimal>> expression)
        {
            return RuleForValueBetween(expression, 0, 1);
        }

        public IRuleBuilderOptions<T, decimal?> RuleForPercentageWhenNotNull(Expression<Func<T, decimal?>> expression)
        {
            return RuleForValueBetween(expression, 0, 1);
        }

        public IRuleBuilderOptions<T, DateTime> RuleForReasonableDate(Expression<Func<T, DateTime>> expression)
        {
            DateTime minDate = DateTime.Now.AddYears(-50);
            DateTime maxDate = DateTime.Now.AddYears(50);
            return RuleForValueBetween(expression, minDate, maxDate);
        }

        public IRuleBuilderOptions<T, DateTime?> RuleForReasonableDateWhenNotNull(Expression<Func<T, DateTime?>> expression)
        {
            DateTime minDate = DateTime.Now.AddYears(-50);
            DateTime maxDate = DateTime.Now.AddYears(50);
            return RuleForValueBetween(expression, minDate, maxDate);
        }

        public IRuleBuilderOptions<T, DateTime> RuleForReasonableDateWhenNotNull(Expression<Func<T, IEnumerable<DateTime>>> expression)
        {
            DateTime minDate = DateTime.Now.AddYears(-50);
            DateTime maxDate = DateTime.Now.AddYears(50);

            return RuleForEach(expression).InclusiveBetween(minDate, maxDate)
                .WithMustBeBetweenMessage(expression, minDate, maxDate);
        }

        public IRuleBuilderOptions<T, DateTime> RuleForSuccessiveDate(Expression<Func<T, DateTime>> expression, Expression<Func<T, DateTime>> initialDateExpression)
        {
            return RuleFor(expression)
                .GreaterThanOrEqualTo(initialDateExpression)
                .WithMustBeGreaterThanMessage(expression, ToFriendlyName(initialDateExpression));
        }

        public IRuleBuilderOptions<T, DateTime?> RuleForSuccessiveDateWhenNotNull(Expression<Func<T, DateTime?>> endDateExpression, Expression<Func<T, DateTime?>> startDateExpression)
        {
            var expressionNullableDecomposition = DecomposeNullable(endDateExpression);
            var initialExpressionNullableDecomposition = DecomposeNullable(startDateExpression);

            return RuleFor(endDateExpression)
                .GreaterThanOrEqualTo(startDateExpression)
                .WithMustBeGreaterThanMessage(expressionNullableDecomposition.valueExpression, ToFriendlyName(startDateExpression))
                .When(o => expressionNullableDecomposition.hasValueFunc(o) && initialExpressionNullableDecomposition.hasValueFunc(o));
        }

        public IRuleBuilderOptions<T, TValue> RuleForPositiveValue<TValue>(Expression<Func<T, TValue>> expression) where TValue : IComparable<TValue>, IComparable
        {
            return RuleFor(expression)
                .GreaterThan(default(TValue))
                .WithMustBeGreaterThanMessage(expression, 0);
        }

        public IRuleBuilderOptions<T, TValue?> RuleForPositiveValueWhenNotNull<TValue>(Expression<Func<T, TValue?>> expression) where TValue : struct, IComparable<TValue>, IComparable
        {
            var nullableDecomposition = DecomposeNullable(expression);

            return RuleFor(expression)
                .GreaterThan(default(TValue))
                .WithMustBeGreaterThanMessage(nullableDecomposition.valueExpression, 0)
                .When(nullableDecomposition.hasValueFunc);
        }

        protected (Expression<Func<T, TValue>> valueExpression, Func<T, bool> hasValueFunc) DecomposeNullable<TValue>(Expression<Func<T, TValue?>> expression) where TValue : struct
        {
            var valueExpression = Expression.Lambda<Func<T, TValue>>(
                Expression.PropertyOrField(expression.Body, "Value"),
                expression.Parameters);

            var hasValueExpression = Expression.Lambda<Func<T, bool>>(
                Expression.PropertyOrField(expression.Body, "HasValue"),
                expression.Parameters);

            return (valueExpression, hasValueExpression.Compile());
        }

        protected string ToPropertyName<TReturn>(Expression<Func<T, TReturn>> propertyExpression)
        {
            return propertyExpression.GetPropertyName();
        }

        internal Validator<T> WithPropertyTitleProvider(IPropertyTitleProvider propertyTitleProvider)
        {
            if (propertyTitleProvider == null)
                PropertyTitleProvider = new NullPropertyTitleProvider();
            else
                PropertyTitleProvider = propertyTitleProvider;

            return this;
        }

        internal Validator<T> WithPathContext(string pathContext)
        {
            if (pathContext == null)
                PathContext = string.Empty;
            else
                PathContext = pathContext;

            return this;
        }


    }
}