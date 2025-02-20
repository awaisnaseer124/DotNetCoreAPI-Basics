using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Dtos.Comments
{
    public class CreateCommentsDto
    {
        public string Title { set; get; } = string.Empty;
        public string Content { get; set; } = string.Empty;
    }
}