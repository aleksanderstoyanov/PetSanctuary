﻿using System;
using System.Collections.Generic;
using System.Text;

namespace PetSanctuary.Services.Data.Counts
{
    public interface ICountService
    {
        public int GetUserPostsCount(string id, bool isAdmin);

        public int GetUserBlogsCount(string id, bool isAdmin);
    }
}