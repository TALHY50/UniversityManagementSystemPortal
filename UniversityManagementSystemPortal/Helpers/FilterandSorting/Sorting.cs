using Newtonsoft.Json.Linq;
using System.Linq.Expressions;

namespace UniversityManagementSystemPortal.Helpers.FilterandSorting
{
    public static class Sorting<T>
    {

        public static IQueryable<T> Sort(string sortOrder, string columnName, IQueryable<T> data)
        {
            if (string.IsNullOrEmpty(sortOrder) || string.IsNullOrEmpty(columnName))
            {

                return data;
            }
            IQueryable<T> result;
            var parameter = Expression.Parameter(typeof(T), "x");
            var sortExpression = Expression.Lambda<Func<T, object>>(Expression.Convert(Expression.Property(parameter, columnName), typeof(object)), parameter);
            switch (sortOrder)
            {
                case "asc":
                    result = data.OrderBy(sortExpression);
                    break;
                case "desc":
                    result = data.OrderByDescending(sortExpression);
                    break;
                default:
                    result = data.OrderBy(sortExpression);
                    break;
            }

            return result;
        }
    }
}
