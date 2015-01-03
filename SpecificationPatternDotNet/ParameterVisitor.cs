using System.Linq.Expressions;

namespace SpecificationPatternDotNet
{
    internal sealed class ParameterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _parameterExpression;

        public ParameterVisitor(ParameterExpression parameterExpression)
        {
            _parameterExpression = parameterExpression;
        }

        protected override Expression VisitParameter(ParameterExpression parameterExpression)
        {
            return _parameterExpression;
        }
    }
}