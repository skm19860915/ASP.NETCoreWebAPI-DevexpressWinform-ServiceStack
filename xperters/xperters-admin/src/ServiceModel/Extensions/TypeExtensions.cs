using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Xperters.Admin.ServiceModel.Extensions
{
	public static class TypeExtensions
	{
        public static string GetPropertyPath(this LambdaExpression expression)
		{
			if (expression == null)
				return string.Empty;

			if (expression.Body is UnaryExpression)
			{
				var unex = (UnaryExpression)expression.Body;
				if (unex.NodeType == ExpressionType.Convert)
				{
					Expression ex = unex.Operand;
					MemberExpression mex = (MemberExpression)ex;
					return mex.Member.Name;
				}
			}

			var memberExpression = expression.Body as MemberExpression;
			if (memberExpression == null)
				return string.Empty;

			var memberExpressionOrg = memberExpression;
			string path = string.Empty;
			while (memberExpression?.Expression?.NodeType == ExpressionType.MemberAccess)
			{
				var propInfo = memberExpression.Expression
				  .GetType().GetProperty("Member");
				var propValue = propInfo.GetValue(memberExpression.Expression, null)
				  as PropertyInfo;

				path = $"{propValue.Name}.{path}";

				memberExpression = memberExpression.Expression as MemberExpression;
			}

			return $"{path}{memberExpressionOrg.Member.Name}";
		}

        public static string GetPropertyName(this LambdaExpression expression)
        {
            if (expression?.Body is MemberExpression me)
            {
                return me.Member.Name;
            }

            return string.Empty;
        }
	}

}
