using System;

namespace App.Core.Utilities.Jwt
{
    public class TokenModel
    {
        public int Id { get; set; }
        public string Auth_Token { get; set; }
        public DateTime Expires_In { get; set; }
    }
}
