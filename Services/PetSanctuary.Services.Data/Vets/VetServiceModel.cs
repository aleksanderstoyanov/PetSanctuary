namespace PetSanctuary.Services.Data.Vets
{
    using AutoMapper;
    using PetSanctuary.Data.Models;
    using PetSanctuary.Services.Mapping;

    public class VetServiceModel : IMapFrom<Vet>, IHaveCustomMappings
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public string Description { get; set; }

        public int ClinicId { get; set; }

        public string Clinic { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Vet, VetServiceModel>()
                .ForMember(vet => vet.Clinic, opt => opt.MapFrom(vet => vet.Clinic.Name))
                .ForMember(vet => vet.Likes, opt => opt.MapFrom(vet => vet.Likes.Count))
                .ForMember(vet => vet.Dislikes, opt => opt.MapFrom(vet => vet.Dislikes.Count));
        }
    }
}
