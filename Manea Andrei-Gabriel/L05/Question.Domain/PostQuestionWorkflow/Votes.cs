using CSharp.Choices;
using LanguageExt.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Question.Domain.PostQuestionWorkflow
{
    [AsChoice]
    public static partial class Votes
    {
        public interface IVotes { }
        public class UnverifiedVotesNumber : IVotes
        {
            public int Votes { get; private set; }
            private UnverifiedVotesNumber(int votes)
            {
                Votes = votes;
            }
            public static Result<UnverifiedVotesNumber> Create(int votes)
            {
                if (IsValidVotesNumber(votes))
                {
                    return new UnverifiedVotesNumber(votes);
                
                }
                else
                    return new Result<UnverifiedVotesNumber>(new InvalidVotesException(votes));
            }
            private static bool IsValidVotesNumber(int votes)
            {
                if (votes != 0)
                {
                    return true;
                }
                return false;
            }
        }
        public class VerifiedVotesNumber : IVotes
        {
            public int Votes { get; private set; }
            internal VerifiedVotesNumber(int votes)
            {
                Votes = votes;
            }
        }
    }
}
