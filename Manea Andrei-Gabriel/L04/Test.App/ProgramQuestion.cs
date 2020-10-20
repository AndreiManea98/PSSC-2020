using Question.Domain.PostQuestionWorkflow;
using System;
using System.Collections.Generic;
using System.Net;
using static Question.Domain.PostQuestionWorkflow.PostQuestionResult;

namespace Test.App
{
    class ProgramQuestion
    {
        static void Main(string[] args)
        {
            List<string> tags = new List<string> {"React", "JavaScript"};
            var cmd = new PostQuestionCmd("What are Components?", "Can somebody explain me how Components work in React", tags);
            var result = PostQuestion(cmd);

            result.Match(
                    ProcessQuestionPosted,
                    ProcessQuestionNotPosted,
                    ProcessInvalidQuestion
                );

            Console.ReadLine();
        }

        private static IPostQuestionResult ProcessInvalidQuestion(QuestionValidationFailed validationErrors)
        {
            Console.WriteLine("Question validation failed: ");
            foreach (var error in validationErrors.ValidationErrors)
            {
                Console.WriteLine(error);
            }
            return validationErrors;
        }

        private static IPostQuestionResult ProcessQuestionNotPosted(QuestionNotPosted questionNotCreatedResult)
        {
            Console.WriteLine($"Question not created: {questionNotCreatedResult.Reason}");
            return questionNotCreatedResult;
        }

        private static IPostQuestionResult ProcessQuestionPosted(QuestionPosted question)
        {
            Console.WriteLine($"Question {question.QuestionId}");
            return question;
        }

        public static IPostQuestionResult PostQuestion(PostQuestionCmd postQuestionCommand)
        {
            if (string.IsNullOrWhiteSpace(postQuestionCommand.Title))
            {
                var errors = new List<string>() { "Invalid question title" };
                return new QuestionValidationFailed(errors);
            }

            if (string.IsNullOrWhiteSpace(postQuestionCommand.Body))
            {
                var errors = new List<string>() { "Invalid body for the question" };
                return new QuestionValidationFailed(errors);
            }
      

            if (new Random().Next(10) > 1)
            {
                return new QuestionNotPosted("Question could not be verified");
            }

            var questionId = Guid.NewGuid();
            var result = new QuestionPosted(questionId, postQuestionCommand.Title, postQuestionCommand.Body, postQuestionCommand.Tags);

            //execute logic
            return result;
        }
    }
}
