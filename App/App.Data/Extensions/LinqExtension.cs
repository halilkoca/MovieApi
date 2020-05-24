﻿using App.Entity.Enum;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace App.Data.Extensions
{
    public static class LinqExtension
    {
        public static IQueryable<T> OrderBy<T>(this IQueryable<T> source, string property, OrderDirection direction) where T : class
        {
            //STEP 1: Verify the property is valid
            var searchProperty = typeof(T).GetProperty(property);

            if (searchProperty == null)
                throw new ArgumentException("property");

            if (!searchProperty.PropertyType.IsValueType &&
                !searchProperty.PropertyType.IsPrimitive &&
                !searchProperty.PropertyType.Namespace.StartsWith("System") &&
                !searchProperty.PropertyType.IsEnum)
                throw new ArgumentException("property");

            if (searchProperty.GetMethod == null ||
                !searchProperty.GetMethod.IsPublic)
                throw new ArgumentException("property");

            //STEP 2: Create the OrderBy property selector
            var parameter = Expression.Parameter(typeof(T), "o");
            var selectorExpr = Expression.Lambda(
                Expression.Property(parameter, property),
                parameter);

            //STEP 3: Update the IQueryable expression to include OrderBy
            Expression queryExpr = source.Expression;
            queryExpr = Expression.Call(typeof(Queryable),
                                        direction.Equals(OrderDirection.Asc) ? "OrderBy" : "OrderByDescending",
                                        new Type[] { source.ElementType, searchProperty.PropertyType },
                                        queryExpr,
                                        selectorExpr);

            return source.Provider.CreateQuery<T>(queryExpr);
        }
    }
}
