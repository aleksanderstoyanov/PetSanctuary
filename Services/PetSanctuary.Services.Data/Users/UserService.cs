using Microsoft.AspNetCore.Identity;
using PetSanctuary.Data.Common.Repositories;
using PetSanctuary.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            return this
                 .userRepository
                 .All()
                 .FirstOrDefault(user => user.Id == id);
        }

        public ApplicationUser GetUserByName(string name)
        {
            return this
                .userRepository
                .All()
                .FirstOrDefault(user => user.UserName == name);
        }

        public string GetUserPhoneNumber(string id)
        {
            return this
                .userRepository
                .All()
                .FirstOrDefault(user => user.Id == id)
                .PhoneNumber;
        }
    }
}
