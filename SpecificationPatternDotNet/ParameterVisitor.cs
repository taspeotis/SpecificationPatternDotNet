using System.Linq.Expressions;

namespace SpecificationPatternDotNet
{
    internal sealed class ParameterVisitor : ExpressionVisitor
    {
        private readonly ParameterExpression _sourceParameter;
        private readonly ParameterExpression _destinationParameter;

        public ParameterVisitor(ParameterExpression sourceParameter, ParameterExpression destinationParameter)
        {
            _sourceParameter = sourceParameter;
            _destinationParameter = destinationParameter;
        }

        protected override Expression VisitParameter(ParameterExpression parameter)
        {
            if (ReferenceEquals(parameter, _sourceParameter))
                return _destinationParameter;

            return base.VisitParameter(parameter);
        }
    }
}