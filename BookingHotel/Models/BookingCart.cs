namespace BookingHotel.Models
{
    public class BookingCart
    {
        public string BookingId { get; set; }
        public string? BookingName { get; set; }
        public string? BookingImage { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
    }
}
