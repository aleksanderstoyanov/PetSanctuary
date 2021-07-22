﻿namespace PetSanctuary.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class UserPet
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public string PetId { get; set; }

        public Pet Pet { get; set; }
    }
}
