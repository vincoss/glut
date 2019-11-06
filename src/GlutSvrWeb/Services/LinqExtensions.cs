using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;


namespace GlutSvrWeb.Services
{
    public static class LinqExtensions
    {
        public static IQueryable<TEntity> OrderBy<TEntity>(this IQueryable<TEntity> source, string orderByProperty, bool desc) where TEntity: class
        {
            if(source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }
            if(string.IsNullOrWhiteSpace(orderByProperty))
            {
                throw new ArgumentNullException(nameof(orderByProperty));
            }

            string command = desc ? "OrderByDescending" : "OrderBy";
            var type = typeof(TEntity);
            var property = type.GetProperty(orderByProperty);
            var parameter = Expression.Parameter(type, "p");
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);
            var resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { type, property.PropertyType }, source.Expression, Expression.Quote(orderByExpression));
            return source.Provider.CreateQuery<TEntity>(resultExpression);
        }

        public static string GetPropertyName<T>(System.Linq.Expressions.Expression<Func<T, object>> property)
        {
            if(property == null)
            {
                throw new ArgumentNullException(nameof(property));
            }

            LambdaExpression lambda = (LambdaExpression)property;
            MemberExpression memberExpression;

            if (lambda.Body is UnaryExpression)
            {
                UnaryExpression unaryExpression = (UnaryExpression)(lambda.Body);
                memberExpression = (MemberExpression)(unaryExpression.Operand);
            }
            else
            {
                memberExpression = (MemberExpression)(lambda.Body);
            }

            return ((PropertyInfo)memberExpression.Member).Name;
        }

        public static string GetPropertyNameIgnoreCase(Type type, string propertyName)
        {
            if(type == null)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if(string.IsNullOrWhiteSpace(propertyName))
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            var info = type.GetProperties().Single(pi => pi.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

            if (info == null)
            {
                return null;
            }
            return info.Name;
        }
    }
}
