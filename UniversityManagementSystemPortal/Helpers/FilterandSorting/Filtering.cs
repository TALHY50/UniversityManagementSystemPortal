using System.Linq.Expressions;

namespace UniversityManagementSystemPortal.Helpers.FilterandSorting
{
    public static class Filtering
    {
        public static IQueryable<T> Filter<T>(IQueryable<T> data, string searchTerm, string[] propertyNames)
        {
            if (string.IsNullOrEmpty(searchTerm) || propertyNames == null || propertyNames.Length == 0)
                return data;

            var parameter = Expression.Parameter(typeof(T), "x");
            Expression orExpressions = null;
            foreach (var propertyName in propertyNames)
            {
                var propertyExpression = Expression.PropertyOrField(parameter, propertyName);
                var method = typeof(string).GetMethod("Contains", new[] { typeof(string) });
                var searchTermExpression = Expression.Constant(searchTerm.ToLower());
                var toLowerExpression = Expression.Call(propertyExpression, typeof(string).GetMethod("ToLower", Type.EmptyTypes));
                var containsExpression = Expression.Call(toLowerExpression, method, searchTermExpression);
                var lambda = Expression.Lambda<Func<T, bool>>(containsExpression, parameter);
                if (orExpressions == null)
                {
                    orExpressions = lambda.Body;
                }
                else
                {
                    orExpressions = Expression.OrElse(orExpressions, lambda.Body);
                }
            }
            var predicate = Expression.Lambda<Func<T, bool>>(orExpressions, parameter);
            return data.Where(predicate);
        }

    }

}
