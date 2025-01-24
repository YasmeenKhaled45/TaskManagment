using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TaskManagement.DataAccess.Constants;
using TaskManagement.DataAccess.Data;
using TaskManagement.DataAccess.Entities;
using TaskManagement.DataAccess.Enums;
using TaskManagement.DataAccess.Interfaces;

namespace TaskManagement.BuisnessLogic.Services
{
    public class NotficationService(UserManager<User> userManager , IEmailSender emailSender , AppDbContext context) : INotficationService
    {
        private readonly UserManager<User> userManager = userManager;
        private readonly IEmailSender emailSender = emailSender;
        private readonly AppDbContext context = context;

        public async Task HandleTaskDeadline()
        {
            var overdueTasks = await context.Tasks
           .Where(task => task.DueDate < DateOnly.FromDateTime(DateTime.UtcNow) && task.Status != Status.Completed)
            .ToListAsync();

            foreach (var task in overdueTasks)
            {
                task.Status = Status.Late;
            }
            await context.SaveChangesAsync();
        }

        public async Task SendNewTaskNotfications(Tasks task)
        {

            var users = await userManager.Users.ToListAsync();
                foreach (var user in users)
                {
                    var placeholders = new Dictionary<string, string>()
                {
                    { "{{name}}", user.FirstName},
                    { "{{taskTitle}}", task.Title},
                    {"{{dueDate}}" , task.DueDate.ToString()},
                    {"{{taskDescription}}" , task.Description}

                };
                    var body = EmailBodyBuilder.GenerateEmailBody("NewTaskTemplete", placeholders);
                    await emailSender.SendEmailAsync(user.Email!, $"TaskManagment : New Task - {task.Title}", body);
                }   
        }
 

        public async Task SendTaskReminderDate()
        {
            var tasks = await context.Tasks.Where(t => t.DueDate == DateOnly.
            FromDateTime(DateTime.UtcNow.AddDays(1))).ToListAsync();

            var users = await userManager.Users.ToListAsync();
            foreach (var task in tasks)
            {
                foreach (var user in users)
                {
                    var placeholders = new Dictionary<string, string>()
                {
                    { "{{name}}", user.FirstName},
                    { "{{taskTitle}}", task.Title},
                    {"{{dueDate}}" , task.DueDate.ToString()}

                };
                    var body = EmailBodyBuilder.GenerateEmailBody("TaskReminder", placeholders);
                    await emailSender.SendEmailAsync(user.Email!, $"TaskManagment : Deadline Task - {task.Title}", body);
                }
            }
        }
    }
}
