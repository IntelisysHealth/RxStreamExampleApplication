using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxStreamExampleApplication.Dtos
{
    public class DetailResponseDto
    {
        public string PharmacyId { get; set; }
        public string PrescriptionNumber { get; set; }
        public string Cost { get; set; }
    }
}
