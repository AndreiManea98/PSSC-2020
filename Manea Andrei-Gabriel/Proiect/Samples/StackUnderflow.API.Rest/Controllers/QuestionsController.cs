﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Access.Primitives.Extensions.ObjectExtensions;
using Access.Primitives.IO;
using Microsoft.AspNetCore.Mvc;
using StackUnderflow.Domain.Core;
using StackUnderflow.Domain.Core.Contexts;
using StackUnderflow.EF.Models;
using Access.Primitives.EFCore;
using StackUnderflow.Domain.Schema.Backoffice;
using LanguageExt;
using StackUnderflow.Domain.Core.Contexts.Questions;
using StackUnderflow.EF;
using Microsoft.EntityFrameworkCore;
using StackUnderflow.Domain.Core.Contexts.Questions.PostQuestionOp;
using StackUnderflow.Domain.Core.Contexts.Questions.CreateReplyOp;

namespace StackUnderflow.API.Rest.Controllers
{
    [ApiController]
    [Route("questions")]
    public class QuestionsController : ControllerBase
    {
        private readonly IInterpreterAsync _interpreter;
        private readonly DatabaseContext _dbContext;

        public QuestionsController(IInterpreterAsync interpreter, DatabaseContext dbContext)
        {
            _interpreter = interpreter;
            _dbContext = dbContext;
        }

        [HttpPost("postQuestion")]
        public async Task<IActionResult> PostQuestion([FromBody] PostQuestionCmd cmd)
        {
            var dep = new QuestionsDependencies();
            var questions = await _dbContext.Questions.ToListAsync();
            var ctx = new QuestionsWriteContext(questions);

            var expr = from postQuestionsResult in QuestionsContext.PostQuestion(cmd)
                       select postQuestionsResult;

            var r = await _interpreter.Interpret(expr, ctx, dep);

            _dbContext.Questions.Add(new DatabaseModel.Models.Post { PostId = cmd.QuestionId, Title = cmd.Title, PostText = cmd.Body, PostedBy = new Guid("f505c32f-3573-4459-8112-af8276d3e919")});
            //var reply = await _dbContext.Replies.Where(r => r.ReplyId == 4).SingleOrDefaultAsync();
            //reply.Body = "Text updated";
            //_dbContext.Replies.Update(reply);
            await _dbContext.SaveChangesAsync();


            return r.Match(
                 succ => (IActionResult)Ok(succ.QuestionId),
                 fail => BadRequest("Question could not be added"),
                 invalid => BadRequest("Invalid Question")
                 );
        }





        [HttpPost("createReply")]
        public async Task<IActionResult> CreateReply([FromBody] CreateReplyCmd cmd)
        {
            var dep = new QuestionsDependencies();
            var replies = await _dbContext.Replies.ToListAsync();
            var ctx = new QuestionsWriteContext(replies);

            var expr = from createReplyResult in QuestionsContext.CreateReply(cmd)
                       select createReplyResult;

            var r = await _interpreter.Interpret(expr, ctx, dep);

            _dbContext.Replies.Add(new DatabaseModel.Models.Reply { Body = cmd.Body, AuthorUserId = new Guid("f505c32f-3573-4459-8112-af8276d3e919"), QuestionId = cmd.QuestionId, ReplyId = 4 });
            //var reply = await _dbContext.Replies.Where(r => r.ReplyId == 4).SingleOrDefaultAsync();
            //reply.Body = "Text updated";
            //_dbContext.Replies.Update(reply);
            await _dbContext.SaveChangesAsync();


            return r.Match(
                succ => (IActionResult)Ok(succ.Body),
                fail => BadRequest("Reply could not be added")
                );
        }
    }
}
