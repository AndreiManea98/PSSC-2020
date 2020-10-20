using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Question.Domain.PostQuestionWorkflow
{
    // product type = Title*Body*Tags
    public struct PostQuestionCmd
    {
        [MaxLength(150)]
        [Required]
        public string Title { get; private set; } // question title
        [Required]
        public string Body { get; set; } // question body ( question description )
        [Required]
        public List<string> Tags { get; set; } // question tags . Am folosit lista pentru ca putem adauga mai multe tags.
        
        public PostQuestionCmd(string title, string body, List<string> tags)
        {
            Title = title;
            Body = body;
            Tags = tags;
        }
    }
}
