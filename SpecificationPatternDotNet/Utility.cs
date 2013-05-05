using System;
using System.Linq;
using System.Linq.Expressions;

namespace SpecificationPatternDotNet
{
    /// <remarks>
    ///     Derived from <a href="http://blogs.msdn.com/b/meek/archive/2008/05/02/linq-to-entities-combining-predicates.aspx">MSDN</a>.
    /// </remarks>
    internal static class Utility
    {
        public static Expression<Func<TEntity, bool>> AndAlso<TEntity>(
            this Expression<Func<TEntity, bool>> firstExpression,
            Expression<Func<TEntity, bool>> secondExpression)
        {
            return firstExpression.Compose(secondExpression, Expression.AndAlso);
        }

        public static Expression<Func<TEntity, bool>> OrElse<TEntity>(
            this Expression<Func<TEntity, bool>> firstExpression,
            Expression<Func<TEntity, bool>> secondExpression)
        {
            return firstExpression.Compose(secondExpression, Expression.OrElse);
        }

        public static Expression<TEntity> Compose<TEntity>(this Expression<TEntity> firstExpression,
                                                           Expression<TEntity> secondExpression,
                                                           Func<Expression, Expression, Expression> mergeFunc)
        {
            var firstParameters = firstExpression.Parameters;

            // Build parameter map (from parameters of second to parameters of first)
            var parameterExpressionMap = firstParameters.Select((f, i) => new {f, s = secondExpression.Parameters[i]})
                                                        .ToDictionary(p => p.s, p => p.f);

            // Replace parameters in the second lambda expression with parameters from the first
            var secondBody = ParameterRebinder.ReplaceParameters(parameterExpressionMap, secondExpression.Body);

            // Apply composition of lambda expression bodies to parameters from the first expression 
            return Expression.Lambda<TEntity>(mergeFunc(firstExpression.Body, secondBody), firstParameters);
        }

        public static Expression<Func<TEntity, bool>> Not<TEntity>(this Expression<Func<TEntity, bool>> expression)
        {
            var unaryExpression = Expression.Not(expression);

            return Expression.Lambda<Func<TEntity, bool>>(unaryExpression, expression.Parameters);
        }
    }
}