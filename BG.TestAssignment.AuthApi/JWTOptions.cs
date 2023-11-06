namespace BGNet.TestAssignment.Api
{
    public class JWTOptions
    {
        public const string Jwt = "Jwt";

        public int Expire { get; set; }
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string TokenValidityInMinutes { get; set; }
    }
}
