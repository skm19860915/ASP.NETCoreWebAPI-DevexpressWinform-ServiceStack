using System;
using System.Linq.Expressions;
using Xperters.Admin.ServiceModel.Extensions;
using Xperters.Admin.ServiceModel.Validations;
using Xperters.Admin.UI.Common.Validators;

namespace Xperters.Admin.UI.Common.LayerProgram
{
    public class ProgramValidator : Validator<PaymentGridViewModel>
    {
        public ProgramValidator(IPropertyTitleProvider propertyTitleProvider)
        {

        }

        protected string ToPropertyPath<TReturn>(Expression<Func<PaymentGridViewModel, TReturn>> propertyExpression)
        {
            return propertyExpression.GetPropertyPath();
        }
    }
}
