namespace Octokit.GraphQL.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Autogenerated input type of RemoveReaction
    /// </summary>
    public class RemoveReactionInput
    {
        public ID SubjectId { get; set; }

        public ReactionContent Content { get; set; }

        public string ClientMutationId { get; set; }
    }
}