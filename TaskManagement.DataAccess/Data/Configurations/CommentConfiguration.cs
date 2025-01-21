using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Entities;

namespace TaskManagement.DataAccess.Data.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comments>
    {
        public void Configure(EntityTypeBuilder<Comments> builder)
        {
            builder.Property(c => c.Content)
             .IsRequired()
               .HasMaxLength(100);
        }
    }
}
