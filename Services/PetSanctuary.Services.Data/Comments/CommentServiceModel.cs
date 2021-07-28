using AutoMapper;
using PetSanctuary.Data.Models;
using PetSanctuary.Services.Mapping;
using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Services.Data.Comments
{
    public class CommentServiceModel : IMapFrom<Comment>, IHaveCustomMappings
    {
        public int Id { get; set; }

        public string Content { get; set; }

        public string PublishedOn { get; set; }

        public string Publisher { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Comment, CommentServiceModel>()
                .ForMember(comment => comment.Publisher, opt => opt.MapFrom(comment => comment.Publisher.UserName));
        }
    }
}
