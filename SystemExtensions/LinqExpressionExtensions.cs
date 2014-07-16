using System;
using System.Linq.Expressions;

namespace SystemExtensions
{
    public static class LinqExpressionExtensions
    {
        public static Expression<Func<T1, T3>> Combine<T1, T2, T3>(
            this Expression<Func<T1, T2>> expression,
            Expression<Func<T2, T3>> other)
        {
            var newBody = new Visitor(expression.Body).Visit(other.Body);
            var result = Expression.Lambda<Func<T1, T3>>(newBody, expression.Parameters);
            return result;
        }

        class Visitor : ExpressionVisitor
        {
            private readonly Expression replacement;

            public Visitor(Expression replacement)
            {
                this.replacement = replacement;
            }

            protected override Expression VisitParameter(ParameterExpression node)
            {
                return this.replacement;
            }
        }
    }
}