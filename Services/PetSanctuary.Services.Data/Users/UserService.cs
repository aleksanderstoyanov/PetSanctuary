using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PetSanctuary.Services.Data.Users
{
    public class UserService : IUserService
    {
        private readonly IDeletableEntityRepository<ApplicationUser> userRepository;

        public UserService(IDeletableEntityRepository<ApplicationUser> userRepository)
        {
            this.userRepository = userRepository;
        }

        public ApplicationUser GetUserById(string id)
        {
            return this.userRepository.All().Where(x => x.Id == id).FirstOrDefault();
        }

        public ApplicationUser GetUserByName(string name)
        {
            return this.userRepository.All().Where(x => x.UserName == name).FirstOrDefault();
        }
    }
}
