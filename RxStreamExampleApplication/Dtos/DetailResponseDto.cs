using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxStreamExampleApplication.Dtos
{
    public class DetailResponseDto
    {
        public string Npi { get; set; }
        public string Ncpdp { get; set; }
        public string Status { get; set; }
        public string PrescriptionNumber { get; set; }
        public string Cost { get; set; }
    }
}
