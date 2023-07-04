namespace ECommerce.Infra.Auth
{
    public class AcessToken
    {
        public bool Authenticated { get; set; }
        public string Created { get; set; }
        public string Expiration { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
