﻿using Microsoft.AspNetCore.Identity;

namespace EasyFinance.DataAccess.Identity
{
    public class User: IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }
    }
}
