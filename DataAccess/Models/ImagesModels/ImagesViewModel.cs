using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.ImagesModels
{
    public class ImagesViewModel
    {
        public string Id { get; set; }

        public string? ImgCode { get; set; }
        public string? PathServer { get; set; }
        public string? FileName { get; set; }
        public string? Description { get; set; }
        public DateTime? SysDate { get; set; }
    }
}
