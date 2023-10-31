namespace APIBookingHotel.Models
{
    public class UserModel
    {
       public string? Email { get; set; }
       public int ID { get; set; }
        public string? FullName { get; set; }
    }
    public class ResponseToken
    {
        public string? Status { get; set; }
        public string? Message { get; set; }
    }
    public class TokenModel
    {
        public string? AccessToken { get; set; }
        public string? RefreshToken { get; set; }
    }
}
