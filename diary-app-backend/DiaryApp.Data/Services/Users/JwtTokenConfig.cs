namespace DiaryApp.Data.Services.Users
{
    public class JwtTokenConfig
    {
        public string Secret { get; set; }
        public double AccessTokenExpiration { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
    }
}
