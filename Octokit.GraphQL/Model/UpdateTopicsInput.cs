namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of UpdateTopics
    /// </summary>
    public class UpdateTopicsInput
    {
        public ID RepositoryId { get; set; }

        public IEnumerable<string> TopicNames { get; set; }

        public string ClientMutationId { get; set; }
    }
}