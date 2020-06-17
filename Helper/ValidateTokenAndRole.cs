using JwtToolBox;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace API.Helper
{
    public static class ValidateTokenAndRole
    {
        //Chekc Role in the JWT Token
        public static string ValidateAndGetRole(Microsoft.AspNetCore.Http.HttpRequest R)
        {
            JWTService JWT = new JWTService("FZeDfgPkyXaDFyMwQfSbIoJhF", "localhost:4200", "localhost:4200");
            string Role = "";
            foreach (KeyValuePair<string, Microsoft.Extensions.Primitives.StringValues> item in R.Headers)
            {
                if (item.Key == "Authorization")
                {
                    Role = JWT.ValidateToken(item.Value.ToString());
                }
            }
            return Role;
        }
    }
}
