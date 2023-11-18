namespace APIBookingHotel.Models
{
    public class Checklogs
    {
        public string Name { get; set; }
        public string Department { get; set; }
        public ILogger _logger { get; set; }
        public Checklogs(string name, string department, ILogger<Checklogs> logger)
        {
            Name = name;
            Department = department;
            _logger = logger;
            _logger.LogInformation("Booking " + Name + " Hotel " + Department);
        }

    }
}
