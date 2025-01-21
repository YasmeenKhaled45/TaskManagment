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
    public class TaskConfiguration : IEntityTypeConfiguration<Tasks>
    {
        public void Configure(EntityTypeBuilder<Tasks> builder)
        {
            builder.Property(t => t.Title)
           .IsRequired()
           .HasMaxLength(100); 

            builder.Property(t => t.Description)
                .IsRequired()
                .HasMaxLength(500);  

            builder.Property(t => t.DueDate)
                .IsRequired();

            builder.Property(t => t.TaskPriority)
                .HasConversion<int>()
                .IsRequired();

            builder.Property(t => t.Status)
                .HasConversion<int>()
                .IsRequired();
        }
    }
}
