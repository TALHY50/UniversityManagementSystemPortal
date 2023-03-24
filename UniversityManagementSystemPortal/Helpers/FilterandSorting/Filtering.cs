using System.Linq.Expressions;

namespace UniversityManagementSystemPortal.FilterandSorting
{
    public static class Filtering
    {
        public static IQueryable<T> Filter<T, TRelated>(string relatedColumnName, string value, IQueryable<T> data, Expression<Func<T, TRelated>> relatedProperty) where T : class where TRelated : class
        {
            if (string.IsNullOrEmpty(relatedColumnName) || string.IsNullOrEmpty(value))
            {
                return data;
            }

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, relatedProperty.Name);
            var join = typeof(T).GetMethod("Join", new[] { typeof(IQueryable<TRelated>), typeof(Expression<Func<T, TRelated, bool>>), typeof(string), typeof(IQueryable<TRelated>) });
            var subQuery = join.Invoke(null, new object[] { data, relatedProperty, null, null });

            var parameter2 = Expression.Parameter(typeof(TRelated), "y");
            var property2 = Expression.Property(parameter2, relatedColumnName);
            var method = typeof(string).GetMethod("ToLower", System.Type.EmptyTypes);
            var toLower = Expression.Call(property2, method);
            var containsMethod = typeof(string).GetMethod("Contains", new[] { typeof(string) });
            var containsExpression = Expression.Call(toLower, containsMethod, Expression.Constant(value.ToLower()));
            var lambda = Expression.Lambda<Func<TRelated, bool>>(containsExpression, parameter2);

            var whereMethod = typeof(Queryable).GetMethods().Single(m => m.Name == "Where" && m.GetParameters().Length == 2).MakeGenericMethod(typeof(TRelated));
            var filteredSubQuery = whereMethod.Invoke(null, new object[] { subQuery, lambda });

            var selectMethod = typeof(Queryable).GetMethods().Single(m => m.Name == "Select" && m.GetParameters().Length == 2).MakeGenericMethod(typeof(T), typeof(TRelated));
            var selectExpression = Expression.Lambda<Func<T, TRelated>>(property, parameter);
            var selectedQuery = selectMethod.Invoke(null, new object[] { data, selectExpression });

            var joinMethod = typeof(Queryable).GetMethods().Single(m => m.Name == "Join" && m.GetParameters().Length == 5).MakeGenericMethod(typeof(T), typeof(TRelated), typeof(Guid), typeof(T));
            var joinExpression = Expression.Lambda<Func<T, TRelated, Guid>>(Expression.Property(Expression.Property(parameter, "Institute"), "Id"), parameter, parameter2);
            var result = (IQueryable<T>)joinMethod.Invoke(null, new object[] { selectedQuery, filteredSubQuery, joinExpression, "Id", data });

            return result;
        }
    }

}
