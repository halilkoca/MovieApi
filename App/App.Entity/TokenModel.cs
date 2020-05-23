namespace App.Entity
{
    public class TokenModel
    {
        public string Id { get; set; }
        public string Auth_Token { get; set; }
        public int Expires_In { get; set; }
    }
}
