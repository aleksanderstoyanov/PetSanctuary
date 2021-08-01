namespace PetSanctuary.Services.Data.Blogs
{
    using AutoMapper;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Mapping;

    public class BlogServiceModel : IMapFrom<Blog>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string Title { get; set; }

        public string Image { get; set; }

        public string Description { get; set; }

        public string Author { get; set; }

        public string CreatedOn { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Blog, BlogServiceModel>()
                .ForMember(blog => blog.Author, opt => opt.MapFrom(blog => blog.Author.UserName))
                .ForMember(blog => blog.CreatedOn, opt => opt.MapFrom(blog => blog.CreatedOn.ToString("dddd, dd MMMM yyyy")));
        }
    }
}
