using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;
using MediatR;

namespace Application.Posts.Commands
{
    public class UpdatePost : IRequest<Post>
    {
        public int PostId { get; set; }

        public string? PostContent { get; set; }
    }
}