﻿using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace App.Core.Utilities.Security
{
    public class SecurityKeyHelper
    {
        public static SecurityKey CreateSecurityKey(string securityKey)
        {
            return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey));
        }
    }
}
