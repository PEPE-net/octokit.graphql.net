﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json.Linq;

namespace Octokit.GraphQL.Core.Builders
{
    public static class Rewritten
    {
        public static class Value
        {
            public static readonly MethodInfo OfTypeMethod = typeof(Value).GetTypeInfo().GetDeclaredMethod(nameof(OfType));
            public static readonly MethodInfo SelectMethod = typeof(Value).GetTypeInfo().GetDeclaredMethod(nameof(Select));
            public static readonly MethodInfo SelectListMethod = typeof(Value).GetTypeInfo().GetDeclaredMethod(nameof(SelectList));
            public static readonly MethodInfo SingleMethod = typeof(Value).GetTypeInfo().GetDeclaredMethod(nameof(Single));
            public static readonly MethodInfo SingleOrDefaultMethod = typeof(Value).GetTypeInfo().GetDeclaredMethod(nameof(SingleOrDefault));

            public static JToken OfType(JToken source, string typeName)
            {
                return (string)source["__typename"] == typeName ? source : null;
            }

            public static TResult Select<TResult>(JToken source, Func<JToken, TResult> selector)
            {
                return source.Type != JTokenType.Null ? selector(source) : default(TResult);
            }

            public static IEnumerable<TResult> SelectList<TResult>(JToken source, Func<JToken, IEnumerable<TResult>> selector)
            {
                return selector(source);
            }

            public static TResult Single<TResult>(TResult value)
            {
                if (value == null)
                {
                    throw new InvalidOperationException("The value passed to Single was null.");
                }

                return value;
            }

            public static TResult SingleOrDefault<TResult>(TResult value) => value;
        }

        public static class List
        {
            public static readonly MethodInfo OfTypeMethod = typeof(List).GetTypeInfo().GetDeclaredMethod(nameof(OfType));
            public static readonly MethodInfo SelectMethod = typeof(List).GetTypeInfo().GetDeclaredMethod(nameof(Select));
            public static readonly MethodInfo ToDictionaryMethod = typeof(List).GetTypeInfo().GetDeclaredMethod(nameof(ToDictionary));
            public static readonly MethodInfo ToListMethod = typeof(List).GetTypeInfo().GetDeclaredMethod(nameof(ToList));
            public static readonly MethodInfo ToSubqueryListMethod = typeof(List).GetTypeInfo().GetDeclaredMethod(nameof(ToSubqueryList));

            public static IEnumerable<JToken> OfType(IEnumerable<JToken> source, string typeName)
            {
                return source.Where(x => (string)x["__typename"] == typeName);
            }

            public static IEnumerable<TResult> Select<TResult>(
                IEnumerable<JToken> source,
                Func<JToken, TResult> selector)
            {
                return source.Select(selector);
            }

            public static Dictionary<TKey, TElement> ToDictionary<TKey, TElement>(
                IEnumerable<JToken> source,
                Func<JToken, TKey> keySelector,
                Func<JToken, TElement> elementSelector)
            {
                return source.ToDictionary(keySelector, elementSelector);
            }

            public static List<TResult> ToList<TResult>(IEnumerable<JToken> source)
            {
                return source.Select(x => x.ToObject<TResult>()).ToList();
            }

            public static List<T> ToSubqueryList<T>(
                IEnumerable<T> source,
                ISubqueryRunner context,
                ISubquery subquery)
            {
                var result = source.ToList();
                context.SetQueryResultSink(subquery, result);
                return result;
            }
        }

        public static class Interface
        {
            public static readonly MethodInfo CastMethod = typeof(Interface).GetTypeInfo().GetDeclaredMethod(nameof(Cast));

            public static JToken Cast(JToken source, string typeName)
            {
                var received = (string)source["__typename"];

                if (received == typeName)
                {
                    return source;
                }

                throw new InvalidOperationException($"Cast failed: expected '{typeName}', received '{received}'.");
            }
        }
    }
}
