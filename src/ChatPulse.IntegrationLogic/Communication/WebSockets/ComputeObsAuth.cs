using ChatPulse.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace ChatPulse.IntegrationLogic.Communication.WebSockets
{
    public static class ComputeObsAuth
    {
        public static string ComputeAuth(string password, string salt, string challenge)
        {
            var secret = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password + salt)));

            return Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(secret + challenge))
            );
        }

        public static string ComputeAuth(string password, ObsAuthenticationInfo auth)
        {
            var secret = Convert.ToBase64String(SHA256.HashData(Encoding.UTF8.GetBytes(password + auth.Salt)));

            return Convert.ToBase64String(
                SHA256.HashData(Encoding.UTF8.GetBytes(secret + auth.Challenge))
            );
        }
    }
}