using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Web.ViewModels.Vets
{
    public class VetsByIdViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string Surname { get; set; }

        public int Likes { get; set; }

        public int Dislikes { get; set; }
    }
}
