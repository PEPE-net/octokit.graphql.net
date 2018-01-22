using System.Linq;
using Octokit.GraphQL.Core;
using Octokit.GraphQL.IntegrationTests.Utilities;
using Octokit.GraphQL.Model;
using Xunit;

namespace Octokit.GraphQL.IntegrationTests.Queries
{
    public class RepositoryTests : IntegrationTestBase
    {
        [IntegrationTest]
        public void Should_Query_All_RepositoryOwner_Repositories()
        {
            var query = new Query().RepositoryOwner("octokit").Repositories(first: 30).Nodes.Select(repository => repository.Name);

            var repositoryNames = Connection.Run(query).Result.ToArray();

            Assert.Contains("discussions", repositoryNames);
            Assert.Contains("octokit.net", repositoryNames);
            Assert.Contains("octokit.rb", repositoryNames);
            Assert.Contains("octokit.objc", repositoryNames);
            Assert.Contains("go-octokit", repositoryNames);
        }

        [IntegrationTest]
        public void Should_Query_Repository_ByName()
        {
            var query = new Query().Repository("octokit", "octokit.net").Select(r => new
            {
                r.Name,
                r.DatabaseId,
            });

            var repository = Connection.Run(query).Result;

            Assert.NotNull(repository);
            Assert.Equal(repository.Name, "octokit.net");
            Assert.Equal(repository.DatabaseId, 7528679);
        }

        [IntegrationTest]
        public void Should_QueryRepositoryOwner_Repositories_OrderBy_Name_Ascending()
        {
            var query = new Query().RepositoryOwner("octokit").Repositories(first: 30, orderBy: new RepositoryOrder
            {
                Direction = OrderDirection.Asc,
                Field = RepositoryOrderField.Name
            }).Nodes.Select(repository => repository.Name);

            var repositoryNames = Connection.Run(query).Result.ToArray();

            Assert.Contains("discussions", repositoryNames);
            Assert.Contains("go-octokit", repositoryNames);
            Assert.Contains("octokit.net", repositoryNames);
            Assert.Contains("octokit.objc", repositoryNames);
            Assert.Contains("octokit.rb", repositoryNames);
        }

        [IntegrationTest]
        public void Should_QueryRepositoryOwner_Repositories_OrderBy_CreatedAt_Descending()
        {
            var query = new Query().RepositoryOwner("octokit").Repositories(first: 30, orderBy: new RepositoryOrder
            {
                Direction = OrderDirection.Asc,
                Field = RepositoryOrderField.CreatedAt
            }).Nodes.Select(repository => repository.Name);

            var repositoryNames = Connection.Run(query).Result.ToArray();

            Assert.Contains("octokit.rb", repositoryNames);
            Assert.Contains("octokit.net", repositoryNames);
            Assert.Contains("octokit.objc", repositoryNames);
            Assert.Contains("go-octokit", repositoryNames);
            Assert.Contains("discussions", repositoryNames);
        }

        [IntegrationTest]
        public void Should_Query_Union_Issue_Or_PullRequest()
        {
            var query = new Query().Repository("octokit", "octokit.net")
                .IssueOrPullRequest(1)
                .Select(issueOrPullRequest => new
                {
                    IssueId = issueOrPullRequest.Issue.Id,
                    PullRequestId = issueOrPullRequest.PullRequest.Id
                });

            var result = Connection.Run(query).Result;
        }
    }
}