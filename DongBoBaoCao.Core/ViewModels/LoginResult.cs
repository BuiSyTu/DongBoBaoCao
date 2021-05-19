namespace DongBoBaoCao.Core.ViewModels
{
    public class LoginResult
    {
        public Token data { get; set; }
        public ErrorResult error { get; set; }
    }

    public class Token
    {
        public string accessToken { get; set; }
        public string expiresIn { get; set; }
        public string rerefreshToken { get; set; }
        public string tokenType { get; set; }
    }
}
