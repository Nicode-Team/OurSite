﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using OurSite.DataLayer.Entities.Accounts;

namespace OurSite.Core.Utilities
{
    public static class AuthenticationHelper
    {
        public static string GenrateUserToken(User user,int Expire)
        {
            var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("sajjadhaniehfaezeherfanmobinsinamehdi"));
            var signInCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
            var tokenOption = new JwtSecurityToken(
                issuer: PathTools.Domain,
                claims: new List<Claim>()
                {
                                new Claim(ClaimTypes.Name, String.Concat(user.FirstName,user.LastName)),
                                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                                new Claim(ClaimTypes.Role,"Customer")

                },
                expires: DateTime.Now.AddDays(Expire),
                signingCredentials: signInCredentials
            );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenOption);
            return token;
        }
    }
}