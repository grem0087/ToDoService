using AutoMapper;
using ToDo.Database.Entities;
using ToDo.Service.Controllers.Dtos;

namespace ToDo.Service.Controllers.Mappers
{
    public class ToDoMapperProfile : Profile
    {
        public ToDoMapperProfile()
        {
            CreateMap<ToDoEntity, ToDoDto>();
            CreateMap<ToDoDto, ToDoEntity>();
            CreateMap<CreateToDoDto, ToDoEntity>()
                .ForMember(ent=>ent.Id, opt => opt.Ignore());
        }
    }
}
