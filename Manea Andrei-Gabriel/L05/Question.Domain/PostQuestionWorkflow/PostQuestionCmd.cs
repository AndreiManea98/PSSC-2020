using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Question.Domain.PostQuestionWorkflow
{
    // product type = Title*Body*Tags
    public struct PostQuestionCmd
    {
        [Required]
        public string Title { get; private set; } // question title
        [Required]
        [MaxLength(1000)]
        public string Body { get; private set; } // question body ( question description )
        [Required]
        public List<string> Tags { get; set; } // question tags . Am folosit lista pentru ca putem adauga mai multe tags.
        [Required]
        public int Votes { get; private set; }

        public PostQuestionCmd(string title, string body, List<string> tags,int votes)
        {
            Title = title;
            Body = body;
            Tags = tags;
            Votes = votes;
        }
    }
}
