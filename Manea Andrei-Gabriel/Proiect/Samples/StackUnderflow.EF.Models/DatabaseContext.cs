using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StackUnderflow.DatabaseModel.Models;
using StackUnderflow.EF.Models;

namespace StackUnderflow.EF
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext()
        {

        }
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        public DbSet<Reply> Replies { get; set; }
        public DbSet<Post> Questions { get; set; }
    }
}
