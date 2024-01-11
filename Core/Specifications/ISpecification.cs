using System.Linq.Expressions;

namespace Core.Specyfications
{
    public interface ISpecification<T>
    {
        Expression<Func<T, bool>> Criteria { get; }
        List<Expression<Func<T, object>>> QueryExpressions { get; }
    }
}
