using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.Core.Builders;

namespace Octokit.GraphQL.Core.UnitTests.Models
{
    public class Repository : QueryableValue<Repository>
    {
        public Repository(Expression expression) : base(expression)
        {
        }

        public DateTimeOffset CreatedAt { get; }
        public int? DatabaseId { get; }
        public string Description { get; }
        public int ForkCount { get; }
        public bool HasIssuesEnabled { get; }
        public ID Id { get; }
        public string Name { get; }
        public Issue Issue(Arg<int> number) => this.CreateMethodCall(x => x.Issue(number), Models.Issue.Create);
        public IssueConnection Issues(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<IEnumerable<string>>? labels = null) => this.CreateMethodCall(x => x.Issues(first, after, last, before, labels), IssueConnection.Create);
        public Label Label(Arg<string> name) => this.CreateMethodCall(x => x.Label(name), Models.Label.Create);
        public LabelConnection Labels(Arg<int>? first = null, Arg<string>? after = null, Arg<int>? last = null, Arg<string>? before = null, Arg<string>? query = null) => this.CreateMethodCall(x => x.Labels(first, after, last, before, query), LabelConnection.Create);
        internal static Repository Create(Expression expression)
        {
            return new Repository(expression);
        }
    }
}