﻿using System;
using System.Collections.Generic;
using System.Text;

namespace StackUnderflow.Domain.Core.Contexts.Questions.SendQuestionOwnerAcknowledgementOp
{
    public class SendQuestionOwnerAcknowledgementCmd
    {
        public SendQuestionOwnerAcknowledgementCmd()
        {

        }

        public SendQuestionOwnerAcknowledgementCmd(int questionId, int questionOwnerId)
        {
            QuestionId = questionId;
            QuestionOwnerId = questionOwnerId;
        }

        public int QuestionId { get; }
        public int QuestionOwnerId { get; }
    }
}
