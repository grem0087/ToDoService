using System;
using System.Collections;
using System.Collections.Generic;

namespace ToDo.Service.Controllers.Dtos
{
    public class SearchResultDto
    {
        public IEnumerable<ToDoDto> Items { get; set; }
    }
}
