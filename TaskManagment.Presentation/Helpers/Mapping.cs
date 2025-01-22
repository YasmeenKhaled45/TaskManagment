using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagement.BuisnessLogic.Contracts.Comments.Commands;
using TaskManagement.BuisnessLogic.Contracts.Tasks.Commands;
using TaskManagement.DataAccess.Dtos.Auth;
using TaskManagement.DataAccess.Dtos.Comments;
using TaskManagement.DataAccess.Dtos.Tasks;
using TaskManagement.DataAccess.Dtos.Teams;
using TaskManagement.DataAccess.Entities;

namespace TaskManagement.DataAccess.Constants
{
    public class Mapping : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<RegisterRequestdto, User>()
                .Map(dest => dest.UserName, src => src.Email);
            config.NewConfig<Tasks, TaskDto>();
            config.NewConfig<CreateTaskCommand,Tasks>();
            config.NewConfig<CreateCommentCommand, Comments>();
            config.NewConfig<Comments, CommentResponse>();
            config.NewConfig<CreateTeamDto, Team>();
            config.NewConfig<Team, TeamDto>();
        }
    }
}
