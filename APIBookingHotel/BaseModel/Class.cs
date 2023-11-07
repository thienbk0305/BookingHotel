using System.ComponentModel.DataAnnotations;

namespace APIBookingHotel.BaseModel
{
    public class UploadFileInput
    {
        [Required]
        public IFormFile? File { get; set; }
    }

}
