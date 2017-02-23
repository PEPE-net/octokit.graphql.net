﻿using System;
using System.Linq.Expressions;
using LinqToGraphQL.Syntax;
using Newtonsoft.Json.Linq;

namespace LinqToGraphQL
{
    public class Query<TResult>
    {
        public Query(OperationDefinition operationDefinition, Expression<Func<JObject, TResult>> expression)
        {
            OperationDefinition = operationDefinition;
            Expression = expression;
            CompiledExpression = expression.Compile();
        }

        public OperationDefinition OperationDefinition { get; }

        public Expression<Func<JObject, TResult>> Expression { get; }

        public Func<JObject, TResult> CompiledExpression { get; }
    }
}
