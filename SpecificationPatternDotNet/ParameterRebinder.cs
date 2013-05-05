using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace SpecificationPatternDotNet
{
    using ParameterExpressionMap = Dictionary<ParameterExpression, ParameterExpression>;

    /// <remarks>
    ///     Derived from <a href="http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx">MSDN</a>.
    /// </remarks>
    internal class ParameterRebinder : ExpressionVisitor
    {
        private readonly ParameterExpressionMap _parameterExpressionMap;

        private ParameterRebinder(ParameterExpressionMap parameterExpressionMap)
        {
            if (parameterExpressionMap == null) throw new ArgumentNullException("parameterExpressionMap");

            _parameterExpressionMap = parameterExpressionMap;
        }

        public static Expression ReplaceParameters(ParameterExpressionMap parameterExpressionMap,
                                                   Expression expression)
        {
            if (parameterExpressionMap == null) throw new ArgumentNullException("parameterExpressionMap");
            if (expression == null) throw new ArgumentNullException("expression");

            return new ParameterRebinder(parameterExpressionMap).Visit(expression);
        }

        protected override Expression VisitParameter(ParameterExpression parameterExpression)
        {
            ParameterExpression replacementParameterExpression;

            if (_parameterExpressionMap.TryGetValue(parameterExpression, out replacementParameterExpression))
                parameterExpression = replacementParameterExpression;

            return base.VisitParameter(parameterExpression);
        }
    }
}